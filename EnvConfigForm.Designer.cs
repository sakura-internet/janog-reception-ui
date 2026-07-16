namespace janog_reception_ui
{
    partial class EnvConfigForm
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
            receptionSetsGroupBox = new GroupBox();
            setEditorGroupBox = new GroupBox();
            readerComboBox = new ComboBox();
            readerLabel = new Label();
            printerComboBox = new ComboBox();
            printerLabel = new Label();
            textBoxGate = new TextBox();
            gateLabel = new Label();
            moveDownButton = new Button();
            moveUpButton = new Button();
            deleteSetButton = new Button();
            addSetButton = new Button();
            setListBox = new ListBox();
            environmentChoiceGroupBox = new GroupBox();
            radioEnvProduction = new RadioButton();
            radioEnvDevelop = new RadioButton();
            developGroupBox = new GroupBox();
            textBoxDevUsername = new TextBox();
            devPasswordLabel = new Label();
            textBoxDevPassword = new TextBox();
            devUsernameLabel = new Label();
            textBoxDevBaseUrl = new TextBox();
            devUrlLabel = new Label();
            productionGroupBox = new GroupBox();
            textBoxProdPassword = new TextBox();
            textBoxProdUsername = new TextBox();
            prodPasswordLabel = new Label();
            prodUsernameLabel = new Label();
            textBoxProdBaseUrl = new TextBox();
            prodUrlLabel = new Label();
            audioGroupBox = new GroupBox();
            playSoundButton = new Button();
            audioEnabledCheckBox = new CheckBox();
            receptionSetsGroupBox.SuspendLayout();
            setEditorGroupBox.SuspendLayout();
            environmentChoiceGroupBox.SuspendLayout();
            developGroupBox.SuspendLayout();
            productionGroupBox.SuspendLayout();
            audioGroupBox.SuspendLayout();
            SuspendLayout();
            //
            // receptionSetsGroupBox
            //
            receptionSetsGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            receptionSetsGroupBox.Controls.Add(setEditorGroupBox);
            receptionSetsGroupBox.Controls.Add(moveDownButton);
            receptionSetsGroupBox.Controls.Add(moveUpButton);
            receptionSetsGroupBox.Controls.Add(deleteSetButton);
            receptionSetsGroupBox.Controls.Add(addSetButton);
            receptionSetsGroupBox.Controls.Add(setListBox);
            receptionSetsGroupBox.Location = new Point(16, 15);
            receptionSetsGroupBox.Name = "receptionSetsGroupBox";
            receptionSetsGroupBox.Size = new Size(948, 275);
            receptionSetsGroupBox.TabIndex = 0;
            receptionSetsGroupBox.TabStop = false;
            receptionSetsGroupBox.Text = "受付セット";
            //
            // setEditorGroupBox
            //
            setEditorGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            setEditorGroupBox.Controls.Add(readerComboBox);
            setEditorGroupBox.Controls.Add(readerLabel);
            setEditorGroupBox.Controls.Add(printerComboBox);
            setEditorGroupBox.Controls.Add(printerLabel);
            setEditorGroupBox.Controls.Add(textBoxGate);
            setEditorGroupBox.Controls.Add(gateLabel);
            setEditorGroupBox.Location = new Point(303, 30);
            setEditorGroupBox.Name = "setEditorGroupBox";
            setEditorGroupBox.Size = new Size(630, 226);
            setEditorGroupBox.TabIndex = 5;
            setEditorGroupBox.TabStop = false;
            setEditorGroupBox.Text = "選択中のセット";
            //
            // readerComboBox
            //
            readerComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            readerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            readerComboBox.FormattingEnabled = true;
            readerComboBox.Location = new Point(108, 155);
            readerComboBox.Name = "readerComboBox";
            readerComboBox.Size = new Size(504, 33);
            readerComboBox.TabIndex = 5;
            //
            // readerLabel
            //
            readerLabel.AutoSize = true;
            readerLabel.Location = new Point(23, 160);
            readerLabel.Name = "readerLabel";
            readerLabel.Size = new Size(78, 25);
            readerLabel.TabIndex = 4;
            readerLabel.Text = "QRリーダ";
            //
            // printerComboBox
            //
            printerComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            printerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            printerComboBox.FormattingEnabled = true;
            printerComboBox.Location = new Point(108, 101);
            printerComboBox.Name = "printerComboBox";
            printerComboBox.Size = new Size(504, 33);
            printerComboBox.TabIndex = 3;
            //
            // printerLabel
            //
            printerLabel.AutoSize = true;
            printerLabel.Location = new Point(36, 106);
            printerLabel.Name = "printerLabel";
            printerLabel.Size = new Size(65, 25);
            printerLabel.TabIndex = 2;
            printerLabel.Text = "プリンタ";
            //
            // textBoxGate
            //
            textBoxGate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxGate.Location = new Point(108, 48);
            textBoxGate.Name = "textBoxGate";
            textBoxGate.Size = new Size(504, 31);
            textBoxGate.TabIndex = 1;
            //
            // gateLabel
            //
            gateLabel.AutoSize = true;
            gateLabel.Location = new Point(51, 53);
            gateLabel.Name = "gateLabel";
            gateLabel.Size = new Size(50, 25);
            gateLabel.TabIndex = 0;
            gateLabel.Text = "ゲート";
            //
            // moveDownButton
            //
            moveDownButton.Location = new Point(218, 215);
            moveDownButton.Name = "moveDownButton";
            moveDownButton.Size = new Size(68, 41);
            moveDownButton.TabIndex = 4;
            moveDownButton.Text = "下へ";
            moveDownButton.UseVisualStyleBackColor = true;
            moveDownButton.Click += moveDownButton_Click;
            //
            // moveUpButton
            //
            moveUpButton.Location = new Point(144, 215);
            moveUpButton.Name = "moveUpButton";
            moveUpButton.Size = new Size(68, 41);
            moveUpButton.TabIndex = 3;
            moveUpButton.Text = "上へ";
            moveUpButton.UseVisualStyleBackColor = true;
            moveUpButton.Click += moveUpButton_Click;
            //
            // deleteSetButton
            //
            deleteSetButton.Location = new Point(78, 215);
            deleteSetButton.Name = "deleteSetButton";
            deleteSetButton.Size = new Size(60, 41);
            deleteSetButton.TabIndex = 2;
            deleteSetButton.Text = "削除";
            deleteSetButton.UseVisualStyleBackColor = true;
            deleteSetButton.Click += deleteSetButton_Click;
            //
            // addSetButton
            //
            addSetButton.Location = new Point(12, 215);
            addSetButton.Name = "addSetButton";
            addSetButton.Size = new Size(60, 41);
            addSetButton.TabIndex = 1;
            addSetButton.Text = "追加";
            addSetButton.UseVisualStyleBackColor = true;
            addSetButton.Click += addSetButton_Click;
            //
            // setListBox
            //
            setListBox.FormattingEnabled = true;
            setListBox.ItemHeight = 25;
            setListBox.Location = new Point(12, 35);
            setListBox.Name = "setListBox";
            setListBox.Size = new Size(274, 154);
            setListBox.TabIndex = 0;
            setListBox.SelectedIndexChanged += setListBox_SelectedIndexChanged;
            //
            // environmentChoiceGroupBox
            //
            environmentChoiceGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            environmentChoiceGroupBox.Controls.Add(radioEnvProduction);
            environmentChoiceGroupBox.Controls.Add(radioEnvDevelop);
            environmentChoiceGroupBox.Location = new Point(16, 299);
            environmentChoiceGroupBox.Name = "environmentChoiceGroupBox";
            environmentChoiceGroupBox.Size = new Size(948, 73);
            environmentChoiceGroupBox.TabIndex = 1;
            environmentChoiceGroupBox.TabStop = false;
            environmentChoiceGroupBox.Text = "利用する環境";
            //
            // radioEnvProduction
            //
            radioEnvProduction.AutoSize = true;
            radioEnvProduction.Location = new Point(132, 30);
            radioEnvProduction.Name = "radioEnvProduction";
            radioEnvProduction.Size = new Size(109, 29);
            radioEnvProduction.TabIndex = 1;
            radioEnvProduction.Text = "本番環境";
            radioEnvProduction.UseVisualStyleBackColor = true;
            //
            // radioEnvDevelop
            //
            radioEnvDevelop.AutoSize = true;
            radioEnvDevelop.Location = new Point(17, 30);
            radioEnvDevelop.Name = "radioEnvDevelop";
            radioEnvDevelop.Size = new Size(109, 29);
            radioEnvDevelop.TabIndex = 0;
            radioEnvDevelop.Text = "開発環境";
            radioEnvDevelop.UseVisualStyleBackColor = true;
            //
            // developGroupBox
            //
            developGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            developGroupBox.Controls.Add(textBoxDevUsername);
            developGroupBox.Controls.Add(devPasswordLabel);
            developGroupBox.Controls.Add(textBoxDevPassword);
            developGroupBox.Controls.Add(devUsernameLabel);
            developGroupBox.Controls.Add(textBoxDevBaseUrl);
            developGroupBox.Controls.Add(devUrlLabel);
            developGroupBox.Location = new Point(16, 381);
            developGroupBox.Name = "developGroupBox";
            developGroupBox.Size = new Size(948, 174);
            developGroupBox.TabIndex = 2;
            developGroupBox.TabStop = false;
            developGroupBox.Text = "開発環境";
            //
            // textBoxDevUsername
            //
            textBoxDevUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDevUsername.Location = new Point(112, 82);
            textBoxDevUsername.Name = "textBoxDevUsername";
            textBoxDevUsername.Size = new Size(817, 31);
            textBoxDevUsername.TabIndex = 3;
            //
            // devPasswordLabel
            //
            devPasswordLabel.AutoSize = true;
            devPasswordLabel.Location = new Point(24, 128);
            devPasswordLabel.Name = "devPasswordLabel";
            devPasswordLabel.Size = new Size(79, 25);
            devPasswordLabel.TabIndex = 4;
            devPasswordLabel.Text = "パスワード";
            //
            // textBoxDevPassword
            //
            textBoxDevPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDevPassword.Location = new Point(112, 123);
            textBoxDevPassword.Name = "textBoxDevPassword";
            textBoxDevPassword.PasswordChar = '*';
            textBoxDevPassword.Size = new Size(817, 31);
            textBoxDevPassword.TabIndex = 5;
            //
            // devUsernameLabel
            //
            devUsernameLabel.AutoSize = true;
            devUsernameLabel.Location = new Point(19, 87);
            devUsernameLabel.Name = "devUsernameLabel";
            devUsernameLabel.Size = new Size(84, 25);
            devUsernameLabel.TabIndex = 2;
            devUsernameLabel.Text = "ユーザー名";
            //
            // textBoxDevBaseUrl
            //
            textBoxDevBaseUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxDevBaseUrl.Location = new Point(112, 40);
            textBoxDevBaseUrl.Name = "textBoxDevBaseUrl";
            textBoxDevBaseUrl.Size = new Size(817, 31);
            textBoxDevBaseUrl.TabIndex = 1;
            //
            // devUrlLabel
            //
            devUrlLabel.AutoSize = true;
            devUrlLabel.Location = new Point(18, 45);
            devUrlLabel.Name = "devUrlLabel";
            devUrlLabel.Size = new Size(85, 25);
            devUrlLabel.TabIndex = 0;
            devUrlLabel.Text = "サーバURL";
            //
            // productionGroupBox
            //
            productionGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            productionGroupBox.Controls.Add(textBoxProdPassword);
            productionGroupBox.Controls.Add(textBoxProdUsername);
            productionGroupBox.Controls.Add(prodPasswordLabel);
            productionGroupBox.Controls.Add(prodUsernameLabel);
            productionGroupBox.Controls.Add(textBoxProdBaseUrl);
            productionGroupBox.Controls.Add(prodUrlLabel);
            productionGroupBox.Location = new Point(16, 564);
            productionGroupBox.Name = "productionGroupBox";
            productionGroupBox.Size = new Size(948, 174);
            productionGroupBox.TabIndex = 3;
            productionGroupBox.TabStop = false;
            productionGroupBox.Text = "本番環境";
            //
            // textBoxProdPassword
            //
            textBoxProdPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxProdPassword.Location = new Point(112, 123);
            textBoxProdPassword.Name = "textBoxProdPassword";
            textBoxProdPassword.PasswordChar = '*';
            textBoxProdPassword.Size = new Size(817, 31);
            textBoxProdPassword.TabIndex = 5;
            //
            // textBoxProdUsername
            //
            textBoxProdUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxProdUsername.Location = new Point(112, 82);
            textBoxProdUsername.Name = "textBoxProdUsername";
            textBoxProdUsername.Size = new Size(817, 31);
            textBoxProdUsername.TabIndex = 3;
            //
            // prodPasswordLabel
            //
            prodPasswordLabel.AutoSize = true;
            prodPasswordLabel.Location = new Point(24, 128);
            prodPasswordLabel.Name = "prodPasswordLabel";
            prodPasswordLabel.Size = new Size(79, 25);
            prodPasswordLabel.TabIndex = 4;
            prodPasswordLabel.Text = "パスワード";
            //
            // prodUsernameLabel
            //
            prodUsernameLabel.AutoSize = true;
            prodUsernameLabel.Location = new Point(19, 87);
            prodUsernameLabel.Name = "prodUsernameLabel";
            prodUsernameLabel.Size = new Size(84, 25);
            prodUsernameLabel.TabIndex = 2;
            prodUsernameLabel.Text = "ユーザー名";
            //
            // textBoxProdBaseUrl
            //
            textBoxProdBaseUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxProdBaseUrl.Location = new Point(112, 40);
            textBoxProdBaseUrl.Name = "textBoxProdBaseUrl";
            textBoxProdBaseUrl.Size = new Size(817, 31);
            textBoxProdBaseUrl.TabIndex = 1;
            //
            // prodUrlLabel
            //
            prodUrlLabel.AutoSize = true;
            prodUrlLabel.Location = new Point(18, 45);
            prodUrlLabel.Name = "prodUrlLabel";
            prodUrlLabel.Size = new Size(85, 25);
            prodUrlLabel.TabIndex = 0;
            prodUrlLabel.Text = "サーバURL";
            //
            // audioGroupBox
            //
            audioGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            audioGroupBox.Controls.Add(playSoundButton);
            audioGroupBox.Controls.Add(audioEnabledCheckBox);
            audioGroupBox.Location = new Point(16, 747);
            audioGroupBox.Name = "audioGroupBox";
            audioGroupBox.Size = new Size(948, 78);
            audioGroupBox.TabIndex = 4;
            audioGroupBox.TabStop = false;
            audioGroupBox.Text = "音声";
            //
            // playSoundButton
            //
            playSoundButton.Location = new Point(142, 29);
            playSoundButton.Name = "playSoundButton";
            playSoundButton.Size = new Size(112, 34);
            playSoundButton.TabIndex = 1;
            playSoundButton.Text = "テスト再生";
            playSoundButton.UseVisualStyleBackColor = true;
            playSoundButton.Click += playSoundButton_Click;
            //
            // audioEnabledCheckBox
            //
            audioEnabledCheckBox.AutoSize = true;
            audioEnabledCheckBox.Location = new Point(17, 32);
            audioEnabledCheckBox.Name = "audioEnabledCheckBox";
            audioEnabledCheckBox.Size = new Size(110, 29);
            audioEnabledCheckBox.TabIndex = 0;
            audioEnabledCheckBox.Text = "音声再生";
            audioEnabledCheckBox.UseVisualStyleBackColor = true;
            //
            // EnvConfigForm
            //
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(980, 842);
            Controls.Add(audioGroupBox);
            Controls.Add(productionGroupBox);
            Controls.Add(developGroupBox);
            Controls.Add(environmentChoiceGroupBox);
            Controls.Add(receptionSetsGroupBox);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            MinimumSize = new Size(800, 700);
            Name = "EnvConfigForm";
            Text = "環境設定";
            FormClosing += EnvConfigForm_FormClosing;
            Load += EnvConfigForm_Load;
            receptionSetsGroupBox.ResumeLayout(false);
            setEditorGroupBox.ResumeLayout(false);
            setEditorGroupBox.PerformLayout();
            environmentChoiceGroupBox.ResumeLayout(false);
            environmentChoiceGroupBox.PerformLayout();
            developGroupBox.ResumeLayout(false);
            developGroupBox.PerformLayout();
            productionGroupBox.ResumeLayout(false);
            productionGroupBox.PerformLayout();
            audioGroupBox.ResumeLayout(false);
            audioGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox receptionSetsGroupBox;
        private GroupBox setEditorGroupBox;
        private ComboBox readerComboBox;
        private Label readerLabel;
        private ComboBox printerComboBox;
        private Label printerLabel;
        private TextBox textBoxGate;
        private Label gateLabel;
        private Button moveDownButton;
        private Button moveUpButton;
        private Button deleteSetButton;
        private Button addSetButton;
        private ListBox setListBox;
        private GroupBox environmentChoiceGroupBox;
        private RadioButton radioEnvProduction;
        private RadioButton radioEnvDevelop;
        private GroupBox developGroupBox;
        private TextBox textBoxDevUsername;
        private Label devPasswordLabel;
        private TextBox textBoxDevPassword;
        private Label devUsernameLabel;
        private TextBox textBoxDevBaseUrl;
        private Label devUrlLabel;
        private GroupBox productionGroupBox;
        private TextBox textBoxProdPassword;
        private TextBox textBoxProdUsername;
        private Label prodPasswordLabel;
        private Label prodUsernameLabel;
        private TextBox textBoxProdBaseUrl;
        private Label prodUrlLabel;
        private GroupBox audioGroupBox;
        private Button playSoundButton;
        private CheckBox audioEnabledCheckBox;
    }
}
