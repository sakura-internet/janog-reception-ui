using bpac;
using Microsoft.VisualBasic.Logging;
using System.Net.WebSockets;

namespace janog_reception_ui
{
    public partial class ReceptionForm : Form
    {

        bpac.Document labelDocument;
        private Config _config;

        public ReceptionForm()
        {
            InitializeComponent();

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

            SetLabelField("program","");
            SetLabelField("full_name", participant.FullName);
            SetLabelField("organization", participant.Organization);
            UpdatePreview();
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
    }
}
