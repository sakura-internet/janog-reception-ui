using bpac;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace janog_reception_ui
{
    public partial class EnvConfigForm : Form
    {

        bpac.Document labelDocument;
        internal Config config;

        public EnvConfigForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void EnvConfigForm_Load(object sender, EventArgs e)
        {
            labelDocument = new bpac.Document();
            var printers = labelDocument.Printer.GetInstalledPrinters();
            printerComboBox.Items.Clear();

            // プリンタ一覧を取得してコンボボックスへ設定
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (labelDocument.Printer.IsPrinterSupported(printer))
                {
                    printerComboBox.Items.Add(printer);
                    if (printer == config.Printer)
                    {
                        printerComboBox.SelectedItem = printer;
                    }
                }
            }

            // シリアルポート一覧を取得してコンボボックスへ設定
            var portList = BuildPortList();
            readerComboBox.Items.Clear();
            
            readerComboBox.DataSource = portList;
            readerComboBox.DisplayMember = "Caption"; // 表示名
            readerComboBox.ValueMember   = "Port";    // COMx
                                                      // いったん非選択状態にする
            readerComboBox.SelectedIndex = -1;
            if (!string.IsNullOrWhiteSpace(config.Reader.Port))
            {
                // ValueMember (= Port) と一致するものがあれば選択される
                readerComboBox.SelectedValue = config.Reader.Port;
            }

        }

        private void EnvConfigForm_Shown(object sender, EventArgs e)
        {
            if (config.Environment.Environment != EnvironmentKind.Production)
            {
                radioEnvDevelop.Checked = true;
                radioEnvProduction.Checked = false;
            }
            else
            {
                radioEnvDevelop.Checked = false;
                radioEnvProduction.Checked = true;
            }

            textBoxGate.Text = config.Gate;
            printerComboBox.SelectedItem = config.Printer;
            textBoxDevBaseUrl.Text = config.Environment.Develop.BaseUrl;
            textBoxDevUsername.Text = config.Environment.Develop.Username;
            textBoxDevPassword.Text = config.Environment.Develop.Password;
            textBoxProdBaseUrl.Text = config.Environment.Production.BaseUrl;
            textBoxProdUsername.Text = config.Environment.Production.Username;
            textBoxProdPassword.Text = config.Environment.Production.Password;
        }

        private void EnvConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (radioEnvProduction.Checked)
            {
                config.Environment.Environment = EnvironmentKind.Production;
            }
            else
            {
                config.Environment.Environment = EnvironmentKind.Develop;
            }
            config.Gate = textBoxGate.Text;
            config.Printer = printerComboBox.SelectedItem.ToString();
            config.Reader = new Reader
            {
                Port = readerComboBox.SelectedValue.ToString(),
                Serial = "",
            };
            config.Environment.Develop.BaseUrl = textBoxDevBaseUrl.Text;
            config.Environment.Develop.Username = textBoxDevUsername.Text;
            config.Environment.Develop.Password = textBoxDevPassword.Text;
            config.Environment.Production.BaseUrl = textBoxProdBaseUrl.Text;
            config.Environment.Production.Username = textBoxProdUsername.Text;
            config.Environment.Production.Password = textBoxProdPassword.Text;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        static Dictionary<string, string> GetComCaptionMapByPnP()
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var regex = new Regex(@"\(COM\d+\)", RegexOptions.IgnoreCase);

            using var searcher = new ManagementObjectSearcher(
                "SELECT Name FROM Win32_PnPEntity WHERE Name LIKE '%(COM%'"
            );

            foreach (ManagementObject mo in searcher.Get())
            {
                var name = mo["Name"]?.ToString();
                if (string.IsNullOrWhiteSpace(name)) continue;

                var m = regex.Match(name);
                if (!m.Success) continue;

                // "(COM3)" -> "COM3"
                var com = m.Value.Trim('(', ')');
                if (!map.ContainsKey(com))
                    map[com] = name; // "USB Serial Device (COM3)" 等
            }

            return map;
        }

        static SerialPortItem[] BuildPortList()
        {
            var ports = SerialPort.GetPortNames()
                                  .OrderBy(x => x, StringComparer.OrdinalIgnoreCase)
                                  .ToArray();

            var captionMap = GetComCaptionMapByPnP();

            return ports.Select(com =>
                new SerialPortItem
                {
                    Port = com,
                    Caption = captionMap.TryGetValue(com, out var cap) ? cap : com
                })
                .ToArray();
        }
    }
    class SerialPortItem
    {
        public string Caption { get; set; }
        public string Port { get; set; }
    }

}
