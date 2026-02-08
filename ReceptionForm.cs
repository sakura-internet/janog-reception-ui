using bpac;
using Microsoft.VisualBasic.Logging;
using System.IO.Ports;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;

namespace janog_reception_ui
{
    public partial class ReceptionForm : Form
    {

        bpac.Document labelDocument;
        private Config _config;
        private SerialPort? _serialPort;

        public ReceptionForm()
        {
            InitializeComponent();

            _serialPort = new SerialPort();
            _serialPort.BaudRate = 115200;
            _serialPort.DataReceived += SerialPort_DataReceived;

            // Load Label Template
            string? exeDirPath = Path.GetDirectoryName(Application.ExecutablePath);
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
            labelDocument.Export(ExportType.bexBmp, filename, 72 * 4);

            Image img;
            using (var tmp = Image.FromFile(filename))
            {
                img = (Image)tmp.Clone();  // メモリ上にコピー
                tmp.Dispose();
            }

            previewBox.Image = img;
        }

        private void PrintLabel()
        {
            labelDocument.SetPrinter(_config.Printer, false);
            labelDocument.StartPrint("", PrintOptionConstants.bpoAutoCut);
            labelDocument.PrintOut(1, PrintOptionConstants.bpoAutoCut);
            labelDocument.EndPrint();
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

        private void execButton_Click(object sender, EventArgs e)
        {
            execute();
        }

        private void execute()
        {

            var auth = _config.Auth();
            Client client = new Client(auth.BaseUrl, auth.Username, auth.Password);
            Participant? participant;
            try
            {
                var response = client.AcceptParticipant(idBox.Text, _config.Gate);
                participant = response.Participant;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetLabelField("program", participant.Program);
            SetLabelField("full_name", participant.FullName);
            SetLabelField("organization", participant.Organization);
            UpdatePreview();
            PrintLabel();
        }

        private void radioDay1_CheckedChanged(object sender, EventArgs e)
        {
            SetDayImage("day1.png");
            UpdatePreview();
        }

        private void radioDay2_CheckedChanged(object sender, EventArgs e)
        {
            SetDayImage("day2.png");
            UpdatePreview();
        }

        private void radioDay3_CheckedChanged(object sender, EventArgs e)
        {
            SetDayImage("day3.png");
            UpdatePreview();
        }

        private void radioStaff_CheckedChanged(object sender, EventArgs e)
        {
            SetDayImage("staff.png");
            UpdatePreview();
        }

        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {

            EnvConfigForm form = new EnvConfigForm();
            form.config = _config;
            form.ShowDialog();
            form.config.SaveYAML();
            SetConfig(_config);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void printButton_Click(object sender, EventArgs e)
        {
            PrintLabel();
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
                    // "passbook?p=01KCJMZ8EF29PQ70ZY3RV42H19"
                    Console.WriteLine(line);
                    var ulid = TryExtractUlid(line);
                    if (ulid != "")
                    {
                        // UIスレッドに処理を渡す
                        BeginInvoke(new Action(() =>
                        {
                            if (idBox.Text != ulid)
                            {
                                idBox.Text = ulid;
                                execute();
                            }
                        }));
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
    }
}
