using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Antivirus.Models;
using Antivirus.Services;

namespace Antivirus
{
    public partial class Form1 : Form
    {
        private ScannerService _scannerService;
        private QuarantineService _quarantineService;
        private ReportService _reportService;
        private CancellationTokenSource _cancellationTokenSource;
        private List<ScanResult> _currentResults;

        public Form1()
        {
            InitializeComponent();
            InitializeServices();
            SetupDataGridView();
        }

        private void InitializeServices()
        {
            // Ayarlardan eşik değerlerini al (varsayılan: 70, 40)
            int maliciousThreshold = Properties.Settings.Default.MaliciousThreshold;
            int suspiciousThreshold = Properties.Settings.Default.SuspiciousThreshold;
            
            _scannerService = new ScannerService(maliciousThreshold, suspiciousThreshold);
            _quarantineService = new QuarantineService();
            _reportService = new ReportService();
            _currentResults = new List<ScanResult>();
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DosyaYolu",
                HeaderText = "Dosya Yolu",
                DataPropertyName = "DosyaYolu",
                Width = 300,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Boyut",
                HeaderText = "Boyut",
                DataPropertyName = "BoyutFormatted",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Hash",
                HeaderText = "Hash",
                DataPropertyName = "Hash",
                Width = 200
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RiskSkoru",
                HeaderText = "Risk Skoru",
                DataPropertyName = "RiskSkoru",
                Width = 80
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Sonuc",
                HeaderText = "Sonuç",
                DataPropertyName = "SonucText",
                Width = 100
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Aksiyon",
                HeaderText = "Aksiyon",
                DataPropertyName = "AksiyonText",
                Width = 100
            });

            // Context menu ekle
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Karantinaya Al", null, (s, e) => QuarantineSelected());
            contextMenu.Items.Add("Sil", null, (s, e) => DeleteSelected());
            dataGridView1.ContextMenuStrip = contextMenu;
        }

        private void btnKlasorSec_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Taranacak klasörü seçin";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtKlasorYolu.Text = dialog.SelectedPath;
                }
            }
        }

        private async void btnHizliTarama_Click(object sender, EventArgs e)
        {
            await StartScan(false);
        }

        private async void btnTamTarama_Click(object sender, EventArgs e)
        {
            await StartScan(true);
        }

        private async System.Threading.Tasks.Task StartScan(bool fullScan)
        {
            if (string.IsNullOrWhiteSpace(txtKlasorYolu.Text))
            {
                MessageBox.Show("Lütfen bir klasör seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.IO.Directory.Exists(txtKlasorYolu.Text))
            {
                MessageBox.Show("Seçilen klasör bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // UI'ı kilitle
            SetScanningState(true);
            _currentResults.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            progressBar1.Value = 0;
            lblTarananDosya.Text = "0 / 0";

            _cancellationTokenSource = new CancellationTokenSource();
            var progress = new Progress<(int scanned, int total, ScanResult result)>(p =>
            {
                progressBar1.Maximum = p.total;
                progressBar1.Value = p.scanned;
                lblTarananDosya.Text = $"{p.scanned} / {p.total}";

                if (p.result != null)
                {
                    _currentResults.Add(p.result);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = _currentResults;
                }
            });

            try
            {
                var results = await _scannerService.ScanAsync(
                    txtKlasorYolu.Text,
                    fullScan,
                    progress,
                    _cancellationTokenSource.Token
                );

                // Sonuçları göster
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = results;

                // Rapor oluştur
                string reportPath = System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(),
                    $"Antivirus_Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
                );
                _reportService.SaveReport(results, reportPath);

                int maliciousCount = results.Count(r => r.Sonuc == ScanResultType.Malicious);
                int suspiciousCount = results.Count(r => r.Sonuc == ScanResultType.Suspicious);

                MessageBox.Show(
                    $"Tarama tamamlandı!\n\n" +
                    $"Toplam: {results.Count}\n" +
                    $"Temiz: {results.Count - maliciousCount - suspiciousCount}\n" +
                    $"Şüpheli: {suspiciousCount}\n" +
                    $"Zararlı: {maliciousCount}\n\n" +
                    $"Rapor: {reportPath}",
                    "Tarama Sonucu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Tarama iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Tarama sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetScanningState(false);
            }
        }

        private void SetScanningState(bool scanning)
        {
            btnHizliTarama.Enabled = !scanning;
            btnTamTarama.Enabled = !scanning;
            btnKlasorSec.Enabled = !scanning;
            btnIptal.Enabled = scanning;
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        private void QuarantineSelected()
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            var selectedRow = dataGridView1.SelectedRows[0];
            var result = selectedRow.DataBoundItem as ScanResult;

            if (result == null)
                return;

            if (MessageBox.Show(
                $"Bu dosyayı karantinaya almak istediğinizden emin misiniz?\n\n{result.DosyaYolu}",
                "Karantina",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool success = _quarantineService.QuarantineFile(result.DosyaYolu, result.Hash, result.Boyut);
                if (success)
                {
                    result.Aksiyon = ActionType.Quarantine;
                    dataGridView1.Refresh();
                    MessageBox.Show("Dosya karantinaya alındı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Dosya karantinaya alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteSelected()
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            var selectedRow = dataGridView1.SelectedRows[0];
            var result = selectedRow.DataBoundItem as ScanResult;

            if (result == null)
                return;

            if (MessageBox.Show(
                $"Bu dosyayı kalıcı olarak silmek istediğinizden emin misiniz?\n\n{result.DosyaYolu}",
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    System.IO.File.Delete(result.DosyaYolu);
                    result.Aksiyon = ActionType.Deleted;
                    dataGridView1.Refresh();
                    MessageBox.Show("Dosya silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Dosya silinemedi: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnKarantina_Click(object sender, EventArgs e)
        {
            var quarantineForm = new QuarantineForm();
            quarantineForm.ShowDialog();
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                // Ayarlar değişti, servisleri yeniden başlat
                InitializeServices();
            }
        }
    }
}
