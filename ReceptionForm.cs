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

        private void idBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void execButton_Click(object sender, EventArgs e)
        {
            Participant? participant;
            try {
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
    }
}
