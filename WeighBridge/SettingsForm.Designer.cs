namespace Weighbridge
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabScale = new System.Windows.Forms.TabPage();
            this.chkSimulator = new System.Windows.Forms.CheckBox();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.cmbHandshake = new System.Windows.Forms.ComboBox();
            this.lblHandshake = new System.Windows.Forms.Label();
            this.cmbStopBits = new System.Windows.Forms.ComboBox();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.cmbDataBits = new System.Windows.Forms.ComboBox();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.cmbParity = new System.Windows.Forms.ComboBox();
            this.lblParity = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.lblPortName = new System.Windows.Forms.Label();
            this.tabBranding = new System.Windows.Forms.TabPage();
            this.txtDefaultGov = new System.Windows.Forms.TextBox();
            this.lblDefaultGov = new System.Windows.Forms.Label();
            this.txtFooterText = new System.Windows.Forms.TextBox();
            this.lblFooterText = new System.Windows.Forms.Label();
            this.txtSubtitle = new System.Windows.Forms.TextBox();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabSettings.SuspendLayout();
            this.tabScale.SuspendLayout();
            this.tabBranding.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabScale);
            this.tabSettings.Controls.Add(this.tabBranding);
            this.tabSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabSettings.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.tabSettings.Location = new System.Drawing.Point(0, 0);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.RightToLeftLayout = true;
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(564, 380);
            this.tabSettings.TabIndex = 0;
            // 
            // tabScale
            // 
            this.tabScale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tabScale.Controls.Add(this.chkSimulator);
            this.tabScale.Controls.Add(this.cmbProtocol);
            this.tabScale.Controls.Add(this.lblProtocol);
            this.tabScale.Controls.Add(this.cmbHandshake);
            this.tabScale.Controls.Add(this.lblHandshake);
            this.tabScale.Controls.Add(this.cmbStopBits);
            this.tabScale.Controls.Add(this.lblStopBits);
            this.tabScale.Controls.Add(this.cmbDataBits);
            this.tabScale.Controls.Add(this.lblDataBits);
            this.tabScale.Controls.Add(this.cmbParity);
            this.tabScale.Controls.Add(this.lblParity);
            this.tabScale.Controls.Add(this.cmbBaudRate);
            this.tabScale.Controls.Add(this.lblBaudRate);
            this.tabScale.Controls.Add(this.cmbPortName);
            this.tabScale.Controls.Add(this.lblPortName);
            this.tabScale.Location = new System.Drawing.Point(4, 28);
            this.tabScale.Name = "tabScale";
            this.tabScale.Padding = new System.Windows.Forms.Padding(3);
            this.tabScale.Size = new System.Drawing.Size(556, 348);
            this.tabScale.TabIndex = 0;
            this.tabScale.Text = "اتصال مؤشر الميزان (COM)";
            // 
            // chkSimulator
            // 
            this.chkSimulator.AutoSize = true;
            this.chkSimulator.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.chkSimulator.ForeColor = System.Drawing.Color.DarkGreen;
            this.chkSimulator.Location = new System.Drawing.Point(50, 305);
            this.chkSimulator.Name = "chkSimulator";
            this.chkSimulator.Size = new System.Drawing.Size(460, 23);
            this.chkSimulator.TabIndex = 14;
            this.chkSimulator.Text = "تفعيل محاكي الميزان الافتراضي (للتجربة بدون جهاز ميزان حقيقي)";
            this.chkSimulator.UseVisualStyleBackColor = true;
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.Items.AddRange(new object[] {
            "ContinuousASCII (Yaohua / Generic)",
            "ToledoContinuous (Mettler Toledo)",
            "CAS_Continuous (CAS CI-Series)",
            "AveryWeighTronix (GSE / Avery)"});
            this.cmbProtocol.Location = new System.Drawing.Point(40, 260);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(260, 27);
            this.cmbProtocol.TabIndex = 13;
            // 
            // lblProtocol
            // 
            this.lblProtocol.Location = new System.Drawing.Point(320, 263);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(190, 24);
            this.lblProtocol.TabIndex = 12;
            this.lblProtocol.Text = "بروتوكول المؤشر:";
            // 
            // cmbHandshake
            // 
            this.cmbHandshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHandshake.FormattingEnabled = true;
            this.cmbHandshake.Items.AddRange(new object[] {
            "None",
            "RequestToSend",
            "RequestToSendXOnXOff",
            "XOnXOff"});
            this.cmbHandshake.Location = new System.Drawing.Point(40, 220);
            this.cmbHandshake.Name = "cmbHandshake";
            this.cmbHandshake.Size = new System.Drawing.Size(260, 27);
            this.cmbHandshake.TabIndex = 11;
            // 
            // lblHandshake
            // 
            this.lblHandshake.Location = new System.Drawing.Point(320, 223);
            this.lblHandshake.Name = "lblHandshake";
            this.lblHandshake.Size = new System.Drawing.Size(190, 24);
            this.lblHandshake.TabIndex = 10;
            this.lblHandshake.Text = "التحكم بالتدفق (Handshake):";
            // 
            // cmbStopBits
            // 
            this.cmbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStopBits.FormattingEnabled = true;
            this.cmbStopBits.Items.AddRange(new object[] {
            "One",
            "Two",
            "OnePointFive",
            "None"});
            this.cmbStopBits.Location = new System.Drawing.Point(40, 180);
            this.cmbStopBits.Name = "cmbStopBits";
            this.cmbStopBits.Size = new System.Drawing.Size(260, 27);
            this.cmbStopBits.TabIndex = 9;
            // 
            // lblStopBits
            // 
            this.lblStopBits.Location = new System.Drawing.Point(320, 183);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(190, 24);
            this.lblStopBits.TabIndex = 8;
            this.lblStopBits.Text = "بتات التوقف (Stop Bits):";
            // 
            // cmbDataBits
            // 
            this.cmbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataBits.FormattingEnabled = true;
            this.cmbDataBits.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.cmbDataBits.Location = new System.Drawing.Point(40, 140);
            this.cmbDataBits.Name = "cmbDataBits";
            this.cmbDataBits.Size = new System.Drawing.Size(260, 27);
            this.cmbDataBits.TabIndex = 7;
            // 
            // lblDataBits
            // 
            this.lblDataBits.Location = new System.Drawing.Point(320, 143);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(190, 24);
            this.lblDataBits.TabIndex = 6;
            this.lblDataBits.Text = "بتات البيانات (Data Bits):";
            // 
            // cmbParity
            // 
            this.cmbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParity.FormattingEnabled = true;
            this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cmbParity.Location = new System.Drawing.Point(40, 100);
            this.cmbParity.Name = "cmbParity";
            this.cmbParity.Size = new System.Drawing.Size(260, 27);
            this.cmbParity.TabIndex = 5;
            // 
            // lblParity
            // 
            this.lblParity.Location = new System.Drawing.Point(320, 103);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(190, 24);
            this.lblParity.TabIndex = 4;
            this.lblParity.Text = "التكافؤ (Parity):";
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(40, 60);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(260, 27);
            this.cmbBaudRate.TabIndex = 3;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.Location = new System.Drawing.Point(320, 63);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(190, 24);
            this.lblBaudRate.TabIndex = 2;
            this.lblBaudRate.Text = "معدل الباود (Baud Rate):";
            // 
            // cmbPortName
            // 
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "/dev/ttyUSB0",
            "/dev/ttyS0"});
            this.cmbPortName.Location = new System.Drawing.Point(40, 20);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(260, 27);
            this.cmbPortName.TabIndex = 1;
            // 
            // lblPortName
            // 
            this.lblPortName.Location = new System.Drawing.Point(320, 23);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(190, 24);
            this.lblPortName.TabIndex = 0;
            this.lblPortName.Text = "اسم المنفذ (Port Name):";
            // 
            // tabBranding
            // 
            this.tabBranding.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.tabBranding.Controls.Add(this.txtDefaultGov);
            this.tabBranding.Controls.Add(this.lblDefaultGov);
            this.tabBranding.Controls.Add(this.txtFooterText);
            this.tabBranding.Controls.Add(this.lblFooterText);
            this.tabBranding.Controls.Add(this.txtSubtitle);
            this.tabBranding.Controls.Add(this.lblSubtitle);
            this.tabBranding.Controls.Add(this.txtCompanyName);
            this.tabBranding.Controls.Add(this.lblCompanyName);
            this.tabBranding.Location = new System.Drawing.Point(4, 28);
            this.tabBranding.Name = "tabBranding";
            this.tabBranding.Padding = new System.Windows.Forms.Padding(3);
            this.tabBranding.Size = new System.Drawing.Size(556, 348);
            this.tabBranding.TabIndex = 1;
            this.tabBranding.Text = "بيانات الميزان وسند الطباعة";
            // 
            // txtDefaultGov
            // 
            this.txtDefaultGov.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.txtDefaultGov.Location = new System.Drawing.Point(30, 230);
            this.txtDefaultGov.Name = "txtDefaultGov";
            this.txtDefaultGov.Size = new System.Drawing.Size(350, 27);
            this.txtDefaultGov.TabIndex = 7;
            // 
            // lblDefaultGov
            // 
            this.lblDefaultGov.Location = new System.Drawing.Point(390, 233);
            this.lblDefaultGov.Name = "lblDefaultGov";
            this.lblDefaultGov.Size = new System.Drawing.Size(140, 24);
            this.lblDefaultGov.TabIndex = 6;
            this.lblDefaultGov.Text = "المحافظة الافتراضية:";
            // 
            // txtFooterText
            // 
            this.txtFooterText.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.txtFooterText.Location = new System.Drawing.Point(30, 160);
            this.txtFooterText.Multiline = true;
            this.txtFooterText.Name = "txtFooterText";
            this.txtFooterText.Size = new System.Drawing.Size(350, 50);
            this.txtFooterText.TabIndex = 5;
            // 
            // lblFooterText
            // 
            this.lblFooterText.Location = new System.Drawing.Point(390, 163);
            this.lblFooterText.Name = "lblFooterText";
            this.lblFooterText.Size = new System.Drawing.Size(140, 24);
            this.lblFooterText.TabIndex = 4;
            this.lblFooterText.Text = "سطر أسفل السند:";
            // 
            // txtSubtitle
            // 
            this.txtSubtitle.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.txtSubtitle.Location = new System.Drawing.Point(30, 90);
            this.txtSubtitle.Multiline = true;
            this.txtSubtitle.Name = "txtSubtitle";
            this.txtSubtitle.Size = new System.Drawing.Size(350, 50);
            this.txtSubtitle.TabIndex = 3;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Location = new System.Drawing.Point(390, 93);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(140, 24);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "العنوان وأرقام الهواتف:";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.txtCompanyName.Location = new System.Drawing.Point(30, 30);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(350, 27);
            this.txtCompanyName.TabIndex = 1;
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Location = new System.Drawing.Point(390, 33);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(140, 24);
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.Text = "اسم الميزان / الترويسة:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(220)))), ((int)(((byte)(150)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Noto Sans Arabic", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(420, 395);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "حفظ الإعدادات";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDefault.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.btnDefault.Location = new System.Drawing.Point(280, 395);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(120, 35);
            this.btnDefault.TabIndex = 2;
            this.btnDefault.Text = "استعادة الافتراضي";
            this.btnDefault.UseVisualStyleBackColor = false;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.btnCancel.Location = new System.Drawing.Point(30, 395);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "إلغاء";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(564, 442);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "إعدادات الميزان والتطبيق";
            this.tabSettings.ResumeLayout(false);
            this.tabScale.ResumeLayout(false);
            this.tabScale.PerformLayout();
            this.tabBranding.ResumeLayout(false);
            this.tabBranding.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabScale;
        private System.Windows.Forms.TabPage tabBranding;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.ComboBox cmbParity;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.ComboBox cmbDataBits;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.ComboBox cmbStopBits;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.ComboBox cmbHandshake;
        private System.Windows.Forms.Label lblHandshake;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.Label lblProtocol;
        private System.Windows.Forms.CheckBox chkSimulator;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtSubtitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TextBox txtFooterText;
        private System.Windows.Forms.Label lblFooterText;
        private System.Windows.Forms.TextBox txtDefaultGov;
        private System.Windows.Forms.Label lblDefaultGov;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnCancel;
    }
}
