using System.Diagnostics;

namespace janog_reception_ui
{
    public partial class ReceptionForm
    {
        private static readonly TimeSpan HeartbeatInterval = TimeSpan.FromMinutes(1);
        private readonly System.Windows.Forms.Timer _heartbeatTimer = new();
        private bool _heartbeatInProgress;
        private bool _heartbeatStarted;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_heartbeatStarted)
            {
                return;
            }
            _heartbeatStarted = true;

            _heartbeatTimer.Interval = (int)HeartbeatInterval.TotalMilliseconds;
            _heartbeatTimer.Tick += HeartbeatTimer_Tick;
            Client.TerminalStatusReported += ResetHeartbeatTimer;
            FormClosed += ReceptionForm_HeartbeatFormClosed;
            _heartbeatTimer.Start();
        }

        private void ResetHeartbeatTimer()
        {
            if (InvokeRequired)
            {
                BeginInvoke(ResetHeartbeatTimer);
                return;
            }

            _heartbeatTimer.Stop();
            _heartbeatTimer.Start();
        }

        private async void HeartbeatTimer_Tick(object? sender, EventArgs e)
        {
            if (_heartbeatInProgress || Volatile.Read(ref _isReceptionProcessing) != 0)
            {
                return;
            }

            _heartbeatInProgress = true;
            try
            {
                var auth = _config.Auth();
                var client = new Client(auth.BaseUrl, auth.Username, auth.Password);
                await client.ReportTerminalStatusHeartbeatAsync(_config.Gate);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Terminal heartbeat failed: {ex.Message}");
            }
            finally
            {
                _heartbeatInProgress = false;
            }
        }

        private void ReceptionForm_HeartbeatFormClosed(object? sender, FormClosedEventArgs e)
        {
            _heartbeatTimer.Stop();
            _heartbeatTimer.Tick -= HeartbeatTimer_Tick;
            Client.TerminalStatusReported -= ResetHeartbeatTimer;
            _heartbeatTimer.Dispose();
        }
    }
}
