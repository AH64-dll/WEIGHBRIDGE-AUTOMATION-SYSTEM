using System;
using System.Drawing;
using System.Windows.Forms;

namespace Weighbridge
{
    public class CustomerAccountsForm : Form
    {
        private ListBox listCustomers;
        private TextBox txtName;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private TextBox txtBalance;
        private Button btnSave;
        private Button btnClose;

        public CustomerAccountsForm()
        {
            InitializeComponent();
            LoadCustomerList();
        }

        private void InitializeComponent()
        {
            this.Text = "إدارة حسابات وبيانات العملاء";
            this.Size = new Size(650, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Noto Sans Arabic", 10F);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.BackColor = Color.FromArgb(225, 240, 240);

            listCustomers = new ListBox
            {
                Location = new Point(380, 20),
                Size = new Size(230, 320),
                Font = new Font("Noto Sans Arabic", 10.5F, FontStyle.Bold)
            };
            listCustomers.SelectedIndexChanged += ListCustomers_SelectedIndexChanged;

            var grp = new GroupBox
            {
                Text = "بيانات العميل",
                Location = new Point(20, 20),
                Size = new Size(340, 320),
                Font = new Font("Noto Sans Arabic", 10F, FontStyle.Bold)
            };

            var lblName = new Label { Text = "اسم العميل:", Location = new Point(230, 35), AutoSize = true };
            txtName = new TextBox { Location = new Point(20, 32), Size = new Size(200, 27), Font = new Font("Noto Sans Arabic", 10F) };

            var lblPhone = new Label { Text = "رقم الهاتف:", Location = new Point(230, 80), AutoSize = true };
            txtPhone = new TextBox { Location = new Point(20, 77), Size = new Size(200, 27), Font = new Font("Noto Sans Arabic", 10F) };

            var lblAddr = new Label { Text = "العنوان:", Location = new Point(230, 125), AutoSize = true };
            txtAddress = new TextBox { Location = new Point(20, 122), Size = new Size(200, 27), Font = new Font("Noto Sans Arabic", 10F) };

            var lblBal = new Label { Text = "الرصيد الحسابي:", Location = new Point(230, 170), AutoSize = true };
            txtBalance = new TextBox { Location = new Point(20, 167), Size = new Size(200, 27), Font = new Font("Noto Sans Arabic", 10F), Text = "0" };

            btnSave = new Button
            {
                Text = "حفظ العميل",
                Location = new Point(110, 240),
                Size = new Size(110, 35),
                BackColor = Color.FromArgb(150, 220, 150),
                FlatStyle = FlatStyle.Popup
            };
            btnSave.Click += BtnSave_Click;

            btnClose = new Button
            {
                Text = "إغلاق",
                Location = new Point(20, 240),
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(235, 235, 235),
                FlatStyle = FlatStyle.Popup
            };
            btnClose.Click += (s, e) => this.Close();

            grp.Controls.AddRange(new Control[] { lblName, txtName, lblPhone, txtPhone, lblAddr, txtAddress, lblBal, txtBalance, btnSave, btnClose });

            this.Controls.Add(listCustomers);
            this.Controls.Add(grp);
        }

        private void LoadCustomerList()
        {
            listCustomers.Items.Clear();
            var names = DatabaseService.Instance.GetCustomerNames();
            foreach (var n in names)
            {
                listCustomers.Items.Add(n);
            }
        }

        private void ListCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listCustomers.SelectedItem != null)
            {
                txtName.Text = listCustomers.SelectedItem.ToString();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("يرجى إدخال اسم العميل.", "تنبيه");
                return;
            }

            double.TryParse(txtBalance.Text, out double bal);
            DatabaseService.Instance.AddOrUpdateCustomer(name, txtPhone.Text.Trim(), txtAddress.Text.Trim(), bal);
            MessageBox.Show("تم حفظ بيانات العميل بنجاح.", "نجاح");
            LoadCustomerList();
        }
    }
}
