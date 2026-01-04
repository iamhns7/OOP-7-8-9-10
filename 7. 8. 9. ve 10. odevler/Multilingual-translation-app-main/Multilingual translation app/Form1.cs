using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multilingual_translation_app
{
    public partial class Form1 : Form
    {
        private TranslationService _translationService;
        private Dictionary<string, string> _languages;

        public Form1()
        {
            InitializeComponent();
            _translationService = new TranslationService();
            InitializeLanguages();
            LoadLanguages();
        }

        private void InitializeLanguages()
        {
            // Çok sayıda dil desteği - ISO 639-1 kodları
            _languages = new Dictionary<string, string>
            {
                { "tr", "Türkçe" },
                { "en", "İngilizce" },
                { "ar", "Arapça" },
                { "de", "Almanca" },
                { "fr", "Fransızca" },
                { "es", "İspanyolca" },
                { "it", "İtalyanca" },
                { "pt", "Portekizce" },
                { "ru", "Rusça" },
                { "zh", "Çince" },
                { "ja", "Japonca" },
                { "ko", "Korece" },
                { "hi", "Hintçe" },
                { "nl", "Felemenkçe" },
                { "pl", "Lehçe" },
                { "sv", "İsveççe" },
                { "da", "Danca" },
                { "no", "Norveççe" },
                { "fi", "Fince" },
                { "el", "Yunanca" },
                { "he", "İbranice" },
                { "cs", "Çekçe" },
                { "ro", "Rumence" },
                { "hu", "Macarca" },
                { "bg", "Bulgarca" },
                { "hr", "Hırvatça" },
                { "sk", "Slovakça" },
                { "sl", "Slovence" },
                { "et", "Estonca" },
                { "lv", "Letonca" },
                { "lt", "Litvanca" },
                { "uk", "Ukraynaca" },
                { "vi", "Vietnamca" },
                { "th", "Tayca" },
                { "id", "Endonezce" },
                { "ms", "Malayca" },
                { "tl", "Filipince" },
                { "sw", "Svahili" },
                { "af", "Afrikaanca" },
                { "sq", "Arnavutça" },
                { "az", "Azerice" },
                { "eu", "Baskça" },
                { "be", "Belarusça" },
                { "bn", "Bengalce" },
                { "bs", "Boşnakça" },
                { "ca", "Katalanca" },
                { "cy", "Galce" },
                { "fa", "Farsça" },
                { "ga", "İrlandaca" },
                { "gl", "Galisyaca" },
                { "gu", "Güceratça" },
                { "is", "İzlandaca" },
                { "ka", "Gürcüce" },
                { "kk", "Kazakça" },
                { "km", "Kmerce" },
                { "kn", "Kannada" },
                { "ky", "Kırgızca" },
                { "lo", "Lao" },
                { "mk", "Makedonca" },
                { "ml", "Malayalam" },
                { "mn", "Moğolca" },
                { "mr", "Marathi" },
                { "my", "Myanmar" },
                { "ne", "Nepalce" },
                { "pa", "Pencapça" },
                { "si", "Sinhala" },
                { "sr", "Sırpça" },
                { "ta", "Tamil" },
                { "te", "Telugu" },
                { "ur", "Urduca" },
                { "uz", "Özbekçe" },
                { "auto", "Otomatik Algıla" }
            };
        }

        private void LoadLanguages()
        {
            cmbSourceLanguage.Items.Clear();
            cmbTargetLanguage.Items.Clear();

            foreach (var lang in _languages.OrderBy(x => x.Value))
            {
                cmbSourceLanguage.Items.Add($"{lang.Value} ({lang.Key})");
                cmbTargetLanguage.Items.Add($"{lang.Value} ({lang.Key})");
            }

            // Varsayılan olarak Türkçe kaynak, İngilizce hedef
            cmbSourceLanguage.SelectedIndex = cmbSourceLanguage.FindStringExact("Türkçe (tr)");
            cmbTargetLanguage.SelectedIndex = cmbTargetLanguage.FindStringExact("İngilizce (en)");
        }

        private string GetLanguageCode(string languageText)
        {
            if (string.IsNullOrEmpty(languageText))
                return "en";

            int startIndex = languageText.LastIndexOf('(') + 1;
            int endIndex = languageText.LastIndexOf(')');
            
            if (startIndex > 0 && endIndex > startIndex)
            {
                return languageText.Substring(startIndex, endIndex - startIndex);
            }
            
            return "en";
        }

        private async void btnTranslate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSourceText.Text))
            {
                MessageBox.Show("Lütfen çevrilecek metni girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbSourceLanguage.SelectedItem == null || cmbTargetLanguage.SelectedItem == null)
            {
                MessageBox.Show("Lütfen kaynak ve hedef dilleri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sourceLang = GetLanguageCode(cmbSourceLanguage.SelectedItem.ToString());
            string targetLang = GetLanguageCode(cmbTargetLanguage.SelectedItem.ToString());

            if (sourceLang == targetLang)
            {
                MessageBox.Show("Kaynak ve hedef dil aynı olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnTranslate.Enabled = false;
            btnTranslate.Text = "Çevriliyor...";
            txtTranslatedText.Text = "Çeviri yapılıyor, lütfen bekleyin...";

            try
            {
                string translatedText = await _translationService.TranslateAsync(
                    txtSourceText.Text,
                    sourceLang,
                    targetLang
                );

                txtTranslatedText.Text = translatedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Çeviri sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTranslatedText.Text = string.Empty;
            }
            finally
            {
                btnTranslate.Enabled = true;
                btnTranslate.Text = "→";
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTranslatedText.Text))
            {
                Clipboard.SetText(txtTranslatedText.Text);
                MessageBox.Show("Çeviri metni panoya kopyalandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kopyalanacak metin yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            // Dilleri değiştir
            int sourceIndex = cmbSourceLanguage.SelectedIndex;
            int targetIndex = cmbTargetLanguage.SelectedIndex;

            cmbSourceLanguage.SelectedIndex = targetIndex;
            cmbTargetLanguage.SelectedIndex = sourceIndex;

            // Metinleri değiştir
            string tempText = txtSourceText.Text;
            txtSourceText.Text = txtTranslatedText.Text;
            txtTranslatedText.Text = tempText;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _translationService?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
