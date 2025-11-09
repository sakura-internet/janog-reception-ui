using bpac;
using Microsoft.VisualBasic.Logging;
using System.IO;

namespace janog_reception_ui
{
    public partial class ReceptionForm : Form
    {

        bpac.Document labelDocument;

        public ReceptionForm()
        {
            InitializeComponent();

            // Load Label Template
            string exeDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            labelDocument = new bpac.Document();
            if (!labelDocument.Open(exeDirPath + "\\" + "label.lbx"))
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
                img = (Image)tmp.Clone();  // ÉÅÉÇÉäè„Ç…ÉRÉsÅ[
                tmp.Dispose();
            }

            previewBox.Image = img;
        }
    }
}
