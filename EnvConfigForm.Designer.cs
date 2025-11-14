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
            label2 = new Label();
            textBoxDevBaseUrl = new TextBox();
            label1 = new Label();
            textBoxDevPassword = new TextBox();
            groupBox3 = new GroupBox();
            textBoxProdPassword = new TextBox();
            textBoxProdUsername = new TextBox();
            label4 = new Label();
            label5 = new Label();
            textBoxProdBaseUrl = new TextBox();
            label6 = new Label();
            groupBox4 = new GroupBox();
            textBoxGate = new TextBox();
            label7 = new Label();
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
            groupBox1.Location = new Point(12, 71);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(536, 53);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "利用する環境";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // radioEnvProduction
            // 
            radioEnvProduction.AutoSize = true;
            radioEnvProduction.Location = new Point(85, 22);
            radioEnvProduction.Name = "radioEnvProduction";
            radioEnvProduction.Size = new Size(73, 19);
            radioEnvProduction.TabIndex = 1;
            radioEnvProduction.TabStop = true;
            radioEnvProduction.Text = "本番環境";
            radioEnvProduction.UseVisualStyleBackColor = true;
            // 
            // radioEnvDevelop
            // 
            radioEnvDevelop.AutoSize = true;
            radioEnvDevelop.Location = new Point(6, 22);
            radioEnvDevelop.Name = "radioEnvDevelop";
            radioEnvDevelop.Size = new Size(73, 19);
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
            groupBox2.Location = new Point(12, 130);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(536, 117);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "開発環境";
            // 
            // textBoxDevUsername
            // 
            textBoxDevUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDevUsername.Location = new Point(71, 51);
            textBoxDevUsername.Name = "textBoxDevUsername";
            textBoxDevUsername.Size = new Size(459, 23);
            textBoxDevUsername.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 83);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 6;
            label3.Text = "パスワード";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 54);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 4;
            label2.Text = "ユーザー名";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // textBoxDevBaseUrl
            // 
            textBoxDevBaseUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDevBaseUrl.Location = new Point(71, 22);
            textBoxDevBaseUrl.Name = "textBoxDevBaseUrl";
            textBoxDevBaseUrl.Size = new Size(459, 23);
            textBoxDevBaseUrl.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 25);
            label1.Name = "label1";
            label1.Size = new Size(56, 15);
            label1.TabIndex = 2;
            label1.Text = "サーバURL";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // textBoxDevPassword
            // 
            textBoxDevPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDevPassword.Location = new Point(71, 80);
            textBoxDevPassword.Name = "textBoxDevPassword";
            textBoxDevPassword.PasswordChar = '*';
            textBoxDevPassword.Size = new Size(459, 23);
            textBoxDevPassword.TabIndex = 5;
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
            groupBox3.Location = new Point(12, 253);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(536, 117);
            groupBox3.TabIndex = 7;
            groupBox3.TabStop = false;
            groupBox3.Text = "本番環境";
            // 
            // textBoxProdPassword
            // 
            textBoxProdPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxProdPassword.Location = new Point(71, 80);
            textBoxProdPassword.Name = "textBoxProdPassword";
            textBoxProdPassword.PasswordChar = '*';
            textBoxProdPassword.Size = new Size(459, 23);
            textBoxProdPassword.TabIndex = 8;
            // 
            // textBoxProdUsername
            // 
            textBoxProdUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxProdUsername.Location = new Point(71, 51);
            textBoxProdUsername.Name = "textBoxProdUsername";
            textBoxProdUsername.Size = new Size(459, 23);
            textBoxProdUsername.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 83);
            label4.Name = "label4";
            label4.Size = new Size(51, 15);
            label4.TabIndex = 6;
            label4.Text = "パスワード";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 54);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 4;
            label5.Text = "ユーザー名";
            label5.TextAlign = ContentAlignment.TopRight;
            // 
            // textBoxProdBaseUrl
            // 
            textBoxProdBaseUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxProdBaseUrl.Location = new Point(71, 22);
            textBoxProdBaseUrl.Name = "textBoxProdBaseUrl";
            textBoxProdBaseUrl.Size = new Size(459, 23);
            textBoxProdBaseUrl.TabIndex = 2;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(9, 25);
            label6.Name = "label6";
            label6.Size = new Size(56, 15);
            label6.TabIndex = 2;
            label6.Text = "サーバURL";
            label6.TextAlign = ContentAlignment.TopRight;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBoxGate);
            groupBox4.Controls.Add(label7);
            groupBox4.Location = new Point(12, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(536, 53);
            groupBox4.TabIndex = 8;
            groupBox4.TabStop = false;
            groupBox4.Text = "基本設定";
            // 
            // textBoxGate
            // 
            textBoxGate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxGate.Location = new Point(71, 19);
            textBoxGate.Name = "textBoxGate";
            textBoxGate.Size = new Size(108, 23);
            textBoxGate.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(32, 22);
            label7.Name = "label7";
            label7.Size = new Size(33, 15);
            label7.TabIndex = 4;
            label7.Text = "ゲート";
            label7.TextAlign = ContentAlignment.TopRight;
            // 
            // EnvConfigForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 381);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
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
    }
}