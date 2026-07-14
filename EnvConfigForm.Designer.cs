namespace janog_reception_ui
{
    partial class EnvConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            radioEnvProduction = new RadioButton();
            radioEnvDevelop = new RadioButton();
            groupBox2 = new GroupBox();
            textBoxDevUsername = new TextBox();
            label3 = new Label();
            textBoxDevPassword = new TextBox();
            label2 = new Label();
            textBoxDevBaseUrl = new TextBox();
            label1 = new Label();
            groupBox3 = new GroupBox();
            textBoxProdPassword = new TextBox();
            textBoxProdUsername = new TextBox();
            label4 = new Label();
            label5 = new Label();
            textBoxProdBaseUrl = new TextBox();
            label6 = new Label();
            groupBox4 = new GroupBox();
            audioEnabledCheckBox = new CheckBox();
            readerComboBox = new ComboBox();
            label9 = new Label();
            label8 = new Label();
            printerComboBox = new ComboBox();
            textBoxGate = new TextBox();
            label7 = new Label();
            playSoundButton = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(radioEnvProduction);
            groupBox1.Controls.Add(radioEnvDevelop);
            groupBox1.Location = new Point(17, 298);
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(766, 88);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "利用する環境";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // radioEnvProduction
            // 
            radioEnvProduction.AutoSize = true;
            radioEnvProduction.Location = new Point(121, 37);
            radioEnvProduction.Margin = new Padding(4, 5, 4, 5);
            radioEnvProduction.Name = "radioEnvProduction";
            radioEnvProduction.Size = new Size(109, 29);
            radioEnvProduction.TabIndex = 1;
            radioEnvProduction.TabStop = true;
            radioEnvProduction.Text = "本番環境";
            radioEnvProduction.UseVisualStyleBackColor = true;
            // 
            // radioEnvDevelop
            // 
            radioEnvDevelop.AutoSize = true;
            radioEnvDevelop.Location = new Point(9, 37);
            radioEnvDevelop.Margin = new Padding(4, 5, 4, 5);
            radioEnvDevelop.Name = "radioEnvDevelop";
            radioEnvDevelop.Size = new Size(109, 29);
            radioEnvDevelop.TabIndex = 1;
            radioEnvDevelop.TabStop = true;
            radioEnvDevelop.Text = "開発環境";
            radioEnvDevelop.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(textBoxDevUsername);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(textBoxDevPassword);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textBoxDevBaseUrl);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(17, 397);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new Size(766, 195);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "開発環境";
            // 
            // textBoxDevUsername
            // 
            textBoxDevUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDevUsername.Location = new Point(101, 85);
            textBoxDevUsername.Margin = new Padding(4, 5, 4, 5);
            textBoxDevUsername.Name = "textBoxDevUsername";
            textBoxDevUsername.Size = new Size(654, 31);
            textBoxDevUsername.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 138);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(79, 25);
            label3.TabIndex = 6;
            label3.Text = "パスワード";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // textBoxDevPassword
            // 
            textBoxDevPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDevPassword.Location = new Point(101, 133);
            textBoxDevPassword.Margin = new Padding(4, 5, 4, 5);
            textBoxDevPassword.Name = "textBoxDevPassword";
            textBoxDevPassword.PasswordChar = '*';
            textBoxDevPassword.Size = new Size(654, 31);
            textBoxDevPassword.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 90);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(84, 25);
            label2.TabIndex = 4;
            label2.Text = "ユーザー名";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // textBoxDevBaseUrl
            // 
            textBoxDevBaseUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDevBaseUrl.Location = new Point(101, 37);
            textBoxDevBaseUrl.Margin = new Padding(4, 5, 4, 5);
            textBoxDevBaseUrl.Name = "textBoxDevBaseUrl";
            textBoxDevBaseUrl.Size = new Size(654, 31);
            textBoxDevBaseUrl.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 42);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(85, 25);
            label1.TabIndex = 2;
            label1.Text = "サーバURL";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(textBoxProdPassword);
            groupBox3.Controls.Add(textBoxProdUsername);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(textBoxProdBaseUrl);
            groupBox3.Controls.Add(label6);
            groupBox3.Location = new Point(17, 602);
            groupBox3.Margin = new Padding(4, 5, 4, 5);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 5, 4, 5);
            groupBox3.Size = new Size(766, 195);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "本番環境";
            // 
            // textBoxProdPassword
            // 
            textBoxProdPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxProdPassword.Location = new Point(101, 133);
            textBoxProdPassword.Margin = new Padding(4, 5, 4, 5);
            textBoxProdPassword.Name = "textBoxProdPassword";
            textBoxProdPassword.PasswordChar = '*';
            textBoxProdPassword.Size = new Size(654, 31);
            textBoxProdPassword.TabIndex = 8;
            // 
            // textBoxProdUsername
            // 
            textBoxProdUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxProdUsername.Location = new Point(101, 85);
            textBoxProdUsername.Margin = new Padding(4, 5, 4, 5);
            textBoxProdUsername.Name = "textBoxProdUsername";
            textBoxProdUsername.Size = new Size(654, 31);
            textBoxProdUsername.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 138);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(79, 25);
            label4.TabIndex = 6;
            label4.Text = "パスワード";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 90);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(84, 25);
            label5.TabIndex = 4;
            label5.Text = "ユーザー名";
            label5.TextAlign = ContentAlignment.TopRight;
            // 
            // textBoxProdBaseUrl
            // 
            textBoxProdBaseUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxProdBaseUrl.Location = new Point(101, 37);
            textBoxProdBaseUrl.Margin = new Padding(4, 5, 4, 5);
            textBoxProdBaseUrl.Name = "textBoxProdBaseUrl";
            textBoxProdBaseUrl.Size = new Size(654, 31);
            textBoxProdBaseUrl.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 42);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(85, 25);
            label6.TabIndex = 2;
            label6.Text = "サーバURL";
            label6.TextAlign = ContentAlignment.TopRight;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(playSoundButton);
            groupBox4.Controls.Add(audioEnabledCheckBox);
            groupBox4.Controls.Add(readerComboBox);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(printerComboBox);
            groupBox4.Controls.Add(textBoxGate);
            groupBox4.Controls.Add(label7);
            groupBox4.Location = new Point(17, 20);
            groupBox4.Margin = new Padding(4, 5, 4, 5);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4, 5, 4, 5);
            groupBox4.Size = new Size(766, 228);
            groupBox4.TabIndex = 8;
            groupBox4.TabStop = false;
            groupBox4.Text = "基本設定";
            // 
            // audioEnabledCheckBox
            // 
            audioEnabledCheckBox.AutoSize = true;
            audioEnabledCheckBox.Location = new Point(101, 180);
            audioEnabledCheckBox.Name = "audioEnabledCheckBox";
            audioEnabledCheckBox.Size = new Size(110, 29);
            audioEnabledCheckBox.TabIndex = 10;
            audioEnabledCheckBox.Text = "音声再生";
            audioEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // readerComboBox
            // 
            readerComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            readerComboBox.FormattingEnabled = true;
            readerComboBox.Location = new Point(101, 128);
            readerComboBox.Margin = new Padding(4, 5, 4, 5);
            readerComboBox.Name = "readerComboBox";
            readerComboBox.Size = new Size(654, 33);
            readerComboBox.TabIndex = 8;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(36, 133);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(63, 25);
            label9.TabIndex = 7;
            label9.Text = "リーダー";
            label9.TextAlign = ContentAlignment.TopRight;
            label9.Click += label9_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(33, 85);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(65, 25);
            label8.TabIndex = 6;
            label8.Text = "プリンタ";
            label8.TextAlign = ContentAlignment.TopRight;
            // 
            // printerComboBox
            // 
            printerComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            printerComboBox.FormattingEnabled = true;
            printerComboBox.Location = new Point(101, 80);
            printerComboBox.Margin = new Padding(4, 5, 4, 5);
            printerComboBox.Name = "printerComboBox";
            printerComboBox.Size = new Size(654, 33);
            printerComboBox.TabIndex = 5;
            // 
            // textBoxGate
            // 
            textBoxGate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxGate.Location = new Point(101, 32);
            textBoxGate.Margin = new Padding(4, 5, 4, 5);
            textBoxGate.Name = "textBoxGate";
            textBoxGate.Size = new Size(153, 31);
            textBoxGate.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(46, 37);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(50, 25);
            label7.TabIndex = 4;
            label7.Text = "ゲート";
            label7.TextAlign = ContentAlignment.TopRight;
            // 
            // playSoundButton
            // 
            playSoundButton.Location = new Point(217, 176);
            playSoundButton.Name = "playSoundButton";
            playSoundButton.Size = new Size(112, 34);
            playSoundButton.TabIndex = 11;
            playSoundButton.Text = "テスト再生";
            playSoundButton.UseVisualStyleBackColor = true;
            playSoundButton.Click += playSoundButton_Click;
            // 
            // EnvConfigForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 828);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 5, 4, 5);
            Name = "EnvConfigForm";
            Text = "環境設定";
            FormClosing += EnvConfigForm_FormClosing;
            Load += EnvConfigForm_Load;
            Shown += EnvConfigForm_Shown;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton radioEnvDevelop;
        private RadioButton radioEnvProduction;
        private GroupBox groupBox2;
        private Label label1;
        private TextBox textBoxDevBaseUrl;
        private TextBox textBoxDevUsername;
        private Label label3;
        private Label label2;
        private TextBox textBoxDevPassword;
        private GroupBox groupBox3;
        private TextBox textBoxProdUsername;
        private Label label4;
        private Label label5;
        private TextBox textBoxProdBaseUrl;
        private Label label6;
        private TextBox textBoxProdPassword;
        private GroupBox groupBox4;
        private TextBox textBoxGate;
        private Label label7;
        private Label label8;
        private ComboBox printerComboBox;
        private Label label9;
        private ComboBox readerComboBox;
        private CheckBox audioEnabledCheckBox;
        private Button playSoundButton;
    }
}
