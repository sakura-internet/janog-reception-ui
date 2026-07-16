using bpac;
using System.Diagnostics;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;

namespace janog_reception_ui
{
    public partial class ReceptionSetControl : UserControl
    {
        private static readonly TimeSpan PrintStatusTimeout = TimeSpan.FromSeconds(30);
        private static readonly Regex UlidAfterP = new(
            @"\?p=(?<ulid>[0-9A-HJKMNP-TV-Z]{26})(?:\b|$)",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private readonly StringBuilder _serialBuffer = new();
        private Config? _config;
        private ReceptionSetConfig? _setConfig;
        private SerialPort? _serialPort;
        private Document? _labelDocument;
        private System.Media.SoundPlayer? _speakersVoicePlayer;
        private Func<string, bool>? _tryHandleConfigQr;
        private Func<bool>? _isSettingsOpen;
        private string _currentImage = "day1.png";
        private int _isReceptionProcessing;
        private bool _heartbeatInProgress;
        private DateTimeOffset? _lastTransmissionAt;
        private bool _shutDown;

        private sealed record PrintStatusResult(int EventCode, int ErrorCode, string ErrorString);

        public ReceptionSetControl()
        {
            InitializeComponent();
            errorLabel.Text = string.Empty;
            RefreshLastTransmissionLabel();
        }

        internal void Initialize(
            Config config,
            ReceptionSetConfig setConfig,
            Func<string, bool> tryHandleConfigQr,
            Func<bool> isSettingsOpen)
        {
            _config = config;
            _setConfig = setConfig;
            _tryHandleConfigQr = tryHandleConfigQr;
            _isSettingsOpen = isSettingsOpen;
            _shutDown = false;

            gateLabel.Text = $"ゲート: {DisplayValue(setConfig.Gate)}";
            printerLabel.Text = $"プリンタ: {DisplayValue(setConfig.Printer)}";
            readerLabel.Text = $"COM: {DisplayValue(setConfig.Reader.Port)}";

            string exeDirPath = Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty;
            _speakersVoicePlayer = new System.Media.SoundPlayer(
                Path.Combine(exeDirPath, "voice-speakers.wav"));
            _labelDocument = new Document();
            if (!_labelDocument.Open(Path.Combine(exeDirPath, "label.lbx")))
            {
                SetError("ラベルテンプレートを読み込めませんでした");
            }
            else
            {
                SetDayImage(_currentImage);
                UpdatePreview();
            }

            _serialPort = new SerialPort { BaudRate = 115200 };
            _serialPort.DataReceived += SerialPort_DataReceived;
            ConfigureSerialPort(showInitialError: true);
            reconnectTimer.Enabled = !string.IsNullOrWhiteSpace(setConfig.Reader.Port);
            heartbeatTimer.Start();
        }

        private static string DisplayValue(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? "未設定" : value;
        }

        private ReceptionSetConfig SetConfig =>
            _setConfig ?? throw new InvalidOperationException("Reception set is not initialized.");

        private Config AppConfig =>
            _config ?? throw new InvalidOperationException("Configuration is not initialized.");

        private Document LabelDocument =>
            _labelDocument ?? throw new InvalidOperationException("Label document is not initialized.");

        private void ConfigureSerialPort(bool showInitialError)
        {
            if (_serialPort == null || string.IsNullOrWhiteSpace(SetConfig.Reader.Port))
            {
                readerLabel.Text = "COM: 未設定";
                return;
            }

            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _serialPort.PortName = SetConfig.Reader.Port;
                _serialPort.Open();
                readerLabel.Text = $"COM: {SetConfig.Reader.Port} (接続済み)";
            }
            catch (Exception ex)
            {
                readerLabel.Text = $"COM: {SetConfig.Reader.Port} (エラー)";
                if (showInitialError)
                {
                    SetError("シリアルポートのオープンに失敗しました: " + ex.Message);
                }
            }
        }

        private void UpdatePreview()
        {
            if (_labelDocument == null) return;

            string filename = Path.GetTempFileName();
            try
            {
                _labelDocument.Export(ExportType.bexBmp, filename, 72 * 4);
                Image image;
                using (var temporaryImage = Image.FromFile(filename))
                {
                    image = (Image)temporaryImage.Clone();
                }

                var previousImage = previewBox.Image;
                previewBox.Image = image;
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
                catch (IOException) when (attempt < maxAttempts)
                {
                    await Task.Delay(100);
                }
                catch (UnauthorizedAccessException) when (attempt < maxAttempts)
                {
                    await Task.Delay(100);
                }
            }
        }

        private static Exception CreatePrintException(Document document, string stage, int? printEvent = null)
        {
            var documentErrorCode = 0;
            var printerErrorCode = 0;
            var printerErrorString = string.Empty;

            try { documentErrorCode = document.ErrorCode; }
            catch (Exception ex) { Debug.WriteLine($"Failed to read document error code: {ex.Message}"); }

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

        private async Task PrintDocumentAsync(Document document)
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
                    completion.TrySetResult(new PrintStatusResult(-1, -1, ex.Message));
                }
            }

            document.Printed += PrintedHandler;
            try
            {
                if (!document.SetPrinter(SetConfig.Printer, false))
                    throw CreatePrintException(document, "SetPrinter failed");
                if (!document.StartPrint("", PrintOptionConstants.bpoAutoCut))
                    throw CreatePrintException(document, "StartPrint failed");

                printStarted = true;
                if (!document.PrintOut(1, PrintOptionConstants.bpoAutoCut))
                    throw CreatePrintException(document, "PrintOut failed");
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
                    try { document.EndPrint(); }
                    catch (Exception ex) { Debug.WriteLine($"EndPrint cleanup failed: {ex.Message}"); }
                }
                document.Printed -= PrintedHandler;
            }
        }

        private void ResetLabelDocument()
        {
            string exeDirPath = Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty;
            _labelDocument?.Close();
            _labelDocument = new Document();
            if (!_labelDocument.Open(Path.Combine(exeDirPath, "label.lbx")))
                throw new Exception("Load label template error");
            SetDayImage(_currentImage);
        }

        private async Task PrintLabelAsync()
        {
            try
            {
                await PrintDocumentAsync(LabelDocument);
            }
            catch
            {
                try { ResetLabelDocument(); }
                catch (Exception ex) { Debug.WriteLine($"Label document reset failed: {ex.Message}"); }
                throw;
            }
        }

        private async Task PrintSpeakersLabelAsync()
        {
            string exeDirPath = Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty;
            var speakersLabelDocument = new Document();
            try
            {
                if (!speakersLabelDocument.Open(Path.Combine(exeDirPath, "label-speakers.lbx")))
                    throw new Exception("Load speakers label template error");
                await PrintDocumentAsync(speakersLabelDocument);
            }
            finally
            {
                speakersLabelDocument.Close();
            }
        }

        private void SetLabelField(string fieldName, string value)
        {
            LabelDocument.GetObject(fieldName).Text = value;
        }

        private void SetDayImage(string filename)
        {
            LabelDocument.GetObject("day_image").SetData(0, filename, 0);
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
            if (!TryStartReception()) return;
            try { await ExecuteAsync(); }
            finally { FinishReception(); }
        }

        private async Task ReportTerminalStatusSafelyAsync(
            Client client,
            string status,
            string eventName,
            string? error = null)
        {
            if (_isSettingsOpen?.Invoke() == true)
            {
                return;
            }

            try
            {
                await client.ReportTerminalStatusAsync(SetConfig.Gate, status, eventName, error);
                MarkTransmissionSent();
                ResetHeartbeatTimer();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Terminal status report failed: {ex.Message}");
            }
        }

        private async Task ExecuteAsync()
        {
            SetError(string.Empty);
            var auth = AppConfig.Auth();
            var client = new Client(auth.BaseUrl, auth.Username, auth.Password);
            Participant participant;

            try
            {
                participant = client.AcceptParticipant(idBox.Text, SetConfig.Gate, mediaBox.Text).Participant;
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Beep.Play();
                SetError(ex.Message);
                await ReportTerminalStatusSafelyAsync(client, "error", "受付処理エラー", ex.Message);
                return;
            }

            try
            {
                if (participant.Type == "staff") SetDayImage("staff.png");
                else if (participant.Type == "host") SetDayImage("host.png");

                SetLabelField("program", participant.Program);
                SetLabelField("full_name", participant.FullName);
                SetLabelField("organization", participant.Organization);
                UpdatePreview();

                if (participant.Type == "speaker" && participant.AcceptCount == 1)
                {
                    if (AppConfig.AudioEnabled) _speakersVoicePlayer?.Play();
                    await PrintSpeakersLabelAsync();
                }
                await PrintLabelAsync();
                await ReportTerminalStatusSafelyAsync(client, "ok", "印刷完了");
            }
            catch (Exception ex)
            {
                System.Media.SystemSounds.Beep.Play();
                SetError(ex.Message);
                await ReportTerminalStatusSafelyAsync(client, "error", "印刷エラー", ex.Message);
            }
            finally
            {
                try { SetDayImage(_currentImage); }
                catch (Exception ex) { Debug.WriteLine($"Failed to restore label image: {ex.Message}"); }
            }
        }

        private void SetError(string message)
        {
            errorLabel.Text = message;
        }

        private void idBox_TextChanged(object sender, EventArgs e)
        {
            execButton.Enabled = Regex.IsMatch(idBox.Text, "^[0123456789ABCDEFGHJKMNPQRSTVWXYZ]{26}$");
        }

        private async void execButton_Click(object sender, EventArgs e)
        {
            await ExecuteIfIdleAsync();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            idBox.Clear();
            mediaBox.Clear();
            idBox.Focus();
        }

        private async void printButton_Click(object sender, EventArgs e)
        {
            if (!TryStartReception()) return;
            try
            {
                var auth = AppConfig.Auth();
                var client = new Client(auth.BaseUrl, auth.Username, auth.Password);
                try
                {
                    await PrintLabelAsync();
                    await ReportTerminalStatusSafelyAsync(client, "ok", "印刷完了");
                }
                catch (Exception ex)
                {
                    System.Media.SystemSounds.Beep.Play();
                    SetError(ex.Message);
                    await ReportTerminalStatusSafelyAsync(client, "error", "印刷エラー", ex.Message);
                }
            }
            finally
            {
                FinishReception();
            }
        }

        private void radioDay1_CheckedChanged(object sender, EventArgs e) => SetSelectedImage(radioDay1, "day1.png");
        private void radioDay2_CheckedChanged(object sender, EventArgs e) => SetSelectedImage(radioDay2, "day2.png");
        private void radioDay3_CheckedChanged(object sender, EventArgs e) => SetSelectedImage(radioDay3, "day3.png");
        private void radioStaff_CheckedChanged(object sender, EventArgs e) => SetSelectedImage(radioStaff, "staff.png");
        private void radioHost_CheckedChanged(object sender, EventArgs e) => SetSelectedImage(radioHost, "host.png");

        private void SetSelectedImage(RadioButton radioButton, string filename)
        {
            if (!radioButton.Checked || _labelDocument == null) return;
            _currentImage = filename;
            SetDayImage(filename);
            UpdatePreview();
        }

        internal static string TryExtractUlid(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            var match = UlidAfterP.Match(input);
            return match.Success ? match.Groups["ulid"].Value : string.Empty;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sender is not SerialPort serialPort) return;
            string data;
            try { data = serialPort.ReadExisting(); }
            catch (Exception) { return; }

            lock (_serialBuffer)
            {
                _serialBuffer.Append(data);
                while (true)
                {
                    int index = _serialBuffer.ToString().IndexOf('\r');
                    if (index < 0) break;

                    string line = _serialBuffer.ToString(0, index).Trim();
                    _serialBuffer.Remove(0, index + 1);
                    if (!string.IsNullOrEmpty(line)) HandleScannedLine(line);
                }
            }
        }

        private void HandleScannedLine(string line)
        {
            if (_tryHandleConfigQr?.Invoke(line) == true) return;

            string ulid = TryExtractUlid(line);
            if (string.IsNullOrEmpty(ulid) || !TryStartReception()) return;

            int queryIndex = line.IndexOf('?');
            string beforeQuery = queryIndex >= 0 ? line[..queryIndex] : line;
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
            catch (ObjectDisposedException) { FinishReception(); }
            catch (InvalidOperationException) { FinishReception(); }
        }

        private void reconnectTimer_Tick(object sender, EventArgs e)
        {
            if (_serialPort == null || _serialPort.IsOpen || string.IsNullOrWhiteSpace(SetConfig.Reader.Port)) return;
            readerLabel.Text = $"COM: {SetConfig.Reader.Port} (切断)";
            ConfigureSerialPort(showInitialError: false);
        }

        private void ResetHeartbeatTimer()
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                BeginInvoke(ResetHeartbeatTimer);
                return;
            }
            heartbeatTimer.Stop();
            heartbeatTimer.Start();
        }

        private void MarkTransmissionSent()
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                BeginInvoke(MarkTransmissionSent);
                return;
            }

            _lastTransmissionAt = DateTimeOffset.Now;
            RefreshLastTransmissionLabel();
        }

        internal void RefreshLastTransmissionLabel()
        {
            if (!_lastTransmissionAt.HasValue)
            {
                lastTransmissionLabel.Text = "最終送信: なし";
                return;
            }

            var elapsed = DateTimeOffset.Now - _lastTransmissionAt.Value;
            var elapsedSeconds = Math.Max(0, (long)elapsed.TotalSeconds);
            lastTransmissionLabel.Text =
                $"最終送信: {_lastTransmissionAt.Value.LocalDateTime:yyyy/MM/dd HH:mm:ss} ({elapsedSeconds}秒前)";
        }

        private async void heartbeatTimer_Tick(object sender, EventArgs e)
        {
            if (_heartbeatInProgress ||
                Volatile.Read(ref _isReceptionProcessing) != 0 ||
                _config == null ||
                _setConfig == null ||
                _isSettingsOpen?.Invoke() == true)
            {
                return;
            }

            _heartbeatInProgress = true;
            try
            {
                var auth = AppConfig.Auth();
                var client = new Client(auth.BaseUrl, auth.Username, auth.Password);
                await client.ReportTerminalStatusHeartbeatAsync(SetConfig.Gate);
                MarkTransmissionSent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Terminal heartbeat failed ({SetConfig.Gate}): {ex.Message}");
            }
            finally
            {
                _heartbeatInProgress = false;
            }
        }

        internal void Shutdown()
        {
            if (_shutDown) return;
            _shutDown = true;
            reconnectTimer.Stop();
            heartbeatTimer.Stop();

            if (_serialPort != null)
            {
                _serialPort.DataReceived -= SerialPort_DataReceived;
                try { if (_serialPort.IsOpen) _serialPort.Close(); }
                catch (Exception ex) { Debug.WriteLine($"Serial port close failed: {ex.Message}"); }
                _serialPort.Dispose();
                _serialPort = null;
            }

            previewBox.Image?.Dispose();
            previewBox.Image = null;
            try { _labelDocument?.Close(); }
            catch (Exception ex) { Debug.WriteLine($"Label document close failed: {ex.Message}"); }
            _labelDocument = null;
            _speakersVoicePlayer?.Dispose();
            _speakersVoicePlayer = null;
        }
    }
}
