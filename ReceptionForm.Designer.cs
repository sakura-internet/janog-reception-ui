namespace janog_reception_ui
{
    partial class ReceptionForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            contextMenuStrip = new ContextMenuStrip(components);
            ConfigToolStripMenuItem = new ToolStripMenuItem();
            receptionFlowPanel = new FlowLayoutPanel();
            environmentStatusStrip = new StatusStrip();
            environmentSpringLabel = new ToolStripStatusLabel();
            toolStripEnvLabel = new ToolStripStatusLabel();
            statusAgeTimer = new System.Windows.Forms.Timer(components);
            contextMenuStrip.SuspendLayout();
            environmentStatusStrip.SuspendLayout();
            SuspendLayout();
            //
            // contextMenuStrip
            //
            contextMenuStrip.ImageScalingSize = new Size(24, 24);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { ConfigToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(157, 36);
            //
            // ConfigToolStripMenuItem
            //
            ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            ConfigToolStripMenuItem.Size = new Size(156, 32);
            ConfigToolStripMenuItem.Text = "環境設定";
            ConfigToolStripMenuItem.Click += ConfigToolStripMenuItem_Click;
            //
            // receptionFlowPanel
            //
            receptionFlowPanel.AutoScroll = true;
            receptionFlowPanel.Dock = DockStyle.Fill;
            receptionFlowPanel.FlowDirection = FlowDirection.TopDown;
            receptionFlowPanel.Location = new Point(0, 0);
            receptionFlowPanel.Name = "receptionFlowPanel";
            receptionFlowPanel.Padding = new Padding(6);
            receptionFlowPanel.Size = new Size(1280, 868);
            receptionFlowPanel.TabIndex = 0;
            receptionFlowPanel.WrapContents = false;
            receptionFlowPanel.Resize += receptionFlowPanel_Resize;
            //
            // environmentStatusStrip
            //
            environmentStatusStrip.ImageScalingSize = new Size(24, 24);
            environmentStatusStrip.Items.AddRange(new ToolStripItem[] { environmentSpringLabel, toolStripEnvLabel });
            environmentStatusStrip.Location = new Point(0, 868);
            environmentStatusStrip.Name = "environmentStatusStrip";
            environmentStatusStrip.Size = new Size(1280, 32);
            environmentStatusStrip.TabIndex = 1;
            //
            // environmentSpringLabel
            //
            environmentSpringLabel.Name = "environmentSpringLabel";
            environmentSpringLabel.Size = new Size(1109, 25);
            environmentSpringLabel.Spring = true;
            //
            // toolStripEnvLabel
            //
            toolStripEnvLabel.Name = "toolStripEnvLabel";
            toolStripEnvLabel.Size = new Size(150, 25);
            toolStripEnvLabel.Text = "toolStripEnvLabel";
            //
            // statusAgeTimer
            //
            statusAgeTimer.Interval = 5000;
            statusAgeTimer.Tick += statusAgeTimer_Tick;
            //
            // ReceptionForm
            //
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 900);
            ContextMenuStrip = contextMenuStrip;
            Controls.Add(receptionFlowPanel);
            Controls.Add(environmentStatusStrip);
            MinimumSize = new Size(1040, 520);
            Name = "ReceptionForm";
            Text = "JANOG Reception";
            contextMenuStrip.ResumeLayout(false);
            environmentStatusStrip.ResumeLayout(false);
            environmentStatusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem ConfigToolStripMenuItem;
        private FlowLayoutPanel receptionFlowPanel;
        private StatusStrip environmentStatusStrip;
        private ToolStripStatusLabel environmentSpringLabel;
        private ToolStripStatusLabel toolStripEnvLabel;
        private System.Windows.Forms.Timer statusAgeTimer;
    }
}
