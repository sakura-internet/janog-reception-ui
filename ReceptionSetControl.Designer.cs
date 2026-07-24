namespace janog_reception_ui
{
    partial class ReceptionSetControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Shutdown();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            previewGroupBox = new GroupBox();
            printButton = new Button();
            previewBox = new PictureBox();
            inputGroupBox = new GroupBox();
            mediaLabel = new Label();
            mediaBox = new TextBox();
            clearButton = new Button();
            execButton = new Button();
            idLabel = new Label();
            idBox = new TextBox();
            statusGroupBox = new GroupBox();
            lastTransmissionLabel = new Label();
            gateLabel = new Label();
            printerLabel = new Label();
            readerLabel = new Label();
            errorLabel = new Label();
            reconnectTimer = new System.Windows.Forms.Timer(components);
            heartbeatTimer = new System.Windows.Forms.Timer(components);
            previewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewBox).BeginInit();
            inputGroupBox.SuspendLayout();
            statusGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // previewGroupBox
            // 
            previewGroupBox.Controls.Add(printButton);
            previewGroupBox.Controls.Add(previewBox);
            previewGroupBox.Location = new Point(8, 8);
            previewGroupBox.Name = "previewGroupBox";
            previewGroupBox.Size = new Size(531, 408);
            previewGroupBox.TabIndex = 0;
            previewGroupBox.TabStop = false;
            previewGroupBox.Text = "印刷プレビュー";
            // 
            // printButton
            // 
            printButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            printButton.Enabled = false;
            printButton.Location = new Point(416, 359);
            printButton.Name = "printButton";
            printButton.Size = new Size(107, 38);
            printButton.TabIndex = 1;
            printButton.Text = "再印刷";
            printButton.UseVisualStyleBackColor = true;
            printButton.Click += printButton_Click;
            // 
            // previewBox
            // 
            previewBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            previewBox.Location = new Point(9, 37);
            previewBox.Name = "previewBox";
            previewBox.Size = new Size(514, 313);
            previewBox.SizeMode = PictureBoxSizeMode.Zoom;
            previewBox.TabIndex = 0;
            previewBox.TabStop = false;
            // 
            // inputGroupBox
            // 
            inputGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inputGroupBox.Controls.Add(mediaLabel);
            inputGroupBox.Controls.Add(mediaBox);
            inputGroupBox.Controls.Add(clearButton);
            inputGroupBox.Controls.Add(execButton);
            inputGroupBox.Controls.Add(idLabel);
            inputGroupBox.Controls.Add(idBox);
            inputGroupBox.Location = new Point(548, 8);
            inputGroupBox.Name = "inputGroupBox";
            inputGroupBox.Size = new Size(706, 277);
            inputGroupBox.TabIndex = 1;
            inputGroupBox.TabStop = false;
            inputGroupBox.Text = "入力";
            // 
            // mediaLabel
            // 
            mediaLabel.AutoSize = true;
            mediaLabel.Font = new Font("Yu Gothic UI", 18F);
            mediaLabel.Location = new Point(9, 112);
            mediaLabel.Name = "mediaLabel";
            mediaLabel.Size = new Size(119, 48);
            mediaLabel.TabIndex = 8;
            mediaLabel.Text = "Media";
            // 
            // mediaBox
            // 
            mediaBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mediaBox.Font = new Font("Yu Gothic UI", 18F);
            mediaBox.Location = new Point(133, 107);
            mediaBox.Name = "mediaBox";
            mediaBox.Size = new Size(563, 55);
            mediaBox.TabIndex = 7;
            // 
            // clearButton
            // 
            clearButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            clearButton.Font = new Font("Yu Gothic UI", 18F);
            clearButton.Location = new Point(429, 193);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(129, 68);
            clearButton.TabIndex = 3;
            clearButton.Text = "クリア";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // execButton
            // 
            execButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            execButton.Enabled = false;
            execButton.Font = new Font("Yu Gothic UI", 18F);
            execButton.Location = new Point(567, 193);
            execButton.Name = "execButton";
            execButton.Size = new Size(129, 68);
            execButton.TabIndex = 4;
            execButton.Text = "実行";
            execButton.UseVisualStyleBackColor = true;
            execButton.Click += execButton_Click;
            // 
            // idLabel
            // 
            idLabel.AutoSize = true;
            idLabel.Font = new Font("Yu Gothic UI", 18F);
            idLabel.Location = new Point(9, 37);
            idLabel.Name = "idLabel";
            idLabel.Size = new Size(55, 48);
            idLabel.TabIndex = 2;
            idLabel.Text = "ID";
            // 
            // idBox
            // 
            idBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            idBox.Font = new Font("Yu Gothic UI", 18F);
            idBox.Location = new Point(133, 32);
            idBox.Name = "idBox";
            idBox.Size = new Size(563, 55);
            idBox.TabIndex = 1;
            idBox.TextChanged += idBox_TextChanged;
            // 
            // statusGroupBox
            // 
            statusGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusGroupBox.Controls.Add(lastTransmissionLabel);
            statusGroupBox.Controls.Add(gateLabel);
            statusGroupBox.Controls.Add(printerLabel);
            statusGroupBox.Controls.Add(readerLabel);
            statusGroupBox.Controls.Add(errorLabel);
            statusGroupBox.Location = new Point(545, 291);
            statusGroupBox.Name = "statusGroupBox";
            statusGroupBox.Size = new Size(706, 125);
            statusGroupBox.TabIndex = 2;
            statusGroupBox.TabStop = false;
            statusGroupBox.Text = "状態";
            // 
            // lastTransmissionLabel
            // 
            lastTransmissionLabel.AutoEllipsis = true;
            lastTransmissionLabel.Location = new Point(12, 65);
            lastTransmissionLabel.Name = "lastTransmissionLabel";
            lastTransmissionLabel.Size = new Size(680, 25);
            lastTransmissionLabel.TabIndex = 4;
            lastTransmissionLabel.Text = "最終送信: なし";
            // 
            // gateLabel
            // 
            gateLabel.AutoEllipsis = true;
            gateLabel.Location = new Point(12, 29);
            gateLabel.Name = "gateLabel";
            gateLabel.Size = new Size(180, 25);
            gateLabel.TabIndex = 0;
            gateLabel.Text = "ゲート";
            // 
            // printerLabel
            // 
            printerLabel.AutoEllipsis = true;
            printerLabel.Location = new Point(198, 29);
            printerLabel.Name = "printerLabel";
            printerLabel.Size = new Size(286, 25);
            printerLabel.TabIndex = 1;
            printerLabel.Text = "プリンタ";
            // 
            // readerLabel
            // 
            readerLabel.AutoEllipsis = true;
            readerLabel.Location = new Point(490, 29);
            readerLabel.Name = "readerLabel";
            readerLabel.Size = new Size(202, 25);
            readerLabel.TabIndex = 2;
            readerLabel.Text = "COM";
            // 
            // errorLabel
            // 
            errorLabel.AutoEllipsis = true;
            errorLabel.ForeColor = Color.Red;
            errorLabel.Location = new Point(12, 94);
            errorLabel.Name = "errorLabel";
            errorLabel.Size = new Size(680, 25);
            errorLabel.TabIndex = 5;
            errorLabel.Text = "error";
            // 
            // reconnectTimer
            // 
            reconnectTimer.Interval = 1000;
            reconnectTimer.Tick += reconnectTimer_Tick;
            // 
            // heartbeatTimer
            // 
            heartbeatTimer.Interval = 60000;
            heartbeatTimer.Tick += heartbeatTimer_Tick;
            // 
            // ReceptionSetControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(statusGroupBox);
            Controls.Add(inputGroupBox);
            Controls.Add(previewGroupBox);
            Margin = new Padding(3, 3, 3, 10);
            MinimumSize = new Size(1000, 430);
            Name = "ReceptionSetControl";
            Size = new Size(1262, 430);
            previewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)previewBox).EndInit();
            inputGroupBox.ResumeLayout(false);
            inputGroupBox.PerformLayout();
            statusGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox previewGroupBox;
        private Button printButton;
        private PictureBox previewBox;
        private GroupBox inputGroupBox;
        private Label mediaLabel;
        private TextBox mediaBox;
        private Button clearButton;
        private Button execButton;
        private Label idLabel;
        private TextBox idBox;
        private GroupBox statusGroupBox;
        private Label lastTransmissionLabel;
        private Label gateLabel;
        private Label printerLabel;
        private Label readerLabel;
        private Label errorLabel;
        private System.Windows.Forms.Timer reconnectTimer;
        private System.Windows.Forms.Timer heartbeatTimer;
    }
}
