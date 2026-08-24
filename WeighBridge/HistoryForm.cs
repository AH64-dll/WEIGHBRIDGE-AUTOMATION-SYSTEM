using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Weighbridge
{
    public class HistoryForm : Form
    {
        private DataGridView grid;
        private TextBox txtSearch;
        private ComboBox cmbStatus;
        private Button btnSearch;
        private Button btnReprint;
        private Button btnClose;

        public HistoryForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "سجل الأوزان والتذاكر";
            this.Size = new Size(920, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Noto Sans Arabic", 10F);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.BackColor = Color.FromArgb(225, 240, 240);

            var panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10)
            };

            var lblSearch = new Label
            {
                Text = "بحث (لوحة / عميل / رقم):",
                Location = new Point(720, 18),
                AutoSize = true,
                Font = new Font("Noto Sans Arabic", 10F, FontStyle.Bold)
            };

            txtSearch = new TextBox
            {
                Location = new Point(500, 15),
                Width = 210,
                Font = new Font("Noto Sans Arabic", 10F)
            };

            var lblStatus = new Label
            {
                Text = "الحالة:",
                Location = new Point(440, 18),
                AutoSize = true,
                Font = new Font("Noto Sans Arabic", 10F, FontStyle.Bold)
            };

            cmbStatus = new ComboBox
            {
                Location = new Point(310, 15),
                Width = 120,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new object[] { "الكل", "معلقة (Pending)", "مكتملة (Completed)" });
            cmbStatus.SelectedIndex = 0;

            btnSearch = new Button
            {
                Text = "تطبيق البحث",
                Location = new Point(190, 13),
                Size = new Size(110, 32),
                BackColor = Color.FromArgb(180, 225, 180),
                FlatStyle = FlatStyle.Popup,
                Font = new Font("Noto Sans Arabic", 9.5F, FontStyle.Bold)
            };
            btnSearch.Click += (s, e) => LoadData();

            panelTop.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblStatus, cmbStatus, btnSearch });

            grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                Font = new Font("Noto Sans Arabic", 9.5F)
            };

            var panelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(10)
            };

            btnReprint = new Button
            {
                Text = "إعادة طباعة التذكرة",
                Location = new Point(740, 10),
                Size = new Size(150, 32),
                BackColor = Color.FromArgb(180, 220, 250),
                FlatStyle = FlatStyle.Popup,
                Font = new Font("Noto Sans Arabic", 9.5F, FontStyle.Bold)
            };
            btnReprint.Click += BtnReprint_Click;

            btnClose = new Button
            {
                Text = "إغلاق",
                Location = new Point(20, 10),
                Size = new Size(90, 32),
                BackColor = Color.FromArgb(240, 240, 240),
                FlatStyle = FlatStyle.Popup
            };
            btnClose.Click += (s, e) => this.Close();

            panelBottom.Controls.AddRange(new Control[] { btnReprint, btnClose });

            this.Controls.Add(grid);
            this.Controls.Add(panelTop);
            this.Controls.Add(panelBottom);
        }

        private void LoadData()
        {
            string status = "All";
            if (cmbStatus.SelectedIndex == 1) status = "Pending";
            if (cmbStatus.SelectedIndex == 2) status = "Completed";

            var dt = DatabaseService.Instance.GetAllWeighings(txtSearch.Text.Trim(), status);
            grid.DataSource = dt;
        }

        private void BtnReprint_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار تذكرة من الجدول لإعادة طباعتها.", "تنبيه");
                return;
            }

            int ticketNo = Convert.ToInt32(grid.SelectedRows[0].Cells["رقم التذكرة"].Value);
            var rec = DatabaseService.Instance.GetWeighingByTicket(ticketNo);
            if (rec != null)
            {
                MessageBox.Show($"سند الوزن رقم {rec.TicketNo}\nالعميل: {rec.CustomerName}\nالسيارة: {rec.CarPlate}\nالوزن الصافي: {rec.NetWeight} كجم\nجاهز للإرسال إلى الطابعة.", "معاينة السند");
            }
        }
    }
}
