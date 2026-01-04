namespace Multilingual_translation_app
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
            this.lblSourceLanguage = new System.Windows.Forms.Label();
            this.cmbSourceLanguage = new System.Windows.Forms.ComboBox();
            this.lblTargetLanguage = new System.Windows.Forms.Label();
            this.cmbTargetLanguage = new System.Windows.Forms.ComboBox();
            this.txtSourceText = new System.Windows.Forms.TextBox();
            this.txtTranslatedText = new System.Windows.Forms.TextBox();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnSwap = new System.Windows.Forms.Button();
            this.lblSource = new System.Windows.Forms.Label();
            this.lblTarget = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSourceLanguage
            // 
            this.lblSourceLanguage.AutoSize = true;
            this.lblSourceLanguage.Location = new System.Drawing.Point(12, 15);
            this.lblSourceLanguage.Name = "lblSourceLanguage";
            this.lblSourceLanguage.Size = new System.Drawing.Size(73, 13);
            this.lblSourceLanguage.TabIndex = 0;
            this.lblSourceLanguage.Text = "Kaynak Dil:";
            // 
            // cmbSourceLanguage
            // 
            this.cmbSourceLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceLanguage.FormattingEnabled = true;
            this.cmbSourceLanguage.Location = new System.Drawing.Point(91, 12);
            this.cmbSourceLanguage.Name = "cmbSourceLanguage";
            this.cmbSourceLanguage.Size = new System.Drawing.Size(200, 21);
            this.cmbSourceLanguage.TabIndex = 1;
            // 
            // lblTargetLanguage
            // 
            this.lblTargetLanguage.AutoSize = true;
            this.lblTargetLanguage.Location = new System.Drawing.Point(350, 15);
            this.lblTargetLanguage.Name = "lblTargetLanguage";
            this.lblTargetLanguage.Size = new System.Drawing.Size(65, 13);
            this.lblTargetLanguage.TabIndex = 2;
            this.lblTargetLanguage.Text = "Hedef Dil:";
            // 
            // cmbTargetLanguage
            // 
            this.cmbTargetLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTargetLanguage.FormattingEnabled = true;
            this.cmbTargetLanguage.Location = new System.Drawing.Point(421, 12);
            this.cmbTargetLanguage.Name = "cmbTargetLanguage";
            this.cmbTargetLanguage.Size = new System.Drawing.Size(200, 21);
            this.cmbTargetLanguage.TabIndex = 3;
            // 
            // txtSourceText
            // 
            this.txtSourceText.Location = new System.Drawing.Point(12, 60);
            this.txtSourceText.Multiline = true;
            this.txtSourceText.Name = "txtSourceText";
            this.txtSourceText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSourceText.Size = new System.Drawing.Size(300, 250);
            this.txtSourceText.TabIndex = 4;
            // 
            // txtTranslatedText
            // 
            this.txtTranslatedText.Location = new System.Drawing.Point(350, 60);
            this.txtTranslatedText.Multiline = true;
            this.txtTranslatedText.Name = "txtTranslatedText";
            this.txtTranslatedText.ReadOnly = true;
            this.txtTranslatedText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslatedText.Size = new System.Drawing.Size(300, 250);
            this.txtTranslatedText.TabIndex = 5;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(318, 150);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(26, 50);
            this.btnTranslate.TabIndex = 6;
            this.btnTranslate.Text = "→";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(350, 316);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(100, 30);
            this.btnCopy.TabIndex = 7;
            this.btnCopy.Text = "Kopyala";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnSwap
            // 
            this.btnSwap.Location = new System.Drawing.Point(318, 100);
            this.btnSwap.Name = "btnSwap";
            this.btnSwap.Size = new System.Drawing.Size(26, 30);
            this.btnSwap.TabIndex = 8;
            this.btnSwap.Text = "⇄";
            this.btnSwap.UseVisualStyleBackColor = true;
            this.btnSwap.Click += new System.EventHandler(this.btnSwap_Click);
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(12, 44);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(76, 13);
            this.lblSource.TabIndex = 9;
            this.lblSource.Text = "Kaynak Metin:";
            // 
            // lblTarget
            // 
            this.lblTarget.AutoSize = true;
            this.lblTarget.Location = new System.Drawing.Point(350, 44);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(68, 13);
            this.lblTarget.TabIndex = 10;
            this.lblTarget.Text = "Çeviri Metni:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 358);
            this.Controls.Add(this.lblTarget);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.btnSwap);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.txtTranslatedText);
            this.Controls.Add(this.txtSourceText);
            this.Controls.Add(this.cmbTargetLanguage);
            this.Controls.Add(this.lblTargetLanguage);
            this.Controls.Add(this.cmbSourceLanguage);
            this.Controls.Add(this.lblSourceLanguage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Çoklu Dil Çeviri Uygulaması";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSourceLanguage;
        private System.Windows.Forms.ComboBox cmbSourceLanguage;
        private System.Windows.Forms.Label lblTargetLanguage;
        private System.Windows.Forms.ComboBox cmbTargetLanguage;
        private System.Windows.Forms.TextBox txtSourceText;
        private System.Windows.Forms.TextBox txtTranslatedText;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnSwap;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.Label lblTarget;
    }
}

