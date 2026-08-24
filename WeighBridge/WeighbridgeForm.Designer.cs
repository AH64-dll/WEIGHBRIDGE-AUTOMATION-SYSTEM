namespace Weighbridge
{
    partial class WeighBridgeForm
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWeights = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsBtnUsers = new System.Windows.Forms.ToolStripButton();
            this.tsBtnWeights = new System.Windows.Forms.ToolStripButton();
            this.tsBtnCustomers = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDatabase = new System.Windows.Forms.ToolStripButton();
            this.tsBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExit = new System.Windows.Forms.ToolStripButton();
            this.ticketPreviewPanel = new System.Windows.Forms.Panel();
            this.lblTicketTitle = new System.Windows.Forms.Label();
            this.lblTicketSubtitle = new System.Windows.Forms.Label();
            this.lblSerial = new System.Windows.Forms.Label();
            this.ticketTablePanel = new System.Windows.Forms.Panel();
            this.lblValCustomer = new System.Windows.Forms.Label();
            this.lblTagCustomer = new System.Windows.Forms.Label();
            this.lblValDriver = new System.Windows.Forms.Label();
            this.lblTagDriver = new System.Windows.Forms.Label();
            this.lblValPlate = new System.Windows.Forms.Label();
            this.lblTagPlate = new System.Windows.Forms.Label();
            this.lblValTrailer = new System.Windows.Forms.Label();
            this.lblTagTrailer = new System.Windows.Forms.Label();
            this.lblValCargo = new System.Windows.Forms.Label();
            this.lblTagCargo = new System.Windows.Forms.Label();
            this.lblValGovernorate = new System.Windows.Forms.Label();
            this.lblTagGovernorate = new System.Windows.Forms.Label();
            this.weightsTablePanel = new System.Windows.Forms.Panel();
            this.lblTagFirstWeight = new System.Windows.Forms.Label();
            this.lblValFirstWeight = new System.Windows.Forms.Label();
            this.lblTagFirstTime = new System.Windows.Forms.Label();
            this.lblValFirstTime = new System.Windows.Forms.Label();
            this.lblTagFirstDate = new System.Windows.Forms.Label();
            this.lblValFirstDate = new System.Windows.Forms.Label();
            this.lblTagSecondWeight = new System.Windows.Forms.Label();
            this.lblValSecondWeight = new System.Windows.Forms.Label();
            this.lblTagSecondTime = new System.Windows.Forms.Label();
            this.lblValSecondTime = new System.Windows.Forms.Label();
            this.lblTagSecondDate = new System.Windows.Forms.Label();
            this.lblValSecondDate = new System.Windows.Forms.Label();
            this.lblTagNetWeight = new System.Windows.Forms.Label();
            this.lblValNetWeight = new System.Windows.Forms.Label();
            this.lblTicketFooter = new System.Windows.Forms.Label();
            this.vehicleInputGroup = new System.Windows.Forms.GroupBox();
            this.btnPrevRecord = new System.Windows.Forms.Button();
            this.btnNextRecord = new System.Windows.Forms.Button();
            this.lblInputCustomer = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.ComboBox();
            this.lblInputPlate = new System.Windows.Forms.Label();
            this.txtCarPlate = new System.Windows.Forms.TextBox();
            this.lblInputCargo = new System.Windows.Forms.Label();
            this.txtCargoType = new System.Windows.Forms.ComboBox();
            this.lblInputGov = new System.Windows.Forms.Label();
            this.txtGovernorate = new System.Windows.Forms.ComboBox();
            this.lblInputDriver = new System.Windows.Forms.Label();
            this.txtDriverName = new System.Windows.Forms.TextBox();
            this.lblInputTrailer = new System.Windows.Forms.Label();
            this.txtTrailerNo = new System.Windows.Forms.TextBox();
            this.btnFirstWeight = new System.Windows.Forms.Button();
            this.btnSecondWeight = new System.Windows.Forms.Button();
            this.btnDailySettings = new System.Windows.Forms.Button();
            this.pendingQueueGroup = new System.Windows.Forms.GroupBox();
            this.dtpPendingDate = new System.Windows.Forms.DateTimePicker();
            this.gridPending = new System.Windows.Forms.ListView();
            this.colPlate = new System.Windows.Forms.ColumnHeader();
            this.colFirstWeight = new System.Windows.Forms.ColumnHeader();
            this.colFirstTime = new System.Windows.Forms.ColumnHeader();
            this.scaleMonitorGroup = new System.Windows.Forms.GroupBox();
            this.panelLiveWeight = new System.Windows.Forms.Panel();
            this.lblLiveWeightValue = new System.Windows.Forms.Label();
            this.lblLiveWeightUnit = new System.Windows.Forms.Label();
            this.ledStability = new System.Windows.Forms.Panel();
            this.lblSearchPlate = new System.Windows.Forms.Label();
            this.txtSearchPlate = new System.Windows.Forms.TextBox();
            this.btnSearchPlate = new System.Windows.Forms.Button();
            this.lblRxEvent = new System.Windows.Forms.Label();
            this.lblInputLen = new System.Windows.Forms.Label();
            this.lblScaleStatus = new System.Windows.Forms.Label();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.ticketPreviewPanel.SuspendLayout();
            this.ticketTablePanel.SuspendLayout();
            this.weightsTablePanel.SuspendLayout();
            this.vehicleInputGroup.SuspendLayout();
            this.pendingQueueGroup.SuspendLayout();
            this.scaleMonitorGroup.SuspendLayout();
            this.panelLiveWeight.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.menuStrip.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuUsers,
            this.menuWeights,
            this.menuCustomers,
            this.menuDatabase,
            this.menuAbout,
            this.menuPrint,
            this.menuExit});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuStrip.Size = new System.Drawing.Size(964, 27);
            this.menuStrip.TabIndex = 0;
            // 
            // menuUsers
            // 
            this.menuUsers.Name = "menuUsers";
            this.menuUsers.Size = new System.Drawing.Size(81, 23);
            this.menuUsers.Text = "المستخدمين";
            this.menuUsers.Click += new System.EventHandler(this.menuUsers_Click);
            // 
            // menuWeights
            // 
            this.menuWeights.Name = "menuWeights";
            this.menuWeights.Size = new System.Drawing.Size(56, 23);
            this.menuWeights.Text = "الأوزان";
            this.menuWeights.Click += new System.EventHandler(this.menuWeights_Click);
            // 
            // menuCustomers
            // 
            this.menuCustomers.Name = "menuCustomers";
            this.menuCustomers.Size = new System.Drawing.Size(90, 23);
            this.menuCustomers.Text = "حساب العملاء";
            this.menuCustomers.Click += new System.EventHandler(this.menuCustomers_Click);
            // 
            // menuDatabase
            // 
            this.menuDatabase.Name = "menuDatabase";
            this.menuDatabase.Size = new System.Drawing.Size(89, 23);
            this.menuDatabase.Text = "قاعدة البيانات";
            this.menuDatabase.Click += new System.EventHandler(this.menuDatabase_Click);
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(82, 23);
            this.menuAbout.Text = "عن البرنامج";
            this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
            // 
            // menuPrint
            // 
            this.menuPrint.Name = "menuPrint";
            this.menuPrint.Size = new System.Drawing.Size(51, 23);
            this.menuPrint.Text = "طباعة";
            this.menuPrint.Click += new System.EventHandler(this.menuPrint_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(49, 23);
            this.menuExit.Text = "خروج";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnUsers,
            this.tsBtnWeights,
            this.tsBtnCustomers,
            this.tsBtnDatabase,
            this.tsBtnPrint,
            this.tsBtnExit});
            this.toolStrip.Location = new System.Drawing.Point(0, 27);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip.Size = new System.Drawing.Size(964, 31);
            this.toolStrip.TabIndex = 1;
            // 
            // tsBtnUsers
            // 
            this.tsBtnUsers.Font = new System.Drawing.Font("Noto Sans Arabic", 9F, System.Drawing.FontStyle.Bold);
            this.tsBtnUsers.Name = "tsBtnUsers";
            this.tsBtnUsers.Size = new System.Drawing.Size(73, 28);
            this.tsBtnUsers.Text = "المستخدمين";
            this.tsBtnUsers.Click += new System.EventHandler(this.menuUsers_Click);
            // 
            // tsBtnWeights
            // 
            this.tsBtnWeights.Font = new System.Drawing.Font("Noto Sans Arabic", 9F, System.Drawing.FontStyle.Bold);
            this.tsBtnWeights.Name = "tsBtnWeights";
            this.tsBtnWeights.Size = new System.Drawing.Size(75, 28);
            this.tsBtnWeights.Text = "سجل الأوزان";
            this.tsBtnWeights.Click += new System.EventHandler(this.menuWeights_Click);
            // 
            // tsBtnCustomers
            // 
            this.tsBtnCustomers.Font = new System.Drawing.Font("Noto Sans Arabic", 9F, System.Drawing.FontStyle.Bold);
            this.tsBtnCustomers.Name = "tsBtnCustomers";
            this.tsBtnCustomers.Size = new System.Drawing.Size(81, 28);
            this.tsBtnCustomers.Text = "حساب العملاء";
            this.tsBtnCustomers.Click += new System.EventHandler(this.menuCustomers_Click);
            // 
            // tsBtnDatabase
            // 
            this.tsBtnDatabase.Font = new System.Drawing.Font("Noto Sans Arabic", 9F, System.Drawing.FontStyle.Bold);
            this.tsBtnDatabase.Name = "tsBtnDatabase";
            this.tsBtnDatabase.Size = new System.Drawing.Size(59, 28);
            this.tsBtnDatabase.Text = "الإعدادات";
            this.tsBtnDatabase.Click += new System.EventHandler(this.menuDatabase_Click);
            // 
            // tsBtnPrint
            // 
            this.tsBtnPrint.Font = new System.Drawing.Font("Noto Sans Arabic", 9F, System.Drawing.FontStyle.Bold);
            this.tsBtnPrint.Name = "tsBtnPrint";
            this.tsBtnPrint.Size = new System.Drawing.Size(86, 28);
            this.tsBtnPrint.Text = "معاينة وطباعة";
            this.tsBtnPrint.Click += new System.EventHandler(this.menuPrint_Click);
            // 
            // tsBtnExit
            // 
            this.tsBtnExit.Font = new System.Drawing.Font("Noto Sans Arabic", 9F, System.Drawing.FontStyle.Bold);
            this.tsBtnExit.Name = "tsBtnExit";
            this.tsBtnExit.Size = new System.Drawing.Size(43, 28);
            this.tsBtnExit.Text = "خروج";
            this.tsBtnExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // ticketPreviewPanel
            // 
            this.ticketPreviewPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(248)))), ((int)(((byte)(235)))));
            this.ticketPreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ticketPreviewPanel.Controls.Add(this.lblTicketTitle);
            this.ticketPreviewPanel.Controls.Add(this.lblTicketSubtitle);
            this.ticketPreviewPanel.Controls.Add(this.lblSerial);
            this.ticketPreviewPanel.Controls.Add(this.ticketTablePanel);
            this.ticketPreviewPanel.Controls.Add(this.weightsTablePanel);
            this.ticketPreviewPanel.Controls.Add(this.lblTicketFooter);
            this.ticketPreviewPanel.Location = new System.Drawing.Point(8, 62);
            this.ticketPreviewPanel.Name = "ticketPreviewPanel";
            this.ticketPreviewPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ticketPreviewPanel.Size = new System.Drawing.Size(948, 285);
            this.ticketPreviewPanel.TabIndex = 2;
            // 
            // lblTicketTitle
            // 
            this.lblTicketTitle.Font = new System.Drawing.Font("Noto Sans Arabic", 18F, System.Drawing.FontStyle.Bold);
            this.lblTicketTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTicketTitle.Location = new System.Drawing.Point(100, 4);
            this.lblTicketTitle.Name = "lblTicketTitle";
            this.lblTicketTitle.Size = new System.Drawing.Size(748, 38);
            this.lblTicketTitle.TabIndex = 0;
            this.lblTicketTitle.Text = "ميزان بسكول ابو السعد ۱۲۰ طن";
            this.lblTicketTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTicketSubtitle
            // 
            this.lblTicketSubtitle.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTicketSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTicketSubtitle.Location = new System.Drawing.Point(100, 40);
            this.lblTicketSubtitle.Name = "lblTicketSubtitle";
            this.lblTicketSubtitle.Size = new System.Drawing.Size(748, 22);
            this.lblTicketSubtitle.TabIndex = 1;
            this.lblTicketSubtitle.Text = "العنوان فوه كفر الشيخ ٠١٠٩٢١٨٠١٤٦ ايمن / ٠١٠٦٢٥٥١١٨٩ محمد";
            this.lblTicketSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSerial
            // 
            this.lblSerial.Font = new System.Drawing.Font("Noto Sans Arabic", 11F, System.Drawing.FontStyle.Bold);
            this.lblSerial.Location = new System.Drawing.Point(15, 8);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(120, 25);
            this.lblSerial.TabIndex = 2;
            this.lblSerial.Text = "مسلسل: 1-";
            this.lblSerial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ticketTablePanel
            // 
            this.ticketTablePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ticketTablePanel.Controls.Add(this.lblValCustomer);
            this.ticketTablePanel.Controls.Add(this.lblTagCustomer);
            this.ticketTablePanel.Controls.Add(this.lblValDriver);
            this.ticketTablePanel.Controls.Add(this.lblTagDriver);
            this.ticketTablePanel.Controls.Add(this.lblValPlate);
            this.ticketTablePanel.Controls.Add(this.lblTagPlate);
            this.ticketTablePanel.Controls.Add(this.lblValTrailer);
            this.ticketTablePanel.Controls.Add(this.lblTagTrailer);
            this.ticketTablePanel.Controls.Add(this.lblValCargo);
            this.ticketTablePanel.Controls.Add(this.lblTagCargo);
            this.ticketTablePanel.Controls.Add(this.lblValGovernorate);
            this.ticketTablePanel.Controls.Add(this.lblTagGovernorate);
            this.ticketTablePanel.Location = new System.Drawing.Point(10, 65);
            this.ticketTablePanel.Name = "ticketTablePanel";
            this.ticketTablePanel.Size = new System.Drawing.Size(926, 95);
            this.ticketTablePanel.TabIndex = 3;
            // 
            // lblTagCustomer
            // 
            this.lblTagCustomer.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagCustomer.Location = new System.Drawing.Point(825, 6);
            this.lblTagCustomer.Name = "lblTagCustomer";
            this.lblTagCustomer.Size = new System.Drawing.Size(95, 24);
            this.lblTagCustomer.TabIndex = 0;
            this.lblTagCustomer.Text = "اسم العميل:";
            // 
            // lblValCustomer
            // 
            this.lblValCustomer.Font = new System.Drawing.Font("Noto Sans Arabic", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblValCustomer.ForeColor = System.Drawing.Color.Navy;
            this.lblValCustomer.Location = new System.Drawing.Point(475, 6);
            this.lblValCustomer.Name = "lblValCustomer";
            this.lblValCustomer.Size = new System.Drawing.Size(345, 24);
            this.lblValCustomer.TabIndex = 1;
            this.lblValCustomer.Text = "محمد الشبيبة";
            // 
            // lblTagDriver
            // 
            this.lblTagDriver.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagDriver.Location = new System.Drawing.Point(375, 6);
            this.lblTagDriver.Name = "lblTagDriver";
            this.lblTagDriver.Size = new System.Drawing.Size(95, 24);
            this.lblTagDriver.TabIndex = 2;
            this.lblTagDriver.Text = "اسم السائق:";
            // 
            // lblValDriver
            // 
            this.lblValDriver.Font = new System.Drawing.Font("Noto Sans Arabic", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblValDriver.Location = new System.Drawing.Point(10, 6);
            this.lblValDriver.Name = "lblValDriver";
            this.lblValDriver.Size = new System.Drawing.Size(360, 24);
            this.lblValDriver.TabIndex = 3;
            this.lblValDriver.Text = "-";
            // 
            // lblTagPlate
            // 
            this.lblTagPlate.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagPlate.Location = new System.Drawing.Point(825, 35);
            this.lblTagPlate.Name = "lblTagPlate";
            this.lblTagPlate.Size = new System.Drawing.Size(95, 24);
            this.lblTagPlate.TabIndex = 4;
            this.lblTagPlate.Text = "رقم السيارة:";
            // 
            // lblValPlate
            // 
            this.lblValPlate.Font = new System.Drawing.Font("Noto Sans Arabic", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblValPlate.ForeColor = System.Drawing.Color.DarkRed;
            this.lblValPlate.Location = new System.Drawing.Point(475, 35);
            this.lblValPlate.Name = "lblValPlate";
            this.lblValPlate.Size = new System.Drawing.Size(345, 24);
            this.lblValPlate.TabIndex = 5;
            this.lblValPlate.Text = "٧٩٦٨";
            // 
            // lblTagTrailer
            // 
            this.lblTagTrailer.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagTrailer.Location = new System.Drawing.Point(375, 35);
            this.lblTagTrailer.Name = "lblTagTrailer";
            this.lblTagTrailer.Size = new System.Drawing.Size(95, 24);
            this.lblTagTrailer.TabIndex = 6;
            this.lblTagTrailer.Text = "رقم المقطورة:";
            // 
            // lblValTrailer
            // 
            this.lblValTrailer.Font = new System.Drawing.Font("Noto Sans Arabic", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblValTrailer.Location = new System.Drawing.Point(10, 35);
            this.lblValTrailer.Name = "lblValTrailer";
            this.lblValTrailer.Size = new System.Drawing.Size(360, 24);
            this.lblValTrailer.TabIndex = 7;
            this.lblValTrailer.Text = "-";
            // 
            // lblTagCargo
            // 
            this.lblTagCargo.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagCargo.Location = new System.Drawing.Point(825, 64);
            this.lblTagCargo.Name = "lblTagCargo";
            this.lblTagCargo.Size = new System.Drawing.Size(95, 24);
            this.lblTagCargo.TabIndex = 8;
            this.lblTagCargo.Text = "نوع الشحنة:";
            // 
            // lblValCargo
            // 
            this.lblValCargo.Font = new System.Drawing.Font("Noto Sans Arabic", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblValCargo.Location = new System.Drawing.Point(475, 64);
            this.lblValCargo.Name = "lblValCargo";
            this.lblValCargo.Size = new System.Drawing.Size(345, 24);
            this.lblValCargo.TabIndex = 9;
            this.lblValCargo.Text = "-";
            // 
            // lblTagGovernorate
            // 
            this.lblTagGovernorate.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagGovernorate.Location = new System.Drawing.Point(375, 64);
            this.lblTagGovernorate.Name = "lblTagGovernorate";
            this.lblTagGovernorate.Size = new System.Drawing.Size(95, 24);
            this.lblTagGovernorate.TabIndex = 10;
            this.lblTagGovernorate.Text = "المحافظة:";
            // 
            // lblValGovernorate
            // 
            this.lblValGovernorate.Font = new System.Drawing.Font("Noto Sans Arabic", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblValGovernorate.Location = new System.Drawing.Point(10, 64);
            this.lblValGovernorate.Name = "lblValGovernorate";
            this.lblValGovernorate.Size = new System.Drawing.Size(360, 24);
            this.lblValGovernorate.TabIndex = 11;
            this.lblValGovernorate.Text = "كفر الشيخ";
            // 
            // weightsTablePanel
            // 
            this.weightsTablePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.weightsTablePanel.Controls.Add(this.lblTagFirstWeight);
            this.weightsTablePanel.Controls.Add(this.lblValFirstWeight);
            this.weightsTablePanel.Controls.Add(this.lblTagFirstTime);
            this.weightsTablePanel.Controls.Add(this.lblValFirstTime);
            this.weightsTablePanel.Controls.Add(this.lblTagFirstDate);
            this.weightsTablePanel.Controls.Add(this.lblValFirstDate);
            this.weightsTablePanel.Controls.Add(this.lblTagSecondWeight);
            this.weightsTablePanel.Controls.Add(this.lblValSecondWeight);
            this.weightsTablePanel.Controls.Add(this.lblTagSecondTime);
            this.weightsTablePanel.Controls.Add(this.lblValSecondTime);
            this.weightsTablePanel.Controls.Add(this.lblTagSecondDate);
            this.weightsTablePanel.Controls.Add(this.lblValSecondDate);
            this.weightsTablePanel.Controls.Add(this.lblTagNetWeight);
            this.weightsTablePanel.Controls.Add(this.lblValNetWeight);
            this.weightsTablePanel.Location = new System.Drawing.Point(10, 163);
            this.weightsTablePanel.Name = "weightsTablePanel";
            this.weightsTablePanel.Size = new System.Drawing.Size(926, 88);
            this.weightsTablePanel.TabIndex = 4;
            // 
            // lblTagFirstWeight
            // 
            this.lblTagFirstWeight.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagFirstWeight.Location = new System.Drawing.Point(825, 4);
            this.lblTagFirstWeight.Name = "lblTagFirstWeight";
            this.lblTagFirstWeight.Size = new System.Drawing.Size(95, 24);
            this.lblTagFirstWeight.TabIndex = 0;
            this.lblTagFirstWeight.Text = "الوزن الأول:";
            // 
            // lblValFirstWeight
            // 
            this.lblValFirstWeight.Font = new System.Drawing.Font("Noto Sans Arabic", 11F, System.Drawing.FontStyle.Bold);
            this.lblValFirstWeight.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblValFirstWeight.Location = new System.Drawing.Point(690, 4);
            this.lblValFirstWeight.Name = "lblValFirstWeight";
            this.lblValFirstWeight.Size = new System.Drawing.Size(130, 24);
            this.lblValFirstWeight.TabIndex = 1;
            this.lblValFirstWeight.Text = "٨٧٧٨";
            // 
            // lblTagFirstTime
            // 
            this.lblTagFirstTime.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagFirstTime.Location = new System.Drawing.Point(620, 4);
            this.lblTagFirstTime.Name = "lblTagFirstTime";
            this.lblTagFirstTime.Size = new System.Drawing.Size(60, 24);
            this.lblTagFirstTime.TabIndex = 2;
            this.lblTagFirstTime.Text = "الوقت:";
            // 
            // lblValFirstTime
            // 
            this.lblValFirstTime.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblValFirstTime.Location = new System.Drawing.Point(480, 4);
            this.lblValFirstTime.Name = "lblValFirstTime";
            this.lblValFirstTime.Size = new System.Drawing.Size(135, 24);
            this.lblValFirstTime.TabIndex = 3;
            this.lblValFirstTime.Text = "٠٧:١٣:٤٢ م";
            // 
            // lblTagFirstDate
            // 
            this.lblTagFirstDate.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagFirstDate.Location = new System.Drawing.Point(400, 4);
            this.lblTagFirstDate.Name = "lblTagFirstDate";
            this.lblTagFirstDate.Size = new System.Drawing.Size(70, 24);
            this.lblTagFirstDate.TabIndex = 4;
            this.lblTagFirstDate.Text = "التاريخ:";
            // 
            // lblValFirstDate
            // 
            this.lblValFirstDate.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblValFirstDate.Location = new System.Drawing.Point(260, 4);
            this.lblValFirstDate.Name = "lblValFirstDate";
            this.lblValFirstDate.Size = new System.Drawing.Size(135, 24);
            this.lblValFirstDate.TabIndex = 5;
            this.lblValFirstDate.Text = "٢٠٢٦/٠٨/٢٤";
            // 
            // lblTagSecondWeight
            // 
            this.lblTagSecondWeight.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagSecondWeight.Location = new System.Drawing.Point(825, 32);
            this.lblTagSecondWeight.Name = "lblTagSecondWeight";
            this.lblTagSecondWeight.Size = new System.Drawing.Size(95, 24);
            this.lblTagSecondWeight.TabIndex = 6;
            this.lblTagSecondWeight.Text = "الوزن الثاني:";
            // 
            // lblValSecondWeight
            // 
            this.lblValSecondWeight.Font = new System.Drawing.Font("Noto Sans Arabic", 11F, System.Drawing.FontStyle.Bold);
            this.lblValSecondWeight.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblValSecondWeight.Location = new System.Drawing.Point(690, 32);
            this.lblValSecondWeight.Name = "lblValSecondWeight";
            this.lblValSecondWeight.Size = new System.Drawing.Size(130, 24);
            this.lblValSecondWeight.TabIndex = 7;
            this.lblValSecondWeight.Text = "٤٣٥٤";
            // 
            // lblTagSecondTime
            // 
            this.lblTagSecondTime.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagSecondTime.Location = new System.Drawing.Point(620, 32);
            this.lblTagSecondTime.Name = "lblTagSecondTime";
            this.lblTagSecondTime.Size = new System.Drawing.Size(60, 24);
            this.lblTagSecondTime.TabIndex = 8;
            this.lblTagSecondTime.Text = "الوقت:";
            // 
            // lblValSecondTime
            // 
            this.lblValSecondTime.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblValSecondTime.Location = new System.Drawing.Point(480, 32);
            this.lblValSecondTime.Name = "lblValSecondTime";
            this.lblValSecondTime.Size = new System.Drawing.Size(135, 24);
            this.lblValSecondTime.TabIndex = 9;
            this.lblValSecondTime.Text = "٠٨:٥٤:١٩ م";
            // 
            // lblTagSecondDate
            // 
            this.lblTagSecondDate.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblTagSecondDate.Location = new System.Drawing.Point(400, 32);
            this.lblTagSecondDate.Name = "lblTagSecondDate";
            this.lblTagSecondDate.Size = new System.Drawing.Size(70, 24);
            this.lblTagSecondDate.TabIndex = 10;
            this.lblTagSecondDate.Text = "التاريخ:";
            // 
            // lblValSecondDate
            // 
            this.lblValSecondDate.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.lblValSecondDate.Location = new System.Drawing.Point(260, 32);
            this.lblValSecondDate.Name = "lblValSecondDate";
            this.lblValSecondDate.Size = new System.Drawing.Size(135, 24);
            this.lblValSecondDate.TabIndex = 11;
            this.lblValSecondDate.Text = "٢٠٢٦/٠٨/٢٤";
            // 
            // lblTagNetWeight
            // 
            this.lblTagNetWeight.Font = new System.Drawing.Font("Noto Sans Arabic", 11F, System.Drawing.FontStyle.Bold);
            this.lblTagNetWeight.Location = new System.Drawing.Point(825, 59);
            this.lblTagNetWeight.Name = "lblTagNetWeight";
            this.lblTagNetWeight.Size = new System.Drawing.Size(95, 24);
            this.lblTagNetWeight.TabIndex = 12;
            this.lblTagNetWeight.Text = "صافي الوزن:";
            // 
            // lblValNetWeight
            // 
            this.lblValNetWeight.Font = new System.Drawing.Font("Noto Sans Arabic", 12F, System.Drawing.FontStyle.Bold);
            this.lblValNetWeight.ForeColor = System.Drawing.Color.DarkRed;
            this.lblValNetWeight.Location = new System.Drawing.Point(550, 59);
            this.lblValNetWeight.Name = "lblValNetWeight";
            this.lblValNetWeight.Size = new System.Drawing.Size(270, 24);
            this.lblValNetWeight.TabIndex = 13;
            this.lblValNetWeight.Text = "٤٤٢٤ كجم";
            // 
            // lblTicketFooter
            // 
            this.lblTicketFooter.Font = new System.Drawing.Font("Noto Sans Arabic", 8.5F, System.Drawing.FontStyle.Regular);
            this.lblTicketFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lblTicketFooter.Location = new System.Drawing.Point(10, 258);
            this.lblTicketFooter.Name = "lblTicketFooter";
            this.lblTicketFooter.Size = new System.Drawing.Size(926, 20);
            this.lblTicketFooter.TabIndex = 5;
            this.lblTicketFooter.Text = "هذا البرنامج صنع خصيصا لشركة اولاد الغلباني الحديثة بدمنهور";
            this.lblTicketFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // vehicleInputGroup
            // 
            this.vehicleInputGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.vehicleInputGroup.Controls.Add(this.btnPrevRecord);
            this.vehicleInputGroup.Controls.Add(this.btnNextRecord);
            this.vehicleInputGroup.Controls.Add(this.lblInputCustomer);
            this.vehicleInputGroup.Controls.Add(this.txtCustomerName);
            this.vehicleInputGroup.Controls.Add(this.lblInputPlate);
            this.vehicleInputGroup.Controls.Add(this.txtCarPlate);
            this.vehicleInputGroup.Controls.Add(this.lblInputCargo);
            this.vehicleInputGroup.Controls.Add(this.txtCargoType);
            this.vehicleInputGroup.Controls.Add(this.lblInputGov);
            this.vehicleInputGroup.Controls.Add(this.txtGovernorate);
            this.vehicleInputGroup.Controls.Add(this.lblInputDriver);
            this.vehicleInputGroup.Controls.Add(this.txtDriverName);
            this.vehicleInputGroup.Controls.Add(this.lblInputTrailer);
            this.vehicleInputGroup.Controls.Add(this.txtTrailerNo);
            this.vehicleInputGroup.Controls.Add(this.btnFirstWeight);
            this.vehicleInputGroup.Controls.Add(this.btnSecondWeight);
            this.vehicleInputGroup.Controls.Add(this.btnDailySettings);
            this.vehicleInputGroup.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.vehicleInputGroup.Location = new System.Drawing.Point(545, 353);
            this.vehicleInputGroup.Name = "vehicleInputGroup";
            this.vehicleInputGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.vehicleInputGroup.Size = new System.Drawing.Size(411, 285);
            this.vehicleInputGroup.TabIndex = 3;
            this.vehicleInputGroup.TabStop = false;
            this.vehicleInputGroup.Text = "ادخال بيانات السيارة";
            // 
            // btnPrevRecord
            // 
            this.btnPrevRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(220)))), ((int)(((byte)(150)))));
            this.btnPrevRecord.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrevRecord.Font = new System.Drawing.Font("Noto Sans Arabic", 11F, System.Drawing.FontStyle.Bold);
            this.btnPrevRecord.Location = new System.Drawing.Point(20, 20);
            this.btnPrevRecord.Name = "btnPrevRecord";
            this.btnPrevRecord.Size = new System.Drawing.Size(40, 30);
            this.btnPrevRecord.TabIndex = 0;
            this.btnPrevRecord.Text = "◀";
            this.btnPrevRecord.UseVisualStyleBackColor = false;
            this.btnPrevRecord.Click += new System.EventHandler(this.btnPrevRecord_Click);
            // 
            // btnNextRecord
            // 
            this.btnNextRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(220)))), ((int)(((byte)(150)))));
            this.btnNextRecord.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNextRecord.Font = new System.Drawing.Font("Noto Sans Arabic", 11F, System.Drawing.FontStyle.Bold);
            this.btnNextRecord.Location = new System.Drawing.Point(65, 20);
            this.btnNextRecord.Name = "btnNextRecord";
            this.btnNextRecord.Size = new System.Drawing.Size(40, 30);
            this.btnNextRecord.TabIndex = 1;
            this.btnNextRecord.Text = "▶";
            this.btnNextRecord.UseVisualStyleBackColor = false;
            this.btnNextRecord.Click += new System.EventHandler(this.btnNextRecord_Click);
            // 
            // lblInputCustomer
            // 
            this.lblInputCustomer.Location = new System.Drawing.Point(305, 55);
            this.lblInputCustomer.Name = "lblInputCustomer";
            this.lblInputCustomer.Size = new System.Drawing.Size(95, 24);
            this.lblInputCustomer.TabIndex = 2;
            this.lblInputCustomer.Text = "اسم العميل";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.txtCustomerName.FormattingEnabled = true;
            this.txtCustomerName.Location = new System.Drawing.Point(20, 52);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(280, 27);
            this.txtCustomerName.TabIndex = 3;
            this.txtCustomerName.TextChanged += new System.EventHandler(this.txtInputs_Changed);
            // 
            // lblInputPlate
            // 
            this.lblInputPlate.Location = new System.Drawing.Point(305, 87);
            this.lblInputPlate.Name = "lblInputPlate";
            this.lblInputPlate.Size = new System.Drawing.Size(95, 24);
            this.lblInputPlate.TabIndex = 4;
            this.lblInputPlate.Text = "رقم السيارة";
            // 
            // txtCarPlate
            // 
            this.txtCarPlate.Font = new System.Drawing.Font("Noto Sans Arabic", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtCarPlate.Location = new System.Drawing.Point(20, 84);
            this.txtCarPlate.Name = "txtCarPlate";
            this.txtCarPlate.Size = new System.Drawing.Size(280, 28);
            this.txtCarPlate.TabIndex = 5;
            this.txtCarPlate.TextChanged += new System.EventHandler(this.txtInputs_Changed);
            // 
            // lblInputCargo
            // 
            this.lblInputCargo.Location = new System.Drawing.Point(305, 120);
            this.lblInputCargo.Name = "lblInputCargo";
            this.lblInputCargo.Size = new System.Drawing.Size(95, 24);
            this.lblInputCargo.TabIndex = 6;
            this.lblInputCargo.Text = "نوع الشحنة";
            // 
            // txtCargoType
            // 
            this.txtCargoType.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.txtCargoType.FormattingEnabled = true;
            this.txtCargoType.Items.AddRange(new object[] {
            "حديد وخردة",
            "رمل وزلط",
            "أسمنت ومواد بناء",
            "أرز وحبوب",
            "قمح ودقيق",
            "بضاعة عامة",
            "أعلاف"});
            this.txtCargoType.Location = new System.Drawing.Point(20, 117);
            this.txtCargoType.Name = "txtCargoType";
            this.txtCargoType.Size = new System.Drawing.Size(280, 27);
            this.txtCargoType.TabIndex = 7;
            this.txtCargoType.TextChanged += new System.EventHandler(this.txtInputs_Changed);
            // 
            // lblInputGov
            // 
            this.lblInputGov.Location = new System.Drawing.Point(305, 153);
            this.lblInputGov.Name = "lblInputGov";
            this.lblInputGov.Size = new System.Drawing.Size(95, 24);
            this.lblInputGov.TabIndex = 8;
            this.lblInputGov.Text = "المحافظة";
            // 
            // txtGovernorate
            // 
            this.txtGovernorate.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.txtGovernorate.FormattingEnabled = true;
            this.txtGovernorate.Items.AddRange(new object[] {
            "كفر الشيخ",
            "البحيرة",
            "الغربية",
            "الدقهلية",
            "الإسكندرية",
            "القاهرة",
            "الشرقية",
            "المنوفية",
            "دمياط"});
            this.txtGovernorate.Location = new System.Drawing.Point(20, 150);
            this.txtGovernorate.Name = "txtGovernorate";
            this.txtGovernorate.Size = new System.Drawing.Size(280, 27);
            this.txtGovernorate.TabIndex = 9;
            this.txtGovernorate.Text = "كفر الشيخ";
            this.txtGovernorate.TextChanged += new System.EventHandler(this.txtInputs_Changed);
            // 
            // lblInputDriver
            // 
            this.lblInputDriver.Location = new System.Drawing.Point(305, 186);
            this.lblInputDriver.Name = "lblInputDriver";
            this.lblInputDriver.Size = new System.Drawing.Size(95, 24);
            this.lblInputDriver.TabIndex = 10;
            this.lblInputDriver.Text = "اسم السائق";
            // 
            // txtDriverName
            // 
            this.txtDriverName.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.txtDriverName.Location = new System.Drawing.Point(20, 183);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(280, 27);
            this.txtDriverName.TabIndex = 11;
            this.txtDriverName.TextChanged += new System.EventHandler(this.txtInputs_Changed);
            // 
            // lblInputTrailer
            // 
            this.lblInputTrailer.Location = new System.Drawing.Point(305, 219);
            this.lblInputTrailer.Name = "lblInputTrailer";
            this.lblInputTrailer.Size = new System.Drawing.Size(95, 24);
            this.lblInputTrailer.TabIndex = 12;
            this.lblInputTrailer.Text = "رقم المقطورة";
            // 
            // txtTrailerNo
            // 
            this.txtTrailerNo.Font = new System.Drawing.Font("Noto Sans Arabic", 10F);
            this.txtTrailerNo.Location = new System.Drawing.Point(20, 216);
            this.txtTrailerNo.Name = "txtTrailerNo";
            this.txtTrailerNo.Size = new System.Drawing.Size(280, 27);
            this.txtTrailerNo.TabIndex = 13;
            this.txtTrailerNo.TextChanged += new System.EventHandler(this.txtInputs_Changed);
            // 
            // btnFirstWeight
            // 
            this.btnFirstWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnFirstWeight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFirstWeight.Font = new System.Drawing.Font("Noto Sans Arabic", 11F, System.Drawing.FontStyle.Bold);
            this.btnFirstWeight.Location = new System.Drawing.Point(280, 248);
            this.btnFirstWeight.Name = "btnFirstWeight";
            this.btnFirstWeight.Size = new System.Drawing.Size(120, 32);
            this.btnFirstWeight.TabIndex = 14;
            this.btnFirstWeight.Text = "الوزن الأول";
            this.btnFirstWeight.UseVisualStyleBackColor = false;
            this.btnFirstWeight.Click += new System.EventHandler(this.btnFirstWeight_Click);
            // 
            // btnSecondWeight
            // 
            this.btnSecondWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(210)))), ((int)(((byte)(100)))));
            this.btnSecondWeight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSecondWeight.Font = new System.Drawing.Font("Noto Sans Arabic", 11F, System.Drawing.FontStyle.Bold);
            this.btnSecondWeight.Location = new System.Drawing.Point(150, 248);
            this.btnSecondWeight.Name = "btnSecondWeight";
            this.btnSecondWeight.Size = new System.Drawing.Size(120, 32);
            this.btnSecondWeight.TabIndex = 15;
            this.btnSecondWeight.Text = "الوزن الثاني";
            this.btnSecondWeight.UseVisualStyleBackColor = false;
            this.btnSecondWeight.Click += new System.EventHandler(this.btnSecondWeight_Click);
            // 
            // btnDailySettings
            // 
            this.btnDailySettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnDailySettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDailySettings.Font = new System.Drawing.Font("Noto Sans Arabic", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnDailySettings.Location = new System.Drawing.Point(20, 248);
            this.btnDailySettings.Name = "btnDailySettings";
            this.btnDailySettings.Size = new System.Drawing.Size(120, 32);
            this.btnDailySettings.TabIndex = 16;
            this.btnDailySettings.Text = "ضبط اليوم/النول";
            this.btnDailySettings.UseVisualStyleBackColor = false;
            this.btnDailySettings.Click += new System.EventHandler(this.btnDailySettings_Click);
            // 
            // pendingQueueGroup
            // 
            this.pendingQueueGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.pendingQueueGroup.Controls.Add(this.dtpPendingDate);
            this.pendingQueueGroup.Controls.Add(this.gridPending);
            this.pendingQueueGroup.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.pendingQueueGroup.Location = new System.Drawing.Point(260, 353);
            this.pendingQueueGroup.Name = "pendingQueueGroup";
            this.pendingQueueGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pendingQueueGroup.Size = new System.Drawing.Size(278, 285);
            this.pendingQueueGroup.TabIndex = 4;
            this.pendingQueueGroup.TabStop = false;
            this.pendingQueueGroup.Text = "سيارات لم تصفى الوزن بتاريخ";
            // 
            // dtpPendingDate
            // 
            this.dtpPendingDate.CustomFormat = "yyyy/MM/dd";
            this.dtpPendingDate.Font = new System.Drawing.Font("Noto Sans Arabic", 9.5F);
            this.dtpPendingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPendingDate.Location = new System.Drawing.Point(10, 24);
            this.dtpPendingDate.Name = "dtpPendingDate";
            this.dtpPendingDate.Size = new System.Drawing.Size(258, 26);
            this.dtpPendingDate.TabIndex = 0;
            this.dtpPendingDate.ValueChanged += new System.EventHandler(this.dtpPendingDate_ValueChanged);
            // 
            // gridPending
            // 
            this.gridPending.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPlate,
            this.colFirstWeight,
            this.colFirstTime});
            this.gridPending.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.gridPending.FullRowSelect = true;
            this.gridPending.GridLines = true;
            this.gridPending.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.gridPending.Location = new System.Drawing.Point(10, 56);
            this.gridPending.MultiSelect = false;
            this.gridPending.Name = "gridPending";
            this.gridPending.Size = new System.Drawing.Size(258, 222);
            this.gridPending.TabIndex = 1;
            this.gridPending.UseCompatibleStateImageBehavior = false;
            this.gridPending.View = System.Windows.Forms.View.Details;
            this.gridPending.DoubleClick += new System.EventHandler(this.gridPending_DoubleClick);
            // 
            // colPlate
            // 
            this.colPlate.Text = "رقم السيارة";
            this.colPlate.Width = 90;
            // 
            // colFirstWeight
            // 
            this.colFirstWeight.Text = "الوزن الأول";
            this.colFirstWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colFirstWeight.Width = 85;
            // 
            // colFirstTime
            // 
            this.colFirstTime.Text = "الوقت";
            this.colFirstTime.Width = 75;
            // 
            // scaleMonitorGroup
            // 
            this.scaleMonitorGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.scaleMonitorGroup.Controls.Add(this.panelLiveWeight);
            this.scaleMonitorGroup.Controls.Add(this.ledStability);
            this.scaleMonitorGroup.Controls.Add(this.lblSearchPlate);
            this.scaleMonitorGroup.Controls.Add(this.txtSearchPlate);
            this.scaleMonitorGroup.Controls.Add(this.btnSearchPlate);
            this.scaleMonitorGroup.Controls.Add(this.lblRxEvent);
            this.scaleMonitorGroup.Controls.Add(this.lblInputLen);
            this.scaleMonitorGroup.Controls.Add(this.lblScaleStatus);
            this.scaleMonitorGroup.Font = new System.Drawing.Font("Noto Sans Arabic", 10F, System.Drawing.FontStyle.Bold);
            this.scaleMonitorGroup.Location = new System.Drawing.Point(8, 353);
            this.scaleMonitorGroup.Name = "scaleMonitorGroup";
            this.scaleMonitorGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.scaleMonitorGroup.Size = new System.Drawing.Size(245, 285);
            this.scaleMonitorGroup.TabIndex = 5;
            this.scaleMonitorGroup.TabStop = false;
            this.scaleMonitorGroup.Text = "قراءة الميزان الحية";
            // 
            // panelLiveWeight
            // 
            this.panelLiveWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(210)))), ((int)(((byte)(0)))));
            this.panelLiveWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLiveWeight.Controls.Add(this.lblLiveWeightValue);
            this.panelLiveWeight.Controls.Add(this.lblLiveWeightUnit);
            this.panelLiveWeight.Location = new System.Drawing.Point(10, 24);
            this.panelLiveWeight.Name = "panelLiveWeight";
            this.panelLiveWeight.Size = new System.Drawing.Size(185, 48);
            this.panelLiveWeight.TabIndex = 0;
            // 
            // lblLiveWeightValue
            // 
            this.lblLiveWeightValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLiveWeightValue.Font = new System.Drawing.Font("Noto Sans Arabic", 18F, System.Drawing.FontStyle.Bold);
            this.lblLiveWeightValue.ForeColor = System.Drawing.Color.Black;
            this.lblLiveWeightValue.Location = new System.Drawing.Point(0, 0);
            this.lblLiveWeightValue.Name = "lblLiveWeightValue";
            this.lblLiveWeightValue.Size = new System.Drawing.Size(145, 46);
            this.lblLiveWeightValue.TabIndex = 0;
            this.lblLiveWeightValue.Text = "٤٣٥٤";
            this.lblLiveWeightValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLiveWeightUnit
            // 
            this.lblLiveWeightUnit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblLiveWeightUnit.Font = new System.Drawing.Font("Noto Sans Arabic", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLiveWeightUnit.ForeColor = System.Drawing.Color.Black;
            this.lblLiveWeightUnit.Location = new System.Drawing.Point(145, 0);
            this.lblLiveWeightUnit.Name = "lblLiveWeightUnit";
            this.lblLiveWeightUnit.Size = new System.Drawing.Size(38, 46);
            this.lblLiveWeightUnit.TabIndex = 1;
            this.lblLiveWeightUnit.Text = "كجم";
            this.lblLiveWeightUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ledStability
            // 
            this.ledStability.BackColor = System.Drawing.Color.Red;
            this.ledStability.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ledStability.Location = new System.Drawing.Point(205, 36);
            this.ledStability.Name = "ledStability";
            this.ledStability.Size = new System.Drawing.Size(24, 24);
            this.ledStability.TabIndex = 1;
            // 
            // lblSearchPlate
            // 
            this.lblSearchPlate.Location = new System.Drawing.Point(10, 85);
            this.lblSearchPlate.Name = "lblSearchPlate";
            this.lblSearchPlate.Size = new System.Drawing.Size(225, 24);
            this.lblSearchPlate.TabIndex = 2;
            this.lblSearchPlate.Text = "البحث - رقم السيارة";
            // 
            // txtSearchPlate
            // 
            this.txtSearchPlate.Font = new System.Drawing.Font("Noto Sans Arabic", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtSearchPlate.Location = new System.Drawing.Point(60, 112);
            this.txtSearchPlate.Name = "txtSearchPlate";
            this.txtSearchPlate.Size = new System.Drawing.Size(175, 28);
            this.txtSearchPlate.TabIndex = 3;
            // 
            // btnSearchPlate
            // 
            this.btnSearchPlate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnSearchPlate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearchPlate.Font = new System.Drawing.Font("Noto Sans Arabic", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearchPlate.Location = new System.Drawing.Point(10, 112);
            this.btnSearchPlate.Name = "btnSearchPlate";
            this.btnSearchPlate.Size = new System.Drawing.Size(45, 28);
            this.btnSearchPlate.TabIndex = 4;
            this.btnSearchPlate.Text = "بحث";
            this.btnSearchPlate.UseVisualStyleBackColor = false;
            this.btnSearchPlate.Click += new System.EventHandler(this.btnSearchPlate_Click);
            // 
            // lblRxEvent
            // 
            this.lblRxEvent.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.lblRxEvent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblRxEvent.Location = new System.Drawing.Point(10, 165);
            this.lblRxEvent.Name = "lblRxEvent";
            this.lblRxEvent.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblRxEvent.Size = new System.Drawing.Size(225, 20);
            this.lblRxEvent.TabIndex = 5;
            this.lblRxEvent.Text = "Fire Rx Event Every: 12";
            // 
            // lblInputLen
            // 
            this.lblInputLen.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold);
            this.lblInputLen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblInputLen.Location = new System.Drawing.Point(10, 190);
            this.lblInputLen.Name = "lblInputLen";
            this.lblInputLen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblInputLen.Size = new System.Drawing.Size(225, 20);
            this.lblInputLen.TabIndex = 6;
            this.lblInputLen.Text = "InputLen: 0";
            // 
            // lblScaleStatus
            // 
            this.lblScaleStatus.Font = new System.Drawing.Font("Noto Sans Arabic", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblScaleStatus.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblScaleStatus.Location = new System.Drawing.Point(10, 225);
            this.lblScaleStatus.Name = "lblScaleStatus";
            this.lblScaleStatus.Size = new System.Drawing.Size(225, 45);
            this.lblScaleStatus.TabIndex = 7;
            this.lblScaleStatus.Text = "متصل بالمنفذ COM1";
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(600, 500);
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDocument;
            this.printDialog.UseEXDialog = true;
            // 
            // pageSetupDialog
            // 
            this.pageSetupDialog.Document = this.printDocument;
            // 
            // WeighBridgeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ClientSize = new System.Drawing.Size(964, 646);
            this.Controls.Add(this.scaleMonitorGroup);
            this.Controls.Add(this.pendingQueueGroup);
            this.Controls.Add(this.vehicleInputGroup);
            this.Controls.Add(this.ticketPreviewPanel);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "WeighBridgeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تسجيل الاوزان من ميزان الكتروني - ميزان بسكول ابو السعد ۱۲۰ طن";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WeighBridgeForm_FormClosing);
            this.Load += new System.EventHandler(this.WeighBridgeForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ticketPreviewPanel.ResumeLayout(false);
            this.ticketTablePanel.ResumeLayout(false);
            this.weightsTablePanel.ResumeLayout(false);
            this.vehicleInputGroup.ResumeLayout(false);
            this.vehicleInputGroup.PerformLayout();
            this.pendingQueueGroup.ResumeLayout(false);
            this.scaleMonitorGroup.ResumeLayout(false);
            this.scaleMonitorGroup.PerformLayout();
            this.panelLiveWeight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuUsers;
        private System.Windows.Forms.ToolStripMenuItem menuWeights;
        private System.Windows.Forms.ToolStripMenuItem menuCustomers;
        private System.Windows.Forms.ToolStripMenuItem menuDatabase;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.ToolStripMenuItem menuPrint;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsBtnUsers;
        private System.Windows.Forms.ToolStripButton tsBtnWeights;
        private System.Windows.Forms.ToolStripButton tsBtnCustomers;
        private System.Windows.Forms.ToolStripButton tsBtnDatabase;
        private System.Windows.Forms.ToolStripButton tsBtnPrint;
        private System.Windows.Forms.ToolStripButton tsBtnExit;
        private System.Windows.Forms.Panel ticketPreviewPanel;
        private System.Windows.Forms.Label lblTicketTitle;
        private System.Windows.Forms.Label lblTicketSubtitle;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Panel ticketTablePanel;
        private System.Windows.Forms.Label lblTagCustomer;
        private System.Windows.Forms.Label lblValCustomer;
        private System.Windows.Forms.Label lblTagDriver;
        private System.Windows.Forms.Label lblValDriver;
        private System.Windows.Forms.Label lblTagPlate;
        private System.Windows.Forms.Label lblValPlate;
        private System.Windows.Forms.Label lblTagTrailer;
        private System.Windows.Forms.Label lblValTrailer;
        private System.Windows.Forms.Label lblTagCargo;
        private System.Windows.Forms.Label lblValCargo;
        private System.Windows.Forms.Label lblTagGovernorate;
        private System.Windows.Forms.Label lblValGovernorate;
        private System.Windows.Forms.Panel weightsTablePanel;
        private System.Windows.Forms.Label lblTagFirstWeight;
        private System.Windows.Forms.Label lblValFirstWeight;
        private System.Windows.Forms.Label lblTagFirstTime;
        private System.Windows.Forms.Label lblValFirstTime;
        private System.Windows.Forms.Label lblTagFirstDate;
        private System.Windows.Forms.Label lblValFirstDate;
        private System.Windows.Forms.Label lblTagSecondWeight;
        private System.Windows.Forms.Label lblValSecondWeight;
        private System.Windows.Forms.Label lblTagSecondTime;
        private System.Windows.Forms.Label lblValSecondTime;
        private System.Windows.Forms.Label lblTagSecondDate;
        private System.Windows.Forms.Label lblValSecondDate;
        private System.Windows.Forms.Label lblTagNetWeight;
        private System.Windows.Forms.Label lblValNetWeight;
        private System.Windows.Forms.Label lblTicketFooter;
        private System.Windows.Forms.GroupBox vehicleInputGroup;
        private System.Windows.Forms.Button btnPrevRecord;
        private System.Windows.Forms.Button btnNextRecord;
        private System.Windows.Forms.Label lblInputCustomer;
        private System.Windows.Forms.ComboBox txtCustomerName;
        private System.Windows.Forms.Label lblInputPlate;
        private System.Windows.Forms.TextBox txtCarPlate;
        private System.Windows.Forms.Label lblInputCargo;
        private System.Windows.Forms.ComboBox txtCargoType;
        private System.Windows.Forms.Label lblInputGov;
        private System.Windows.Forms.ComboBox txtGovernorate;
        private System.Windows.Forms.Label lblInputDriver;
        private System.Windows.Forms.TextBox txtDriverName;
        private System.Windows.Forms.Label lblInputTrailer;
        private System.Windows.Forms.TextBox txtTrailerNo;
        private System.Windows.Forms.Button btnFirstWeight;
        private System.Windows.Forms.Button btnSecondWeight;
        private System.Windows.Forms.Button btnDailySettings;
        private System.Windows.Forms.GroupBox pendingQueueGroup;
        private System.Windows.Forms.DateTimePicker dtpPendingDate;
        private System.Windows.Forms.ListView gridPending;
        private System.Windows.Forms.ColumnHeader colPlate;
        private System.Windows.Forms.ColumnHeader colFirstWeight;
        private System.Windows.Forms.ColumnHeader colFirstTime;
        private System.Windows.Forms.GroupBox scaleMonitorGroup;
        private System.Windows.Forms.Panel panelLiveWeight;
        private System.Windows.Forms.Label lblLiveWeightValue;
        private System.Windows.Forms.Label lblLiveWeightUnit;
        private System.Windows.Forms.Panel ledStability;
        private System.Windows.Forms.Label lblSearchPlate;
        private System.Windows.Forms.TextBox txtSearchPlate;
        private System.Windows.Forms.Button btnSearchPlate;
        private System.Windows.Forms.Label lblRxEvent;
        private System.Windows.Forms.Label lblInputLen;
        private System.Windows.Forms.Label lblScaleStatus;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog;
    }
}
