using System;
using System.Windows.Forms;

namespace Weighbridge
{
    public partial class SettingsForm : Form
    {
        private readonly DatabaseService _db = DatabaseService.Instance;
        private readonly HelperFunction _helper = new HelperFunction();

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            cmbPortName.Text = _db.GetSetting("PortName", "COM1");
            cmbBaudRate.Text = _db.GetSetting("BaudRate", "9600");
            cmbParity.Text = _db.GetSetting("Parity", "None");
            cmbDataBits.Text = _db.GetSetting("DataBits", "8");
            cmbStopBits.Text = _db.GetSetting("StopBits", "One");
            cmbHandshake.Text = _db.GetSetting("Handshake", "None");

            string proto = _db.GetSetting("ScaleProtocol", "ContinuousASCII");
            int idx = cmbProtocol.FindString(proto);
            cmbProtocol.SelectedIndex = idx >= 0 ? idx : 0;

            chkSimulator.Checked = _db.GetSetting("SimulatorEnabled", "False").Equals("True", StringComparison.OrdinalIgnoreCase);

            txtCompanyName.Text = _db.GetSetting("CompanyName", "ميزان بسكول ابو السعد ۱۲۰ طن");
            txtSubtitle.Text = _db.GetSetting("CompanySubtitle", "العنوان فوه كفر الشيخ ٠١٠٩٢١٨٠١٤٦ ايمن / ٠١٠٦٢٥٥١١٨٩ محمد");
            txtFooterText.Text = _db.GetSetting("FooterText", "هذا البرنامج صنع خصيصا لشركة اولاد الغلباني الحديثة بدمنهور");
            txtDefaultGov.Text = _db.GetSetting("DefaultGovernorate", "كفر الشيخ");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _db.SaveSetting("PortName", cmbPortName.Text);
            _db.SaveSetting("BaudRate", cmbBaudRate.Text);
            _db.SaveSetting("Parity", cmbParity.Text);
            _db.SaveSetting("DataBits", cmbDataBits.Text);
            _db.SaveSetting("StopBits", cmbStopBits.Text);
            _db.SaveSetting("Handshake", cmbHandshake.Text);

            string protoSelected = cmbProtocol.SelectedItem?.ToString() ?? "ContinuousASCII";
            string protoKey = "ContinuousASCII";
            if (protoSelected.Contains("Toledo")) protoKey = "ToledoContinuous";
            else if (protoSelected.Contains("CAS")) protoKey = "CAS_Continuous";
            else if (protoSelected.Contains("Avery")) protoKey = "AveryWeighTronix";

            _db.SaveSetting("ScaleProtocol", protoKey);
            _db.SaveSetting("SimulatorEnabled", chkSimulator.Checked ? "True" : "False");

            _db.SaveSetting("CompanyName", txtCompanyName.Text.Trim());
            _db.SaveSetting("CompanySubtitle", txtSubtitle.Text.Trim());
            _db.SaveSetting("FooterText", txtFooterText.Text.Trim());
            _db.SaveSetting("DefaultGovernorate", txtDefaultGov.Text.Trim());

            _helper.CreateMessageBox("نجاح", "تم حفظ جميع الإعدادات بنجاح!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            cmbPortName.Text = "COM1";
            cmbBaudRate.Text = "9600";
            cmbParity.Text = "None";
            cmbDataBits.Text = "8";
            cmbStopBits.Text = "One";
            cmbHandshake.Text = "None";
            cmbProtocol.SelectedIndex = 0;
            chkSimulator.Checked = false;

            txtCompanyName.Text = "ميزان بسكول ابو السعد ۱۲۰ طن";
            txtSubtitle.Text = "العنوان فوه كفر الشيخ ٠١٠٩٢١٨٠١٤٦ ايمن / ٠١٠٦٢٥٥١١٨٩ محمد";
            txtFooterText.Text = "هذا البرنامج صنع خصيصا لشركة اولاد الغلباني الحديثة بدمنهور";
            txtDefaultGov.Text = "كفر الشيخ";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
