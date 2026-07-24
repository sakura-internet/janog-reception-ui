namespace janog_reception_ui
{
    public partial class ReceptionForm : Form
    {
        private readonly List<ReceptionSetControl> _setControls = new();
        private Config _config;
        private EnvConfigForm? _activeEnvConfigForm;

        public ReceptionForm()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                _config = new Config();
                return;
            }
            _config = Config.LoadYAML();
            ApplyConfig();
            statusAgeTimer.Start();
        }

        public static string TryExtractUlid(string input)
        {
            return ReceptionSetControl.TryExtractUlid(input);
        }

        private void ApplyConfig()
        {
            UpdateEnvironmentStatus();
            RebuildReceptionSets();
        }

        private void UpdateEnvironmentStatus()
        {
            if (_config.Environment.Environment == EnvironmentKind.Production)
            {
                toolStripEnvLabel.Text = "本番環境";
                toolStripEnvLabel.BackColor = Color.DodgerBlue;
            }
            else
            {
                toolStripEnvLabel.Text = "開発環境";
                toolStripEnvLabel.BackColor = Color.Red;
            }
        }

        private void RebuildReceptionSets()
        {
            receptionFlowPanel.SuspendLayout();
            try
            {
                foreach (var control in _setControls)
                {
                    control.Shutdown();
                    receptionFlowPanel.Controls.Remove(control);
                    control.Dispose();
                }
                _setControls.Clear();

                _config.Normalize();
                foreach (var setConfig in _config.ReceptionSets)
                {
                    var control = new ReceptionSetControl();
                    control.Initialize(
                        _config,
                        setConfig,
                        TryRouteConfigQr,
                        () => _activeEnvConfigForm != null);
                    _setControls.Add(control);
                    receptionFlowPanel.Controls.Add(control);
                }
                ResizeSetControls();
            }
            finally
            {
                receptionFlowPanel.ResumeLayout(true);
            }
        }

        private bool TryRouteConfigQr(string rawValue)
        {
            var configForm = _activeEnvConfigForm;
            if (configForm == null || configForm.IsDisposed) return false;

            try
            {
                BeginInvoke(new Action(() =>
                {
                    if (ReferenceEquals(_activeEnvConfigForm, configForm) && !configForm.IsDisposed)
                    {
                        configForm.HandleQrScan(rawValue);
                    }
                }));
                return true;
            }
            catch (ObjectDisposedException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private void ResizeSetControls()
        {
            var width = Math.Max(
                1000,
                receptionFlowPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 8);
            foreach (var control in _setControls)
            {
                control.Width = width;
            }
        }

        private void receptionFlowPanel_Resize(object sender, EventArgs e)
        {
            ResizeSetControls();
        }

        private void statusAgeTimer_Tick(object sender, EventArgs e)
        {
            foreach (var control in _setControls)
            {
                control.RefreshLastTransmissionLabel();
            }
        }

        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var form = new EnvConfigForm { config = _config };
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
            }

            _config.SaveYAML();
            ApplyConfig();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            statusAgeTimer.Stop();
            foreach (var control in _setControls)
            {
                control.Shutdown();
            }
            base.OnFormClosed(e);
        }
    }
}
