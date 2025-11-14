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
            groupBox1 = new GroupBox();
            previewBox = new PictureBox();
            idBox = new TextBox();
            groupBox2 = new GroupBox();
            radioStaff = new RadioButton();
            radioDay3 = new RadioButton();
            radioDay2 = new RadioButton();
            radioDay1 = new RadioButton();
            execButton = new Button();
            label1 = new Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewBox).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(previewBox);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(372, 238);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "印刷プレビュー";
            // 
            // previewBox
            // 
            previewBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            previewBox.Location = new Point(6, 22);
            previewBox.Name = "previewBox";
            previewBox.Size = new Size(360, 210);
            previewBox.SizeMode = PictureBoxSizeMode.Zoom;
            previewBox.TabIndex = 0;
            previewBox.TabStop = false;
            // 
            // idBox
            // 
            idBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            idBox.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            idBox.Location = new Point(49, 19);
            idBox.Name = "idBox";
            idBox.Size = new Size(442, 39);
            idBox.TabIndex = 1;
            idBox.TextChanged += idBox_TextChanged;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(radioStaff);
            groupBox2.Controls.Add(radioDay3);
            groupBox2.Controls.Add(radioDay2);
            groupBox2.Controls.Add(radioDay1);
            groupBox2.Controls.Add(execButton);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(idBox);
            groupBox2.Location = new Point(390, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(497, 238);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "入力";
            // 
            // radioStaff
            // 
            radioStaff.AutoSize = true;
            radioStaff.Location = new Point(177, 100);
            radioStaff.Name = "radioStaff";
            radioStaff.Size = new Size(48, 19);
            radioStaff.TabIndex = 6;
            radioStaff.Text = "Staff";
            radioStaff.UseVisualStyleBackColor = true;
            radioStaff.CheckedChanged += radioStaff_CheckedChanged;
            // 
            // radioDay3
            // 
            radioDay3.AutoSize = true;
            radioDay3.Location = new Point(120, 100);
            radioDay3.Name = "radioDay3";
            radioDay3.Size = new Size(51, 19);
            radioDay3.TabIndex = 5;
            radioDay3.Text = "Day3";
            radioDay3.UseVisualStyleBackColor = true;
            radioDay3.CheckedChanged += radioDay3_CheckedChanged;
            // 
            // radioDay2
            // 
            radioDay2.AutoSize = true;
            radioDay2.Location = new Point(63, 100);
            radioDay2.Name = "radioDay2";
            radioDay2.Size = new Size(51, 19);
            radioDay2.TabIndex = 4;
            radioDay2.Text = "Day2";
            radioDay2.UseVisualStyleBackColor = true;
            radioDay2.CheckedChanged += radioDay2_CheckedChanged;
            // 
            // radioDay1
            // 
            radioDay1.AutoSize = true;
            radioDay1.Checked = true;
            radioDay1.Location = new Point(6, 100);
            radioDay1.Name = "radioDay1";
            radioDay1.Size = new Size(51, 19);
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
            execButton.Location = new Point(401, 64);
            execButton.Name = "execButton";
            execButton.Size = new Size(90, 41);
            execButton.TabIndex = 3;
            execButton.Text = "実行";
            execButton.UseVisualStyleBackColor = true;
            execButton.Click += execButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(6, 22);
            label1.Name = "label1";
            label1.Size = new Size(37, 32);
            label1.TabIndex = 2;
            label1.Text = "ID";
            // 
            // ReceptionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(899, 320);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "ReceptionForm";
            Text = "JANOG Reception";
            Load += ReceptionForm_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)previewBox).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
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
    }
}
