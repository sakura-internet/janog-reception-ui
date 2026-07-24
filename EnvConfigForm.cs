using bpac;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Management;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace janog_reception_ui
{
    public partial class EnvConfigForm : Form
    {
        internal Config config = null!;

        private readonly List<ReceptionSetConfig> _workingSets = new();
        private bool _changingSelection;
        private int _selectedSetIndex = -1;
        private Document? _printerDocument;

        public EnvConfigForm()
        {
            InitializeComponent();
        }

        private void EnvConfigForm_Load(object sender, EventArgs e)
        {
            config.Normalize();
            _workingSets.Clear();
            _workingSets.AddRange(config.ReceptionSets.Select(set => set.Clone()));
            EnsureAtLeastOneSet();

            PopulatePrinterList();
            PopulateReaderList();
            LoadGlobalSettings();
            RefreshSetList(0);
        }

        private void LoadGlobalSettings()
        {
            radioEnvProduction.Checked = config.Environment.Environment == EnvironmentKind.Production;
            radioEnvDevelop.Checked = !radioEnvProduction.Checked;
            audioEnabledCheckBox.Checked = config.AudioEnabled;
            textBoxDevBaseUrl.Text = config.Environment.Develop.BaseUrl;
            textBoxDevUsername.Text = config.Environment.Develop.Username;
            textBoxDevPassword.Text = config.Environment.Develop.Password;
            textBoxProdBaseUrl.Text = config.Environment.Production.BaseUrl;
            textBoxProdUsername.Text = config.Environment.Production.Username;
            textBoxProdPassword.Text = config.Environment.Production.Password;
        }

        private void PopulatePrinterList()
        {
            _printerDocument = new Document();
            printerComboBox.Items.Clear();
            printerComboBox.Items.Add(string.Empty);

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (_printerDocument.Printer.IsPrinterSupported(printer))
                {
                    AddPrinterIfMissing(printer);
                }
            }

            foreach (var set in _workingSets)
            {
                AddPrinterIfMissing(set.Printer);
            }
        }

        private void AddPrinterIfMissing(string? printer)
        {
            string value = printer ?? string.Empty;
            if (!printerComboBox.Items.Cast<string>().Contains(value, StringComparer.OrdinalIgnoreCase))
            {
                printerComboBox.Items.Add(value);
            }
        }

        private void PopulateReaderList()
        {
            readerComboBox.Items.Clear();
            readerComboBox.Items.Add(new SerialPortItem(string.Empty, string.Empty));
            foreach (var port in BuildPortList())
            {
                AddReaderIfMissing(port.Port, port.Caption);
            }
            foreach (var set in _workingSets)
            {
                AddReaderIfMissing(set.Reader.Port, set.Reader.Port);
            }
        }

        private void AddReaderIfMissing(string? port, string? caption)
        {
            string value = port ?? string.Empty;
            if (readerComboBox.Items.Cast<SerialPortItem>()
                .Any(item => string.Equals(item.Port, value, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }
            readerComboBox.Items.Add(new SerialPortItem(value, caption ?? value));
        }

        private void EnsureAtLeastOneSet()
        {
            if (_workingSets.Count == 0)
            {
                _workingSets.Add(new ReceptionSetConfig());
            }
        }

        private static string BuildSetDisplayName(ReceptionSetConfig set, int index)
        {
            string gate = string.IsNullOrWhiteSpace(set.Gate) ? "未設定" : set.Gate;
            return $"{index + 1}. {gate}";
        }

        private void RefreshSetList(int selectedIndex)
        {
            _changingSelection = true;
            try
            {
                setListBox.BeginUpdate();
                setListBox.Items.Clear();
                for (var i = 0; i < _workingSets.Count; i++)
                {
                    setListBox.Items.Add(BuildSetDisplayName(_workingSets[i], i));
                }
                setListBox.EndUpdate();

                selectedIndex = Math.Clamp(selectedIndex, 0, _workingSets.Count - 1);
                setListBox.SelectedIndex = selectedIndex;
                _selectedSetIndex = selectedIndex;
                LoadSetEditor(_workingSets[selectedIndex]);
                UpdateMoveButtons();
            }
            finally
            {
                _changingSelection = false;
            }
        }

        private void LoadSetEditor(ReceptionSetConfig set)
        {
            textBoxGate.Text = set.Gate;
            AddPrinterIfMissing(set.Printer);
            printerComboBox.SelectedItem = set.Printer;
            if (printerComboBox.SelectedIndex < 0) printerComboBox.SelectedIndex = 0;

            AddReaderIfMissing(set.Reader.Port, set.Reader.Port);
            readerComboBox.SelectedItem = readerComboBox.Items.Cast<SerialPortItem>()
                .First(item => string.Equals(item.Port, set.Reader.Port, StringComparison.OrdinalIgnoreCase));
        }

        private void SaveSetEditor(int index)
        {
            if (index < 0 || index >= _workingSets.Count) return;
            var set = _workingSets[index];
            set.Gate = textBoxGate.Text;
            set.Printer = printerComboBox.SelectedItem?.ToString() ?? string.Empty;
            set.Reader = new Reader
            {
                Port = (readerComboBox.SelectedItem as SerialPortItem)?.Port ?? string.Empty,
                Serial = set.Reader.Serial,
            };
        }

        private void setListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_changingSelection || setListBox.SelectedIndex < 0) return;
            SaveSetEditor(_selectedSetIndex);
            int newIndex = setListBox.SelectedIndex;
            _selectedSetIndex = newIndex;
            _changingSelection = true;
            try
            {
                LoadSetEditor(_workingSets[newIndex]);
                UpdateMoveButtons();
            }
            finally
            {
                _changingSelection = false;
            }
        }

        private void addSetButton_Click(object sender, EventArgs e)
        {
            SaveSetEditor(_selectedSetIndex);
            _workingSets.Add(new ReceptionSetConfig());
            RefreshSetList(_workingSets.Count - 1);
            textBoxGate.Focus();
        }

        private void deleteSetButton_Click(object sender, EventArgs e)
        {
            if (_selectedSetIndex < 0) return;
            if (_workingSets.Count == 1)
            {
                _workingSets[0] = new ReceptionSetConfig();
                RefreshSetList(0);
                return;
            }

            int nextIndex = Math.Min(_selectedSetIndex, _workingSets.Count - 2);
            _workingSets.RemoveAt(_selectedSetIndex);
            RefreshSetList(nextIndex);
        }

        private void moveUpButton_Click(object sender, EventArgs e)
        {
            MoveSelectedSet(-1);
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            MoveSelectedSet(1);
        }

        private void MoveSelectedSet(int offset)
        {
            SaveSetEditor(_selectedSetIndex);
            int destination = _selectedSetIndex + offset;
            if (_selectedSetIndex < 0 || destination < 0 || destination >= _workingSets.Count) return;
            (_workingSets[_selectedSetIndex], _workingSets[destination]) =
                (_workingSets[destination], _workingSets[_selectedSetIndex]);
            RefreshSetList(destination);
        }

        private void UpdateMoveButtons()
        {
            moveUpButton.Enabled = _selectedSetIndex > 0;
            moveDownButton.Enabled = _selectedSetIndex >= 0 && _selectedSetIndex < _workingSets.Count - 1;
        }

        private void EnvConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSetEditor(_selectedSetIndex);
            EnsureAtLeastOneSet();
            config.ReceptionSets = _workingSets.Select(set => set.Clone()).ToList();
            config.Environment.Environment = radioEnvProduction.Checked
                ? EnvironmentKind.Production
                : EnvironmentKind.Develop;
            config.AudioEnabled = audioEnabledCheckBox.Checked;
            config.Environment.Develop.BaseUrl = textBoxDevBaseUrl.Text;
            config.Environment.Develop.Username = textBoxDevUsername.Text;
            config.Environment.Develop.Password = textBoxDevPassword.Text;
            config.Environment.Production.BaseUrl = textBoxProdBaseUrl.Text;
            config.Environment.Production.Username = textBoxProdUsername.Text;
            config.Environment.Production.Password = textBoxProdPassword.Text;
            try { _printerDocument?.Close(); }
            catch { }
            _printerDocument = null;
        }

        private async void playSoundButton_Click(object sender, EventArgs e)
        {
            playSoundButton.Enabled = false;
            try
            {
                string soundPath = Path.Combine(
                    Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty,
                    "voice-speakers.wav");
                await Task.Run(() =>
                {
                    using var soundPlayer = new System.Media.SoundPlayer(soundPath);
                    soundPlayer.PlaySync();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "音声の再生に失敗しました: " + ex.Message,
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed) playSoundButton.Enabled = true;
            }
        }

        public void HandleQrScan(string rawValue)
        {
            if (!TryParseQrCredentials(rawValue, out var credentials)) return;
            using var dialog = new QrImportConfirmDialog();
            var result = dialog.ShowDialog(this);
            if (result == DialogResult.Yes)
            {
                textBoxDevBaseUrl.Text = credentials.Url;
                textBoxDevUsername.Text = credentials.Username;
                textBoxDevPassword.Text = credentials.Password;
            }
            else if (result == DialogResult.No)
            {
                textBoxProdBaseUrl.Text = credentials.Url;
                textBoxProdUsername.Text = credentials.Username;
                textBoxProdPassword.Text = credentials.Password;
            }
        }

        private static bool TryParseQrCredentials(string rawValue, out QrCredentials credentials)
        {
            credentials = default;
            try
            {
                using var document = JsonDocument.Parse(rawValue);
                var root = document.RootElement;
                if (root.ValueKind != JsonValueKind.Object ||
                    !root.TryGetProperty("url", out var url) || url.ValueKind != JsonValueKind.String ||
                    !root.TryGetProperty("user", out var username) || username.ValueKind != JsonValueKind.String ||
                    !root.TryGetProperty("pass", out var password) || password.ValueKind != JsonValueKind.String)
                {
                    return false;
                }

                string? urlValue = url.GetString();
                string? usernameValue = username.GetString();
                string? passwordValue = password.GetString();
                if (string.IsNullOrWhiteSpace(urlValue) ||
                    string.IsNullOrWhiteSpace(usernameValue) ||
                    string.IsNullOrWhiteSpace(passwordValue))
                {
                    return false;
                }

                credentials = new QrCredentials(urlValue, usernameValue, passwordValue);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        private static Dictionary<string, string> GetComCaptionMapByPnP()
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var regex = new Regex(@"\(COM\d+\)", RegexOptions.IgnoreCase);
            using var searcher = new ManagementObjectSearcher(
                "SELECT Name FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'");

            foreach (ManagementObject managementObject in searcher.Get())
            {
                string? name = managementObject["Name"]?.ToString();
                if (string.IsNullOrWhiteSpace(name)) continue;
                var match = regex.Match(name);
                if (!match.Success) continue;
                string com = match.Value.Trim('(', ')');
                map.TryAdd(com, name);
            }
            return map;
        }

        private static SerialPortItem[] BuildPortList()
        {
            var captionMap = GetComCaptionMapByPnP();
            return SerialPort.GetPortNames()
                .OrderBy(port => port, StringComparer.OrdinalIgnoreCase)
                .Select(port => new SerialPortItem(
                    port,
                    captionMap.TryGetValue(port, out var caption) ? caption : port))
                .ToArray();
        }
    }

    internal sealed record SerialPortItem(string Port, string Caption)
    {
        public override string ToString() => string.IsNullOrEmpty(Port) ? string.Empty : Caption;
    }

    internal readonly record struct QrCredentials(string Url, string Username, string Password);

    internal sealed class QrImportConfirmDialog : Form
    {
        public QrImportConfirmDialog()
        {
            Text = "設定値の入力";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(390, 120);

            var message = new Label
            {
                AutoSize = true,
                Location = new Point(20, 20),
                Text = "設定値を入力しますか",
            };
            var developButton = new Button
            {
                DialogResult = DialogResult.Yes,
                Location = new Point(20, 65),
                Size = new Size(105, 30),
                Text = "開発環境",
            };
            var productionButton = new Button
            {
                DialogResult = DialogResult.No,
                Location = new Point(142, 65),
                Size = new Size(105, 30),
                Text = "本番環境",
            };
            var cancelButton = new Button
            {
                DialogResult = DialogResult.Cancel,
                Location = new Point(264, 65),
                Size = new Size(105, 30),
                Text = "キャンセル",
            };

            Controls.AddRange(new Control[] { message, developButton, productionButton, cancelButton });
            AcceptButton = developButton;
            CancelButton = cancelButton;
        }
    }
}
