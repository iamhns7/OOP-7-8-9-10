namespace Antivirus
{
    partial class SettingsForm
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
            this.lblMaliciousThreshold = new System.Windows.Forms.Label();
            this.numMaliciousThreshold = new System.Windows.Forms.NumericUpDown();
            this.lblSuspiciousThreshold = new System.Windows.Forms.Label();
            this.numSuspiciousThreshold = new System.Windows.Forms.NumericUpDown();
            this.chkAutoQuarantine = new System.Windows.Forms.CheckBox();
            this.chkShowNotifications = new System.Windows.Forms.CheckBox();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.btnIptal = new System.Windows.Forms.Button();
            this.btnVarsayilan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numMaliciousThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSuspiciousThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaliciousThreshold
            // 
            this.lblMaliciousThreshold.AutoSize = true;
            this.lblMaliciousThreshold.Location = new System.Drawing.Point(12, 20);
            this.lblMaliciousThreshold.Name = "lblMaliciousThreshold";
            this.lblMaliciousThreshold.Size = new System.Drawing.Size(120, 13);
            this.lblMaliciousThreshold.TabIndex = 0;
            this.lblMaliciousThreshold.Text = "Zararlı Eşik Değeri (0-100):";
            // 
            // numMaliciousThreshold
            // 
            this.numMaliciousThreshold.Location = new System.Drawing.Point(150, 18);
            this.numMaliciousThreshold.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaliciousThreshold.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaliciousThreshold.Name = "numMaliciousThreshold";
            this.numMaliciousThreshold.Size = new System.Drawing.Size(120, 20);
            this.numMaliciousThreshold.TabIndex = 1;
            this.numMaliciousThreshold.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // lblSuspiciousThreshold
            // 
            this.lblSuspiciousThreshold.AutoSize = true;
            this.lblSuspiciousThreshold.Location = new System.Drawing.Point(12, 50);
            this.lblSuspiciousThreshold.Name = "lblSuspiciousThreshold";
            this.lblSuspiciousThreshold.Size = new System.Drawing.Size(130, 13);
            this.lblSuspiciousThreshold.TabIndex = 2;
            this.lblSuspiciousThreshold.Text = "Şüpheli Eşik Değeri (0-100):";
            // 
            // numSuspiciousThreshold
            // 
            this.numSuspiciousThreshold.Location = new System.Drawing.Point(150, 48);
            this.numSuspiciousThreshold.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numSuspiciousThreshold.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numSuspiciousThreshold.Name = "numSuspiciousThreshold";
            this.numSuspiciousThreshold.Size = new System.Drawing.Size(120, 20);
            this.numSuspiciousThreshold.TabIndex = 3;
            this.numSuspiciousThreshold.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // chkAutoQuarantine
            // 
            this.chkAutoQuarantine.AutoSize = true;
            this.chkAutoQuarantine.Location = new System.Drawing.Point(12, 80);
            this.chkAutoQuarantine.Name = "chkAutoQuarantine";
            this.chkAutoQuarantine.Size = new System.Drawing.Size(200, 17);
            this.chkAutoQuarantine.TabIndex = 4;
            this.chkAutoQuarantine.Text = "Zararlı dosyaları otomatik karantinaya al";
            this.chkAutoQuarantine.UseVisualStyleBackColor = true;
            // 
            // chkShowNotifications
            // 
            this.chkShowNotifications.AutoSize = true;
            this.chkShowNotifications.Location = new System.Drawing.Point(12, 110);
            this.chkShowNotifications.Name = "chkShowNotifications";
            this.chkShowNotifications.Size = new System.Drawing.Size(150, 17);
            this.chkShowNotifications.TabIndex = 5;
            this.chkShowNotifications.Text = "Bildirimleri göster";
            this.chkShowNotifications.UseVisualStyleBackColor = true;
            // 
            // btnKaydet
            // 
            this.btnKaydet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKaydet.Location = new System.Drawing.Point(200, 150);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(100, 30);
            this.btnKaydet.TabIndex = 6;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // btnIptal
            // 
            this.btnIptal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIptal.Location = new System.Drawing.Point(306, 150);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(100, 30);
            this.btnIptal.TabIndex = 7;
            this.btnIptal.Text = "İptal";
            this.btnIptal.UseVisualStyleBackColor = true;
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnVarsayilan
            // 
            this.btnVarsayilan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVarsayilan.Location = new System.Drawing.Point(12, 150);
            this.btnVarsayilan.Name = "btnVarsayilan";
            this.btnVarsayilan.Size = new System.Drawing.Size(100, 30);
            this.btnVarsayilan.TabIndex = 8;
            this.btnVarsayilan.Text = "Varsayılan";
            this.btnVarsayilan.UseVisualStyleBackColor = true;
            this.btnVarsayilan.Click += new System.EventHandler(this.btnVarsayilan_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 192);
            this.Controls.Add(this.btnVarsayilan);
            this.Controls.Add(this.btnIptal);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.chkShowNotifications);
            this.Controls.Add(this.chkAutoQuarantine);
            this.Controls.Add(this.numSuspiciousThreshold);
            this.Controls.Add(this.lblSuspiciousThreshold);
            this.Controls.Add(this.numMaliciousThreshold);
            this.Controls.Add(this.lblMaliciousThreshold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ayarlar";
            ((System.ComponentModel.ISupportInitialize)(this.numMaliciousThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSuspiciousThreshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaliciousThreshold;
        private System.Windows.Forms.NumericUpDown numMaliciousThreshold;
        private System.Windows.Forms.Label lblSuspiciousThreshold;
        private System.Windows.Forms.NumericUpDown numSuspiciousThreshold;
        private System.Windows.Forms.CheckBox chkAutoQuarantine;
        private System.Windows.Forms.CheckBox chkShowNotifications;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Button btnIptal;
        private System.Windows.Forms.Button btnVarsayilan;
    }
}

