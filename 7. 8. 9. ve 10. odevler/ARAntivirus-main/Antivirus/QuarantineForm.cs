using System;
using System.Linq;
using System.Windows.Forms;
using Antivirus.Services;

namespace Antivirus
{
    public partial class QuarantineForm : Form
    {
        private QuarantineService _quarantineService;
        private DataGridView _dataGridView;

        public QuarantineForm()
        {
            InitializeComponent();
            _quarantineService = new QuarantineService();
            SetupDataGridView();
            LoadQuarantinedFiles();
        }

        private void SetupDataGridView()
        {
            _dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false
            };

            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OriginalPath",
                HeaderText = "Orijinal Yol",
                DataPropertyName = "OriginalPath",
                Width = 300,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Hash",
                HeaderText = "Hash",
                DataPropertyName = "Hash",
                Width = 200
            });

            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QuarantineDate",
                HeaderText = "Karantina Tarihi",
                DataPropertyName = "QuarantineDate",
                Width = 150
            });

            _dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Size",
                HeaderText = "Boyut",
                DataPropertyName = "Size",
                Width = 100
            });

            panelDataGrid.Controls.Add(_dataGridView);
        }

        private void LoadQuarantinedFiles()
        {
            var items = _quarantineService.GetQuarantinedFiles();
            _dataGridView.DataSource = items;
        }

        private void btnGeriYukle_Click(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen geri yüklenecek bir dosya seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = _dataGridView.SelectedRows[0];
            var item = selectedRow.DataBoundItem as QuarantineItem;

            if (item == null)
                return;

            if (MessageBox.Show(
                $"Bu dosyayı geri yüklemek istediğinizden emin misiniz?\n\nOrijinal Yol: {item.OriginalPath}",
                "Geri Yükleme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool success = _quarantineService.RestoreFile(item.QuarantinePath);
                if (success)
                {
                    MessageBox.Show("Dosya başarıyla geri yüklendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadQuarantinedFiles();
                }
                else
                {
                    MessageBox.Show("Dosya geri yüklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnKaliciSil_Click(object sender, EventArgs e)
        {
            if (_dataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek bir dosya seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = _dataGridView.SelectedRows[0];
            var item = selectedRow.DataBoundItem as QuarantineItem;

            if (item == null)
                return;

            if (MessageBox.Show(
                $"Bu dosyayı kalıcı olarak silmek istediğinizden emin misiniz?\n\nBu işlem geri alınamaz!\n\n{item.OriginalPath}",
                "Kalıcı Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool success = _quarantineService.DeleteFile(item.QuarantinePath);
                if (success)
                {
                    MessageBox.Show("Dosya kalıcı olarak silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadQuarantinedFiles();
                }
                else
                {
                    MessageBox.Show("Dosya silinemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadQuarantinedFiles();
        }
    }
}

