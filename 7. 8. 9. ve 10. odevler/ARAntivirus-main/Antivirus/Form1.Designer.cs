namespace Antivirus
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtKlasorYolu = new System.Windows.Forms.TextBox();
            this.btnKlasorSec = new System.Windows.Forms.Button();
            this.btnHizliTarama = new System.Windows.Forms.Button();
            this.btnTamTarama = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblTarananDosya = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnKarantina = new System.Windows.Forms.Button();
            this.btnAyarlar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtKlasorYolu
            // 
            this.txtKlasorYolu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKlasorYolu.Location = new System.Drawing.Point(12, 12);
            this.txtKlasorYolu.Name = "txtKlasorYolu";
            this.txtKlasorYolu.ReadOnly = true;
            this.txtKlasorYolu.Size = new System.Drawing.Size(600, 20);
            this.txtKlasorYolu.TabIndex = 0;
            // 
            // btnKlasorSec
            // 
            this.btnKlasorSec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKlasorSec.Location = new System.Drawing.Point(618, 10);
            this.btnKlasorSec.Name = "btnKlasorSec";
            this.btnKlasorSec.Size = new System.Drawing.Size(100, 23);
            this.btnKlasorSec.TabIndex = 1;
            this.btnKlasorSec.Text = "Klasör Seç";
            this.btnKlasorSec.UseVisualStyleBackColor = true;
            this.btnKlasorSec.Click += new System.EventHandler(this.btnKlasorSec_Click);
            // 
            // btnHizliTarama
            // 
            this.btnHizliTarama.Location = new System.Drawing.Point(12, 38);
            this.btnHizliTarama.Name = "btnHizliTarama";
            this.btnHizliTarama.Size = new System.Drawing.Size(120, 30);
            this.btnHizliTarama.TabIndex = 2;
            this.btnHizliTarama.Text = "Hızlı Tarama";
            this.btnHizliTarama.UseVisualStyleBackColor = true;
            this.btnHizliTarama.Click += new System.EventHandler(this.btnHizliTarama_Click);
            // 
            // btnTamTarama
            // 
            this.btnTamTarama.Location = new System.Drawing.Point(138, 38);
            this.btnTamTarama.Name = "btnTamTarama";
            this.btnTamTarama.Size = new System.Drawing.Size(120, 30);
            this.btnTamTarama.TabIndex = 3;
            this.btnTamTarama.Text = "Tam Tarama";
            this.btnTamTarama.UseVisualStyleBackColor = true;
            this.btnTamTarama.Click += new System.EventHandler(this.btnTamTarama_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 74);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(600, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // lblTarananDosya
            // 
            this.lblTarananDosya.AutoSize = true;
            this.lblTarananDosya.Location = new System.Drawing.Point(618, 79);
            this.lblTarananDosya.Name = "lblTarananDosya";
            this.lblTarananDosya.Size = new System.Drawing.Size(43, 13);
            this.lblTarananDosya.TabIndex = 5;
            this.lblTarananDosya.Text = "0 / 0";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 103);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(706, 350);
            this.dataGridView1.TabIndex = 6;
            // 
            // btnIptal
            // 
            this.btnIptal.Enabled = false;
            this.btnIptal.Location = new System.Drawing.Point(264, 38);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(100, 30);
            this.btnIptal.TabIndex = 7;
            this.btnIptal.Text = "İptal";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnKarantina
            // 
            this.btnKarantina.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnKarantina.Location = new System.Drawing.Point(12, 459);
            this.btnKarantina.Name = "btnKarantina";
            this.btnKarantina.Size = new System.Drawing.Size(120, 30);
            this.btnKarantina.TabIndex = 8;
            this.btnKarantina.Text = "Karantina";
            this.btnKarantina.UseVisualStyleBackColor = true;
            this.btnKarantina.Click += new System.EventHandler(this.btnKarantina_Click);
            // 
            // btnAyarlar
            // 
            this.btnAyarlar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAyarlar.Location = new System.Drawing.Point(598, 459);
            this.btnAyarlar.Name = "btnAyarlar";
            this.btnAyarlar.Size = new System.Drawing.Size(120, 30);
            this.btnAyarlar.TabIndex = 9;
            this.btnAyarlar.Text = "Ayarlar";
            this.btnAyarlar.UseVisualStyleBackColor = true;
            this.btnAyarlar.Click += new System.EventHandler(this.btnAyarlar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 501);
            this.Controls.Add(this.btnAyarlar);
            this.Controls.Add(this.btnKarantina);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblTarananDosya);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnTamTarama);
            this.Controls.Add(this.btnHizliTarama);
            this.Controls.Add(this.btnKlasorSec);
            this.Controls.Add(this.txtKlasorYolu);
            this.MinimumSize = new System.Drawing.Size(746, 540);
            this.Name = "Form1";
            this.Text = "Antivirus Tarayıcı";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKlasorYolu;
        private System.Windows.Forms.Button btnKlasorSec;
        private System.Windows.Forms.Button btnHizliTarama;
        private System.Windows.Forms.Button btnTamTarama;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblTarananDosya;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnKarantina;
        private System.Windows.Forms.Button btnAyarlar;
    }
}
