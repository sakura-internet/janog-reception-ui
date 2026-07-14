namespace janog_reception_ui
{
    partial class ReceptionForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            groupBox1 = new GroupBox();
            printButton = new Button();
            previewBox = new PictureBox();
            idBox = new TextBox();
            groupBox2 = new GroupBox();
            radioHost = new RadioButton();
            label2 = new Label();
            mediaBox = new TextBox();
            radioStaff = new RadioButton();
            radioDay3 = new RadioButton();
            radioDay2 = new RadioButton();
            radioDay1 = new RadioButton();
            execButton = new Button();
            label1 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            ConfigToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            gateLabel = new ToolStripStatusLabel();
            printerLabel = new ToolStripStatusLabel();
            readerLabel = new ToolStripStatusLabel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            errorLabel = new ToolStripStatusLabel();
            toolStripEnvLabel = new ToolStripStatusLabel();
            timer1 = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewBox).BeginInit();
            groupBox2.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(printButton);
            groupBox1.Controls.Add(previewBox);
            groupBox1.Location = new Point(17, 20);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(531, 397);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "印刷プレビュー";
            // 
            // printButton
            // 
            printButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            printButton.Location = new Point(416, 348);
            printButton.Margin = new Padding(4, 5, 4, 5);
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
            previewBox.Margin = new Padding(4, 5, 4, 5);
            previewBox.Name = "previewBox";
            previewBox.Size = new Size(514, 302);
            previewBox.SizeMode = PictureBoxSizeMode.Zoom;
            previewBox.TabIndex = 0;
            previewBox.TabStop = false;
            // 
            // idBox
            // 
            idBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            idBox.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            idBox.Location = new Point(133, 32);
            idBox.Margin = new Padding(4, 5, 4, 5);
            idBox.Name = "idBox";
            idBox.Size = new Size(563, 55);
            idBox.TabIndex = 1;
            idBox.TextChanged += idBox_TextChanged;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(radioHost);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(mediaBox);
            groupBox2.Controls.Add(radioStaff);
            groupBox2.Controls.Add(radioDay3);
            groupBox2.Controls.Add(radioDay2);
            groupBox2.Controls.Add(radioDay1);
            groupBox2.Controls.Add(execButton);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(idBox);
            groupBox2.Location = new Point(557, 20);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new Size(706, 397);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "入力";
            // 
            // radioHost
            // 
            radioHost.AutoSize = true;
            radioHost.Location = new Point(395, 207);
            radioHost.Margin = new Padding(4, 5, 4, 5);
            radioHost.Name = "radioHost";
            radioHost.Size = new Size(75, 29);
            radioHost.TabIndex = 9;
            radioHost.Text = "Host";
            radioHost.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(9, 112);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(119, 48);
            label2.TabIndex = 8;
            label2.Text = "Media";
            // 
            // mediaBox
            // 
            mediaBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mediaBox.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            mediaBox.Location = new Point(133, 107);
            mediaBox.Margin = new Padding(4, 5, 4, 5);
            mediaBox.Name = "mediaBox";
            mediaBox.Size = new Size(563, 55);
            mediaBox.TabIndex = 7;
            // 
            // radioStaff
            // 
            radioStaff.AutoSize = true;
            radioStaff.Location = new Point(314, 207);
            radioStaff.Margin = new Padding(4, 5, 4, 5);
            radioStaff.Name = "radioStaff";
            radioStaff.Size = new Size(73, 29);
            radioStaff.TabIndex = 6;
            radioStaff.Text = "Staff";
            radioStaff.UseVisualStyleBackColor = true;
            radioStaff.CheckedChanged += radioStaff_CheckedChanged;
            // 
            // radioDay3
            // 
            radioDay3.AutoSize = true;
            radioDay3.Location = new Point(233, 207);
            radioDay3.Margin = new Padding(4, 5, 4, 5);
            radioDay3.Name = "radioDay3";
            radioDay3.Size = new Size(78, 29);
            radioDay3.TabIndex = 5;
            radioDay3.Text = "Day3";
            radioDay3.UseVisualStyleBackColor = true;
            radioDay3.CheckedChanged += radioDay3_CheckedChanged;
            // 
            // radioDay2
            // 
            radioDay2.AutoSize = true;
            radioDay2.Location = new Point(151, 207);
            radioDay2.Margin = new Padding(4, 5, 4, 5);
            radioDay2.Name = "radioDay2";
            radioDay2.Size = new Size(78, 29);
            radioDay2.TabIndex = 4;
            radioDay2.Text = "Day2";
            radioDay2.UseVisualStyleBackColor = true;
            radioDay2.CheckedChanged += radioDay2_CheckedChanged;
            // 
            // radioDay1
            // 
            radioDay1.AutoSize = true;
            radioDay1.Checked = true;
            radioDay1.Location = new Point(70, 207);
            radioDay1.Margin = new Padding(4, 5, 4, 5);
            radioDay1.Name = "radioDay1";
            radioDay1.Size = new Size(78, 29);
            radioDay1.TabIndex = 3;
            radioDay1.TabStop = true;
            radioDay1.Text = "Day1";
            radioDay1.UseVisualStyleBackColor = true;
            radioDay1.CheckedChanged += radioDay1_CheckedChanged;
            // 
            // execButton
            // 
            execButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            execButton.Enabled = false;
            execButton.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            execButton.Location = new Point(569, 318);
            execButton.Margin = new Padding(4, 5, 4, 5);
            execButton.Name = "execButton";
            execButton.Size = new Size(129, 68);
            execButton.TabIndex = 3;
            execButton.Text = "実行";
            execButton.UseVisualStyleBackColor = true;
            execButton.Click += execButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(9, 37);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(55, 48);
            label1.TabIndex = 2;
            label1.Text = "ID";
            label1.Click += label1_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { ConfigToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(157, 36);
            // 
            // ConfigToolStripMenuItem
            // 
            ConfigToolStripMenuItem.Name = "ConfigToolStripMenuItem";
            ConfigToolStripMenuItem.Size = new Size(156, 32);
            ConfigToolStripMenuItem.Text = "環境設定";
            ConfigToolStripMenuItem.Click += ConfigToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { gateLabel, printerLabel, readerLabel, toolStripStatusLabel1, errorLabel, toolStripEnvLabel });
            statusStrip1.Location = new Point(0, 435);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 20, 0);
            statusStrip1.Size = new Size(1280, 32);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // gateLabel
            // 
            gateLabel.Name = "gateLabel";
            gateLabel.Size = new Size(88, 25);
            gateLabel.Text = "gateLabel";
            // 
            // printerLabel
            // 
            printerLabel.Name = "printerLabel";
            printerLabel.Size = new Size(105, 25);
            printerLabel.Text = "printerLabel";
            // 
            // readerLabel
            // 
            readerLabel.Name = "readerLabel";
            readerLabel.Size = new Size(103, 25);
            readerLabel.Text = "readerLabel";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(722, 25);
            toolStripStatusLabel1.Spring = true;
            toolStripStatusLabel1.Click += toolStripStatusLabel1_Click;
            // 
            // errorLabel
            // 
            errorLabel.Name = "errorLabel";
            errorLabel.Size = new Size(91, 25);
            errorLabel.Text = "errorLabel";
            // 
            // toolStripEnvLabel
            // 
            toolStripEnvLabel.Name = "toolStripEnvLabel";
            toolStripEnvLabel.Size = new Size(150, 25);
            toolStripEnvLabel.Text = "toolStripEnvLabel";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // ReceptionForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 467);
            ContextMenuStrip = contextMenuStrip1;
            Controls.Add(statusStrip1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "ReceptionForm";
            Text = "JANOG Reception";
            Load += ReceptionForm_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)previewBox).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private PictureBox previewBox;
        private TextBox idBox;
        private GroupBox groupBox2;
        private Label label1;
        private Button execButton;
        private RadioButton radioDay1;
        private RadioButton radioStaff;
        private RadioButton radioDay3;
        private RadioButton radioDay2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ConfigToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripEnvLabel;
        private ToolStripStatusLabel gateLabel;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Button printButton;
        private ToolStripStatusLabel printerLabel;
        private ToolStripStatusLabel readerLabel;
        private System.Windows.Forms.Timer timer1;
        private ToolStripStatusLabel errorLabel;
        private Label label2;
        private TextBox mediaBox;
        private RadioButton radioHost;
    }
}
