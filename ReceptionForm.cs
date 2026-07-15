using bpac;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using System.IO.Ports;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;

namespace janog_reception_ui
{
    public partial class ReceptionForm : Form
    {

        bpac.Document labelDocument;
        private Config _config;
        private SerialPort? _serialPort;
        private readonly System.Media.SoundPlayer _speakersVoicePlayer;
        private string _currentImage = "day1.png";
        private EnvConfigForm? _activeEnvConfigForm;
        private int _isReceptionProcessing;
        private static readonly TimeSpan PrintStatusTimeout = TimeSpan.FromSeconds(30);

        private sealed record PrintStatusResult(int EventCode, int ErrorCode, string ErrorString);

        public ReceptionForm()
        {
            InitializeComponent();

            _serialPort = new SerialPort();
            _serialPort.BaudRate = 115200;
            _serialPort.DataReceived += SerialPort_DataReceived;

            errorLabel.Text = "";

            // Load Label Template
            string? exeDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            _speakersVoicePlayer = new System.Media.SoundPlayer(
                Path.Combine(exeDirPath ?? string.Empty, "voice-speakers.wav"));
            labelDocument = new bpac.Document();
            if (!labelDocument.Open((exeDirPath ?? string.Empty) + "\\" + "label.lbx"))
            {
                MessageBox.Show("Load label template error");
            }
            _config = Config.LoadYAML();
            SetConfig(_config);
        }

        private void ReceptionForm_Load(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void SetConfig(Config cfg)
        {
            if (cfg.Environment.Environment != EnvironmentKind.Production)
            {
                toolStripEnvLabel.Text = "開発環境";
                toolStripEnvLabel.BackColor = Color.Red;
            }
            else
            {
                toolStripEnvLabel.Text = "本番環境";
                toolStripEnvLabel.BackColor = Color.DodgerBlue;
            }
            gateLabel.Text = cfg.Gate;
            printerLabel.Text = cfg.Printer;
            readerLabel.Text = cfg.Reader.Port;

            if (cfg.Reader.Port != "" && _serialPort != null)
            {
                timer1.Enabled = true;
                _serialPort.Close();
                _serialPort.PortName = cfg.Reader.Port;
                try
                {
                    _serialPort.Open();
                    readerLabel.Text = cfg.Reader.Port + " (接続済み)";
                }
                catch (Exception ex)
                {
                    readerLabel.Text = cfg.Reader.Port + " (エラー)";
                    MessageBox.Show("シリアルポートのオープンに失敗しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void UpdatePreview()
        {
            string filename = Path.GetTempFileName();
            try
            {
                labelDocument.Export(ExportType.bexBmp, filename, 72 * 4);

                Image img;
                using (var tmp = Image.FromFile(filename))
                {
                    img = (Image)tmp.Clone();  // メモリ上にコピー
                }

                var previousImage = previewBox.Image;
                previewBox.Image = img;
                previousImage?.Dispose();
            }
            finally
            {
                _ = DeleteTempFileWithRetryAsync(filename);
            }
        }

        private static async Task DeleteTempFileWithRetryAsync(string filename)
        {
            const int maxAttempts = 30;

            for (var attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    File.Delete(filename);
                    return;
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
                {
                    if (attempt == maxAttempts)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"一時ファイルを削除できませんでした: {filename}: {ex.Message}");
                        return;
                    }
                }

                await Task.Delay(100);
            }
        }

        private static Exception CreatePrintException(bpac.Document document, string stage, int? printEvent = null)
        {
            var documentErrorCode = 0;
            var printerErrorCode = 0;
            var printerErrorString = string.Empty;

            try
            {
                documentErrorCode = document.ErrorCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to read document error code: {ex.Message}");
            }

            try
            {
                printerErrorCode = document.Printer.ErrorCode;
                printerErrorString = document.Printer.ErrorString ?? string.Empty;
            }
            catch (Exception ex)
            {
                printerErrorString = ex.Message;
            }

            var eventText = printEvent.HasValue
                ? Enum.IsDefined(typeof(PrintEvent), printEvent.Value)
                    ? ((PrintEvent)printEvent.Value).ToString()
                    : printEvent.Value.ToString()
                : "none";
            return new Exception(
                $"{stage}: event={eventText}, documentErrorCode={documentErrorCode}, " +
                $"printerErrorCode={printerErrorCode}, printerError={printerErrorString}");
        }

        private async Task PrintDocumentAsync(bpac.Document document)
        {
            var completion = new TaskCompletionSource<PrintStatusResult>(
                TaskCreationOptions.RunContinuationsAsynchronously);
            var printStarted = false;

            void PrintedHandler(int eventCode, object value)
            {
                try
                {
                    completion.TrySetResult(new PrintStatusResult(
                        eventCode,
                        document.Printer.ErrorCode,
                        document.Printer.ErrorString ?? string.Empty));
                }
                catch (Exception ex)
                {
                    completion.TrySetResult(new PrintStatusResult(
                        eventCode,
                        -1,
                        $"Failed to read printer status: {ex.Message}"));
                }
            }

            document.Printed += PrintedHandler;
            try
            {
                if (!document.SetPrinter(_config.Printer, false))
                {
                    throw CreatePrintException(document, "SetPrinter failed");
                }
                if (!document.StartPrint("", PrintOptionConstants.bpoAutoCut))
                {
                    throw CreatePrintException(document, "StartPrint failed");
                }
                printStarted = true;
                if (!document.PrintOut(1, PrintOptionConstants.bpoAutoCut))
                {
                    throw CreatePrintException(document, "PrintOut failed");
                }
                if (!document.EndPrint())
                {
                    printStarted = false;
                    throw CreatePrintException(document, "EndPrint failed");
                }
                printStarted = false;

                PrintStatusResult result;
                try
                {
                    result = await completion.Task.WaitAsync(PrintStatusTimeout);
                }
                catch (TimeoutException)
                {
                    throw CreatePrintException(document, "印刷状態確認タイムアウト (30秒)");
                }

                if (result.EventCode != (int)PrintEvent.bpePrinted || result.ErrorCode != 0)
                {
                    var eventText = Enum.IsDefined(typeof(PrintEvent), result.EventCode)
                        ? ((PrintEvent)result.EventCode).ToString()
                        : result.EventCode.ToString();
                    throw new Exception(
                        $"印刷エラー: event={eventText}, printerErrorCode={result.ErrorCode}, " +
                        $"printerError={result.ErrorString}");
                }
            }
            finally
            {
                if (printStarted)
                {
                    try
                    {
                        document.EndPrint();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"EndPrint cleanup failed: {ex.Message}");
                    }
                }
                document.Printed -= PrintedHandler;
            }
        }

        private void ResetLabelDocument()
        {
            string? exeDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            labelDocument.Close();
            labelDocument = new bpac.Document();
            if (!labelDocument.Open(Path.Combine(exeDirPath ?? string.Empty, "label.lbx")))
            {
                throw new Exception("Load label template error");
            }
            SetDayImage(_currentImage);
        }

        private async Task PrintLabelAsync()
        {
            try
            {
                await PrintDocumentAsync(labelDocument);
            }
            catch
            {
                try
                {
                    ResetLabelDocument();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Label document reset failed: {ex.Message}");
                }
                throw;
            }
        }

        private async Task PrintSpeakersLabelAsync()
        {
            string? exeDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            var speakersLabelDocument = new bpac.Document();
            try
            {
                if (!speakersLabelDocument.Open(Path.Combine(exeDirPath ?? string.Empty, "label-speakers.lbx")))
                {
                    throw new Exception("Load speakers label template error");
                }
                await PrintDocumentAsync(speakersLabelDocument);
            }
            finally
            {
                speakersLabelDocument.Close();
            }
        }
        private void SetLabelField(string fieldName, string value)
        {
            labelDocument.GetObject(fieldName).Text = value;
        }

        private void SetDayImage(string filename)
        {
            labelDocument.GetObject("day_image").SetData(0, filename, 0);
        }

        private void idBox_TextChanged(object sender, EventArgs e)
        {
            // check ULID format by regex
            var regex = new System.Text.RegularExpressions.Regex("^[0123456789ABCDEFGHJKMNPQRSTVWXYZ]{26}$");
            execButton.Enabled = regex.IsMatch(idBox.Text);
        }

        private async void execButton_Click(object sender, EventArgs e)
        {
            await ExecuteIfIdleAsync();
        }

        private bool TryStartReception()
        {
            return Interlocked.CompareExchange(ref _isReceptionProcessing, 1, 0) == 0;
        }

        private void FinishReception()
        {
            Volatile.Write(ref _isReceptionProcessing, 0);
        }

        private async Task ExecuteIfIdleAsync()
        {
            if (!TryStartReception())
            {
                return;
            }

            try
            {
                await ExecuteAsync();
            }
            finally
            {
                FinishReception();
            }
        }

        private async Task ReportTerminalStatusSafelyAsync(
            Client client,
            string status,
            string eventName,
            string? error = null)
        {
            try
            {
                await client.ReportTerminalStatusAsync(_config.Gate, status, eventName, error);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Terminal status report failed: {ex.Message}");
            }
        }

        private async Task ExecuteAsync()
        {

            errorLabel.Text = "";
            var auth = _config.Auth();
            Client client = new Client(auth.BaseUrl, auth.Username, auth.Password);
            Participant? participant;
            try
            {
                var response = client.AcceptParticipant(idBox.Text, _config.Gate, mediaBox.Text);
                participant = response.Participant;
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Beep.Play();
                errorLabel.Text = ex.Message;
                await ReportTerminalStatusSafelyAsync(client, "error", "受付処理エラー", ex.Message);
                return;
            }

            try
            {
                if (participant.Type == "staff")
                {
                    SetDayImage("staff.png");
                }
                else if (participant.Type == "host")
                {
                    SetDayImage("host.png");
                }

                SetLabelField("program", participant.Program);
                SetLabelField("full_name", participant.FullName);
                SetLabelField("organization", participant.Organization);
                UpdatePreview();
                if (participant.Type == "speaker" && participant.AcceptCount == 1)
                {
                    if (_config.AudioEnabled)
                    {
                        _speakersVoicePlayer.Play();
                    }
                    await PrintSpeakersLabelAsync();
                }
                await PrintLabelAsync();
                await ReportTerminalStatusSafelyAsync(client, "ok", "印刷完了");
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Beep.Play();
                errorLabel.Text = ex.Message;
                await ReportTerminalStatusSafelyAsync(client, "error", "印刷エラー", ex.Message);
            }
            finally
            {
                try
                {
                    SetDayImage(_currentImage);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to restore label image: {ex.Message}");
                }
            }
        }

        private void radioDay1_CheckedChanged(object sender, EventArgs e)
        {
            _currentImage = "day1.png";
            SetDayImage(_currentImage);
            UpdatePreview();
        }

        private void radioDay2_CheckedChanged(object sender, EventArgs e)
        {
            _currentImage = "day2.png";
            SetDayImage(_currentImage);
            UpdatePreview();
        }

        private void radioDay3_CheckedChanged(object sender, EventArgs e)
        {
            _currentImage = "day3.png";
            SetDayImage(_currentImage);
            UpdatePreview();
        }

        private void radioStaff_CheckedChanged(object sender, EventArgs e)
        {
            _currentImage = "staff.png";
            SetDayImage(_currentImage);
            UpdatePreview();
        }

        private void radioHost_CheckedChanged(object sender, EventArgs e)
        {
            _currentImage = "host.png";
            SetDayImage(_currentImage);
            UpdatePreview();
        }

        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnvConfigForm form = new EnvConfigForm();
            form.config = _config;
            _activeEnvConfigForm = form;
            try
            {
                form.ShowDialog(this);
            }
            finally
            {
                if (ReferenceEquals(_activeEnvConfigForm, form))
                {
                    _activeEnvConfigForm = null;
                }
                form.Dispose();
            }
            form.config.SaveYAML();
            SetConfig(_config);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private async void printButton_Click(object sender, EventArgs e)
        {
            if (!TryStartReception())
            {
                return;
            }

            try
            {
                var auth = _config.Auth();
                var client = new Client(auth.BaseUrl, auth.Username, auth.Password);
                try
                {
                    await PrintLabelAsync();
                    await ReportTerminalStatusSafelyAsync(client, "ok", "印刷完了");
                }
                catch (Exception ex)
                {
                    System.Media.SystemSounds.Beep.Play();
                    errorLabel.Text = ex.Message;
                    await ReportTerminalStatusSafelyAsync(client, "error", "印刷エラー", ex.Message);
                }
            }
            finally
            {
                FinishReception();
            }
        }

        StringBuilder buffer = new StringBuilder();
        // ULID: Crockford Base32で26文字（0-9A-HJKMNP-TV-Z、I/L/O/Uなし）
        private static readonly Regex UlidAfterP =
            new Regex(@"\?p=(?<ulid>[0-9A-HJKMNP-TV-Z]{26})(?:\b|$)",
                      RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public static string TryExtractUlid(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            var m = UlidAfterP.Match(input);
            if (!m.Success)
                return "";

            return m.Groups["ulid"].Value;
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadExisting();
            Console.WriteLine(data);
            buffer.Append(data);
            while (true)
            {
                string current = buffer.ToString();
                int index = current.IndexOf("\r");
                if (index < 0) break;

                string line = current.Substring(0, index).Trim();
                buffer.Remove(0, index + 1);

                if (line != "")
                {
                    var configForm = _activeEnvConfigForm;
                    if (configForm != null)
                    {
                        // 設定画面が開いている間は、参加者受付QRではなく設定用QRとして扱う。
                        BeginInvoke(new Action(() =>
                        {
                            if (ReferenceEquals(_activeEnvConfigForm, configForm) && !configForm.IsDisposed)
                            {
                                configForm.HandleQrScan(line);
                            }
                        }));
                        continue;
                    }

                    // "passbook?p=01KCJMZ8EF29PQ70ZY3RV42H19"
                    Console.WriteLine(line);
                    var ulid = TryExtractUlid(line);
                    if (ulid != "")
                    {
                        if (!TryStartReception())
                        {
                            continue;
                        }

                        int q = line.IndexOf('?');
                        string beforeQuery = q >= 0 ? line.Substring(0, q) : line;

                        // UIスレッドに処理を渡す
                        try
                        {
                            BeginInvoke(new Action(async () =>
                            {
                                try
                                {
                                    if (idBox.Text != ulid)
                                    {
                                        idBox.Text = ulid;
                                        mediaBox.Text = beforeQuery;
                                        await ExecuteAsync();
                                    }
                                }
                                finally
                                {
                                    FinishReception();
                                }
                            }));
                        }
                        catch (ObjectDisposedException)
                        {
                            FinishReception();
                        }
                        catch (InvalidOperationException)
                        {
                            FinishReception();
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_serialPort != null && !_serialPort.IsOpen)
            {
                readerLabel.Text = _config.Reader.Port + " (切断)";

                if (_config.Reader.Port == "") return;
                _serialPort.PortName = _config.Reader.Port;
                try
                {
                    _serialPort.Open();
                    readerLabel.Text = _config.Reader.Port + " (接続済み)";
                }
                catch (Exception ex)
                {
                    readerLabel.Text = _config.Reader.Port + " (エラー)";
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                previewBox.Image?.Dispose();
                previewBox.Image = null;
                labelDocument.Close();
            }
            finally
            {
                base.OnFormClosed(e);
            }
        }
    }
}
