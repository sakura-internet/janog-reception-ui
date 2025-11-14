using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace janog_reception_ui
{
    public partial class EnvConfigForm : Form
    {
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
            config.Environment.Develop.BaseUrl = textBoxDevBaseUrl.Text;
            config.Environment.Develop.Username = textBoxDevUsername.Text;
            config.Environment.Develop.Password = textBoxDevPassword.Text;
            config.Environment.Production.BaseUrl = textBoxProdBaseUrl.Text;
            config.Environment.Production.Username = textBoxProdUsername.Text;
            config.Environment.Production.Password = textBoxProdPassword.Text;
        }
    }
}
