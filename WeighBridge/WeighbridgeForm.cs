using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;

namespace Weighbridge
{
    public partial class WeighBridgeForm : Form
    {
        private readonly DatabaseService _db = DatabaseService.Instance;
        private readonly ScaleProtocolEngine _scale = ScaleProtocolEngine.Instance;
        private readonly HelperFunction _helper = new HelperFunction();

        private WeighingRecord _currentRecord;
        private int _liveWeight = 0;
        private bool _isStable = true;

        public WeighBridgeForm()
        {
            InitializeComponent();
        }

        private void WeighBridgeForm_Load(object sender, EventArgs e)
        {
            ApplyBranding();
            LoadGovernorates();
            LoadCustomerAutocomplete();
            RefreshPendingQueue();
            LoadLatestOrNewRecord();

            // Wire up scale protocol engine
            _scale.OnWeightChanged += Scale_OnWeightChanged;
            _scale.OnStatusChanged += Scale_OnStatusChanged;
            _scale.OnError += Scale_OnError;
            _scale.Start();

            // Print setup
            SetupPrintDocument();
        }

        private void WeighBridgeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _scale.Stop();
        }

        #region Scale Integration
        private void Scale_OnWeightChanged(int weight, bool isStable, bool isNegative)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Scale_OnWeightChanged(weight, isStable, isNegative)));
                return;
            }

            _liveWeight = weight;
            _isStable = isStable;

            if (isNegative)
            {
                panelLiveWeight.BackColor = Color.FromArgb(255, 120, 0);
                lblLiveWeightValue.Text = $"{_liveWeight}";
                lblScaleStatus.Text = $"تحذير: قراءة سالبة ({_liveWeight} كجم) - يلزم تصفير الميزان";
                lblScaleStatus.ForeColor = Color.DarkOrange;
            }
            else
            {
                panelLiveWeight.BackColor = Color.FromArgb(0, 210, 0);
                lblLiveWeightValue.Text = _liveWeight.ToString();
            }

            ledStability.BackColor = _isStable ? Color.FromArgb(0, 220, 0) : Color.Red;
        }
        private void Scale_OnStatusChanged(string status)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Scale_OnStatusChanged(status)));
                return;
            }

            lblScaleStatus.Text = status;
            lblScaleStatus.ForeColor = Color.DarkGreen;
        }

        private void Scale_OnError(string error)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => Scale_OnError(error)));
                return;
            }

            lblScaleStatus.Text = error;
            lblScaleStatus.ForeColor = Color.Red;
        }
        #endregion

        #region Branding and Setup
        private void ApplyBranding()
        {
            lblTicketTitle.Text = _db.GetSetting("CompanyName", "ميزان بسكول ابو السعد ۱۲۰ طن");
            lblTicketSubtitle.Text = _db.GetSetting("CompanySubtitle", "العنوان فوه كفر الشيخ ٠١٠٩٢١٨٠١٤٦ ايمن / ٠١٠٦٢٥٥١١٨٩ محمد");
            lblTicketFooter.Text = _db.GetSetting("FooterText", "هذا البرنامج صنع خصيصا لشركة اولاد الغلباني الحديثة بدمنهور");
            this.Text = $"تسجيل الاوزان من ميزان الكتروني - {lblTicketTitle.Text}";
            
            lblRxEvent.Text = $"Fire Rx Event Every: {_db.GetSetting("RxEventInterval", "12")}";
            lblInputLen.Text = $"InputLen: {_db.GetSetting("InputLen", "0")}";
        }

        private void LoadGovernorates()
        {
            string defaultGov = _db.GetSetting("DefaultGovernorate", "كفر الشيخ");
            if (txtGovernorate.Items.Contains(defaultGov))
            {
                txtGovernorate.SelectedItem = defaultGov;
            }
        }

        private void LoadCustomerAutocomplete()
        {
            txtCustomerName.Items.Clear();
            var names = _db.GetCustomerNames();
            foreach (var n in names)
            {
                txtCustomerName.Items.Add(n);
            }
        }
        #endregion

        #region Record Management
        private void LoadLatestOrNewRecord()
        {
            var latest = _db.GetLatestRecord();
            if (latest != null)
            {
                DisplayRecordOnTicket(latest);
            }
            else
            {
                PrepareNewRecord();
            }
        }

        private void PrepareNewRecord()
        {
            _currentRecord = new WeighingRecord
            {
                TicketNo = _db.GetNextTicketNo(),
                Governorate = txtGovernorate.Text,
                Status = "Pending"
            };

            ClearInputFields();
            lblSerial.Text = $"مسلسل: {_currentRecord.TicketNo}-";
            lblValCustomer.Text = "-";
            lblValPlate.Text = "-";
            lblValDriver.Text = "-";
            lblValTrailer.Text = "-";
            lblValCargo.Text = "-";
            lblValGovernorate.Text = txtGovernorate.Text;
            lblValFirstWeight.Text = "0";
            lblValFirstTime.Text = "-";
            lblValFirstDate.Text = "-";
            lblValSecondWeight.Text = "0";
            lblValSecondTime.Text = "-";
            lblValSecondDate.Text = "-";
            lblValNetWeight.Text = "0 كجم";

            btnFirstWeight.Enabled = true;
            btnSecondWeight.Enabled = false;
            btnSecondWeight.BackColor = Color.FromArgb(235, 240, 240);
        }

        private void DisplayRecordOnTicket(WeighingRecord r)
        {
            if (r == null) return;
            _currentRecord = r;

            lblSerial.Text = $"مسلسل: {r.TicketNo}-";
            lblValCustomer.Text = string.IsNullOrEmpty(r.CustomerName) ? "-" : r.CustomerName;
            lblValPlate.Text = string.IsNullOrEmpty(r.CarPlate) ? "-" : r.CarPlate;
            lblValDriver.Text = string.IsNullOrEmpty(r.DriverName) ? "-" : r.DriverName;
            lblValTrailer.Text = string.IsNullOrEmpty(r.TrailerNo) ? "-" : r.TrailerNo;
            lblValCargo.Text = string.IsNullOrEmpty(r.CargoType) ? "-" : r.CargoType;
            lblValGovernorate.Text = string.IsNullOrEmpty(r.Governorate) ? "-" : r.Governorate;

            lblValFirstWeight.Text = r.FirstWeight.ToString();
            lblValFirstTime.Text = string.IsNullOrEmpty(r.FirstTime) ? "-" : r.FirstTime;
            lblValFirstDate.Text = string.IsNullOrEmpty(r.FirstDate) ? "-" : r.FirstDate;

            lblValSecondWeight.Text = r.SecondWeight.ToString();
            lblValSecondTime.Text = string.IsNullOrEmpty(r.SecondTime) ? "-" : r.SecondTime;
            lblValSecondDate.Text = string.IsNullOrEmpty(r.SecondDate) ? "-" : r.SecondDate;

            lblValNetWeight.Text = $"{r.NetWeight} كجم";
            // Populate form inputs
            txtCustomerName.Text = r.CustomerName;
            txtCarPlate.Text = r.CarPlate;
            txtCargoType.Text = r.CargoType;
            txtGovernorate.Text = r.Governorate;
            txtDriverName.Text = r.DriverName;
            txtTrailerNo.Text = r.TrailerNo;

            if (r.Status == "Pending")
            {
                btnFirstWeight.Enabled = false;
                btnSecondWeight.Enabled = true;
                btnSecondWeight.BackColor = Color.FromArgb(100, 210, 100);
            }
            else
            {
                btnFirstWeight.Enabled = true;
                btnSecondWeight.Enabled = false;
                btnSecondWeight.BackColor = Color.FromArgb(235, 240, 240);
            }
        }

        private void ClearInputFields()
        {
            txtCustomerName.Text = "";
            txtCarPlate.Text = "";
            txtCargoType.Text = "";
            txtDriverName.Text = "";
            txtTrailerNo.Text = "";
        }
        #endregion

        #region Actions (First Weight, Second Weight, Queue)
        private void btnFirstWeight_Click(object sender, EventArgs e)
        {
            string plate = txtCarPlate.Text.Trim();
            if (string.IsNullOrEmpty(plate))
            {
                _helper.CreateMessageBox("تنبيه", "يرجى إدخال رقم لوحة السيارة أولاً.");
                txtCarPlate.Focus();
                return;
            }

            int weight = _liveWeight;
            if (weight < 0)
            {
                _helper.CreateMessageBox("تحذير", $"قراءة الميزان سالبة ({weight} كجم).\nلا يمكن تسجيل وزنة دخول بقيمة سالبة!\nيرجى التحقق من خلو منصة الميزان وتصفير المؤشر.");
                return;
            }

            if (weight == 0)
            {
                var res = MessageBox.Show("تنبيه: وزن الميزان الحالي يساوي صفراً (0 كجم).\nهل أنت متأكد من تسجيل وزنة بدون حمولة على الميزان؟", "تأكيد الوزن الصفري", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res != DialogResult.Yes) return;
            }

            DateTime now = DateTime.Now;
            string dateStr = now.ToString("yyyy/MM/dd");
            string timeStr = now.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-EG"));

            var record = new WeighingRecord
            {
                TicketNo = _db.GetNextTicketNo(),
                CustomerName = txtCustomerName.Text.Trim(),
                CarPlate = plate,
                TrailerNo = txtTrailerNo.Text.Trim(),
                DriverName = txtDriverName.Text.Trim(),
                CargoType = txtCargoType.Text.Trim(),
                Governorate = txtGovernorate.Text.Trim(),
                FirstWeight = weight,
                FirstDate = dateStr,
                FirstTime = timeStr,
                Status = "Pending"
            };

            int ticketNo = _db.SaveFirstWeight(record);
            record.TicketNo = ticketNo;

            DisplayRecordOnTicket(record);
            RefreshPendingQueue();
            LoadCustomerAutocomplete();

            _helper.CreateMessageBox("نجاح", $"تم تسجيل الوزن الأول بنجاح للسيارة ({plate}) برقم تذكرة ({ticketNo}).");
        }
        private void btnSecondWeight_Click(object sender, EventArgs e)
        {
            if (_currentRecord == null || _currentRecord.Status != "Pending")
            {
                _helper.CreateMessageBox("تنبيه", "يرجى اختيار سيارة معلقة من القائمة لتسجيل الوزن الثاني.");
                return;
            }

            int secondWeight = _liveWeight;
            if (secondWeight < 0)
            {
                _helper.CreateMessageBox("تحذير", $"قراءة الميزان سالبة ({secondWeight} كجم).\nلا يمكن تسجيل وزنة خروج بقيمة سالبة!\nيرجى إعادة تصفير الميزان.");
                return;
            }

            if (secondWeight == 0)
            {
                var res = MessageBox.Show("تنبيه: وزن الميزان الحالي يساوي صفراً (0 كجم).\nهل أنت متأكد من تصفية الوزن بقيمة خروج صفرية؟", "تأكيد الوزن الصفري", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res != DialogResult.Yes) return;
            }

            int netWeight = Math.Abs(_currentRecord.FirstWeight - secondWeight);

            DateTime now = DateTime.Now;
            string dateStr = now.ToString("yyyy/MM/dd");
            string timeStr = now.ToString("hh:mm:ss tt", CultureInfo.CreateSpecificCulture("ar-EG"));

            bool ok = _db.SaveSecondWeight(_currentRecord.TicketNo, secondWeight, dateStr, timeStr, netWeight, _currentRecord.Fee, _currentRecord.Notes);
            if (ok)
            {
                _currentRecord.SecondWeight = secondWeight;
                _currentRecord.SecondDate = dateStr;
                _currentRecord.SecondTime = timeStr;
                _currentRecord.NetWeight = netWeight;
                _currentRecord.Status = "Completed";

                DisplayRecordOnTicket(_currentRecord);
                RefreshPendingQueue();

                _helper.CreateMessageBox("نجاح", $"تمت تصفية وزن السيارة ({_currentRecord.CarPlate}) بنجاح!\nالوزن الصافي: {netWeight} كجم.");

                // Prompt print preview
                printPreviewDialog.ShowDialog();
            }
            else
            {
                _helper.CreateMessageBox("خطأ", "حدث خطأ أثناء حفظ الوزن الثاني في قاعدة البيانات.");
            }
        }

        private void RefreshPendingQueue()
        {
            string dateFilter = dtpPendingDate.Value.ToString("yyyy/MM/dd");
            var dt = _db.GetPendingWeighings(dateFilter);

            gridPending.Items.Clear();
            foreach (DataRow row in dt.Rows)
            {
                var item = new ListViewItem(row["CarPlate"].ToString());
                item.SubItems.Add(row["FirstWeight"].ToString());
                item.SubItems.Add(row["FirstTime"].ToString());
                item.Tag = Convert.ToInt32(row["TicketNo"]);
                gridPending.Items.Add(item);
            }
        }

        private void gridPending_DoubleClick(object sender, EventArgs e)
        {
            if (gridPending.SelectedItems.Count > 0)
            {
                int ticketNo = (int)gridPending.SelectedItems[0].Tag;
                var record = _db.GetWeighingByTicket(ticketNo);
                if (record != null)
                {
                    DisplayRecordOnTicket(record);
                }
            }
        }

        private void dtpPendingDate_ValueChanged(object sender, EventArgs e)
        {
            RefreshPendingQueue();
        }

        private void btnSearchPlate_Click(object sender, EventArgs e)
        {
            string query = txtSearchPlate.Text.Trim();
            if (string.IsNullOrEmpty(query)) return;

            var pending = _db.GetPendingWeighingByPlate(query);
            if (pending != null)
            {
                DisplayRecordOnTicket(pending);
                return;
            }

            var all = _db.GetAllWeighings(query);
            if (all.Rows.Count > 0)
            {
                int ticketNo = Convert.ToInt32(all.Rows[0]["رقم التذكرة"]);
                var rec = _db.GetWeighingByTicket(ticketNo);
                if (rec != null)
                {
                    DisplayRecordOnTicket(rec);
                    return;
                }
            }

            _helper.CreateMessageBox("تنبيه", $"لم يتم العثور على سجلات للسيارة ({query}).");
        }

        private void btnPrevRecord_Click(object sender, EventArgs e)
        {
            if (_currentRecord == null) return;
            var prev = _db.GetPreviousRecord(_currentRecord.TicketNo);
            if (prev != null)
            {
                DisplayRecordOnTicket(prev);
            }
        }

        private void btnNextRecord_Click(object sender, EventArgs e)
        {
            if (_currentRecord == null) return;
            var next = _db.GetNextRecord(_currentRecord.TicketNo);
            if (next != null)
            {
                DisplayRecordOnTicket(next);
            }
        }

        private void txtInputs_Changed(object sender, EventArgs e)
        {
            lblValCustomer.Text = string.IsNullOrEmpty(txtCustomerName.Text) ? "-" : txtCustomerName.Text;
            lblValPlate.Text = string.IsNullOrEmpty(txtCarPlate.Text) ? "-" : txtCarPlate.Text;
            lblValCargo.Text = string.IsNullOrEmpty(txtCargoType.Text) ? "-" : txtCargoType.Text;
            lblValGovernorate.Text = string.IsNullOrEmpty(txtGovernorate.Text) ? "-" : txtGovernorate.Text;
            lblValDriver.Text = string.IsNullOrEmpty(txtDriverName.Text) ? "-" : txtDriverName.Text;
            lblValTrailer.Text = string.IsNullOrEmpty(txtTrailerNo.Text) ? "-" : txtTrailerNo.Text;
        }

        private void btnDailySettings_Click(object sender, EventArgs e)
        {
            menuDatabase_Click(sender, e);
        }
        #endregion

        #region Menu Navigation
        private void menuUsers_Click(object sender, EventArgs e)
        {
            _helper.CreateMessageBox("معلومات", "إدارة المستخدمين: حساب المسؤول نشط.");
        }

        private void menuWeights_Click(object sender, EventArgs e)
        {
            var historyForm = new HistoryForm();
            historyForm.ShowDialog();
        }

        private void menuCustomers_Click(object sender, EventArgs e)
        {
            var custForm = new CustomerAccountsForm();
            custForm.ShowDialog();
        }

        private void menuDatabase_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                ApplyBranding();
                LoadGovernorates();
                _scale.Start();
            }
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            _helper.CreateMessageBox("عن البرنامج", "نظام تسجيل الأوزان من ميزان إلكتروني شاحنات (بسكول)\nالإصدار: 2.0 (عربي كامل)\nيدعم العمل الميداني وقواعد بيانات SQLite والأجهزة التسلسلية.");
        }

        private void menuPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog.ShowDialog();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Printing
        private void SetupPrintDocument()
        {
            PaperSize paperSize = new PaperSize("CustomScaleTicket", 650, 450);
            paperSize.RawKind = (int)PaperKind.Custom;

            printDocument.DefaultPageSettings.PaperSize = paperSize;
            printDocument.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            printDocument.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font fontHeader = new Font("Noto Sans Arabic", 16, FontStyle.Bold);
            Font fontSub = new Font("Noto Sans Arabic", 10, FontStyle.Bold);
            Font fontRegular = new Font("Noto Sans Arabic", 10, FontStyle.Regular);
            Font fontBold = new Font("Noto Sans Arabic", 10, FontStyle.Bold);
            Font fontLargeBold = new Font("Noto Sans Arabic", 12, FontStyle.Bold);

            SolidBrush brushBlack = new SolidBrush(Color.Black);
            SolidBrush brushNavy = new SolidBrush(Color.Navy);
            SolidBrush brushRed = new SolidBrush(Color.DarkRed);
            SolidBrush brushGreen = new SolidBrush(Color.DarkGreen);
            Pen penBlack = new Pen(Color.Black, 1.5f);

            int startX = 25;
            int startY = 20;
            int width = 590;

            // Border Box
            g.DrawRectangle(penBlack, startX, startY, width, 380);

            // Title & Header
            string title = lblTicketTitle.Text;
            string subtitle = lblTicketSubtitle.Text;
            g.DrawString(title, fontHeader, brushBlack, new RectangleF(startX, startY + 5, width, 35), new StringFormat { Alignment = StringAlignment.Center });
            g.DrawString(subtitle, fontSub, brushBlack, new RectangleF(startX, startY + 40, width, 25), new StringFormat { Alignment = StringAlignment.Center });

            int ticketNo = _currentRecord != null ? _currentRecord.TicketNo : 1;
            g.DrawString($"مسلسل: {ticketNo}-", fontBold, brushBlack, startX + 15, startY + 15);

            g.DrawLine(penBlack, startX, startY + 70, startX + width, startY + 70);

            // Info Table
            int rowY = startY + 80;
            int rightColX = startX + width - 15;

            var sfRight = new StringFormat { Alignment = StringAlignment.Far };
            var sfLeft = new StringFormat { Alignment = StringAlignment.Near };

            // Row 1
            g.DrawString("اسم العميل:", fontBold, brushBlack, rightColX, rowY, sfRight);
            g.DrawString(_currentRecord?.CustomerName ?? "-", fontBold, brushNavy, rightColX - 90, rowY, sfRight);

            g.DrawString("اسم السائق:", fontBold, brushBlack, startX + 260, rowY, sfRight);
            g.DrawString(_currentRecord?.DriverName ?? "-", fontRegular, brushBlack, startX + 170, rowY, sfRight);

            // Row 2
            rowY += 28;
            g.DrawString("رقم السيارة:", fontBold, brushBlack, rightColX, rowY, sfRight);
            g.DrawString(_currentRecord?.CarPlate ?? "-", fontLargeBold, brushRed, rightColX - 90, rowY, sfRight);

            g.DrawString("رقم المقطورة:", fontBold, brushBlack, startX + 260, rowY, sfRight);
            g.DrawString(_currentRecord?.TrailerNo ?? "-", fontRegular, brushBlack, startX + 170, rowY, sfRight);

            // Row 3
            rowY += 28;
            g.DrawString("نوع الشحنة:", fontBold, brushBlack, rightColX, rowY, sfRight);
            g.DrawString(_currentRecord?.CargoType ?? "-", fontRegular, brushBlack, rightColX - 90, rowY, sfRight);

            g.DrawString("المحافظة:", fontBold, brushBlack, startX + 260, rowY, sfRight);
            g.DrawString(_currentRecord?.Governorate ?? "كفر الشيخ", fontRegular, brushBlack, startX + 170, rowY, sfRight);

            // Separator line before weights
            rowY += 35;
            g.DrawLine(penBlack, startX, rowY, startX + width, rowY);

            // Weights Table
            rowY += 10;
            g.DrawString("الوزن الأول:", fontBold, brushBlack, rightColX, rowY, sfRight);
            g.DrawString($"{_currentRecord?.FirstWeight ?? 0}", fontLargeBold, brushGreen, rightColX - 85, rowY, sfRight);
            g.DrawString("الوقت:", fontBold, brushBlack, rightColX - 180, rowY, sfRight);
            g.DrawString(_currentRecord?.FirstTime ?? "-", fontRegular, brushBlack, rightColX - 230, rowY, sfRight);
            g.DrawString("التاريخ:", fontBold, brushBlack, rightColX - 350, rowY, sfRight);
            g.DrawString(_currentRecord?.FirstDate ?? "-", fontRegular, brushBlack, rightColX - 405, rowY, sfRight);

            rowY += 30;
            g.DrawString("الوزن الثاني:", fontBold, brushBlack, rightColX, rowY, sfRight);
            g.DrawString($"{_currentRecord?.SecondWeight ?? 0}", fontLargeBold, brushNavy, rightColX - 85, rowY, sfRight);
            g.DrawString("الوقت:", fontBold, brushBlack, rightColX - 180, rowY, sfRight);
            g.DrawString(_currentRecord?.SecondTime ?? "-", fontRegular, brushBlack, rightColX - 230, rowY, sfRight);
            g.DrawString("التاريخ:", fontBold, brushBlack, rightColX - 350, rowY, sfRight);
            g.DrawString(_currentRecord?.SecondDate ?? "-", fontRegular, brushBlack, rightColX - 405, rowY, sfRight);

            rowY += 30;
            g.DrawString("صافي الوزن:", fontLargeBold, brushBlack, rightColX, rowY, sfRight);
            g.DrawString($"{_currentRecord?.NetWeight ?? 0} كجم", new Font("Noto Sans Arabic", 14, FontStyle.Bold), brushRed, rightColX - 110, rowY - 2, sfRight);

            // Footer line
            rowY += 40;
            g.DrawLine(penBlack, startX, rowY, startX + width, rowY);
            g.DrawString(lblTicketFooter.Text, new Font("Noto Sans Arabic", 8, FontStyle.Regular), new SolidBrush(Color.FromArgb(80, 80, 80)), new RectangleF(startX, rowY + 5, width, 20), new StringFormat { Alignment = StringAlignment.Center });
        }
        #endregion
    }
}
