using bpac;
using Microsoft.VisualBasic.Logging;
using System.IO;

namespace janog_reception_ui
{
    public partial class ReceptionForm : Form
    {

        bpac.Document labelDocument;
        Client client = new Client("https://register.janog57-dev.sakuraha.jp", "username", "password");

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
        }

        private void ReceptionForm_Load(object sender, EventArgs e)
        {
            UpdatePreview();
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
            Participant? participant;
            try
            {
                participant = client.AcceptParticipant(idBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
    }
}
