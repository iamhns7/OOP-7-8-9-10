using System;
using System.Windows.Forms;

namespace Antivirus
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            numMaliciousThreshold.Value = Properties.Settings.Default.MaliciousThreshold;
            numSuspiciousThreshold.Value = Properties.Settings.Default.SuspiciousThreshold;
            chkAutoQuarantine.Checked = Properties.Settings.Default.AutoQuarantine;
            chkShowNotifications.Checked = Properties.Settings.Default.ShowNotifications;
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Eşik değerlerini kontrol et
            if (numMaliciousThreshold.Value <= numSuspiciousThreshold.Value)
            {
                MessageBox.Show(
                    "Zararlı eşik değeri, şüpheli eşik değerinden büyük olmalıdır.",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            Properties.Settings.Default.MaliciousThreshold = (int)numMaliciousThreshold.Value;
            Properties.Settings.Default.SuspiciousThreshold = (int)numSuspiciousThreshold.Value;
            Properties.Settings.Default.AutoQuarantine = chkAutoQuarantine.Checked;
            Properties.Settings.Default.ShowNotifications = chkShowNotifications.Checked;
            Properties.Settings.Default.Save();

            MessageBox.Show("Ayarlar kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnVarsayilan_Click(object sender, EventArgs e)
        {
            numMaliciousThreshold.Value = 70;
            numSuspiciousThreshold.Value = 40;
            chkAutoQuarantine.Checked = false;
            chkShowNotifications.Checked = true;
        }
    }
}

