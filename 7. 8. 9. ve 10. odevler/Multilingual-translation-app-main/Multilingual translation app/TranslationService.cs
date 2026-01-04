using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Multilingual_translation_app
{
    public class TranslationService
    {
        private readonly HttpClient _httpClient;
        private const string MyMemoryApiUrl = "https://api.mymemory.translated.net/get";

        public TranslationService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<string> TranslateAsync(string text, string sourceLanguage, string targetLanguage)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            try
            {
                // MyMemory Translation API - Ücretsiz, API key gerektirmiyor
                string url = $"{MyMemoryApiUrl}?q={Uri.EscapeDataString(text)}&langpair={sourceLanguage}|{targetLanguage}";
                
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(jsonResponse);

                if (json["responseStatus"]?.ToString() == "200")
                {
                    string translatedText = json["responseData"]?["translatedText"]?.ToString() ?? text;
                    return translatedText;
                }
                else
                {
                    return "Çeviri hatası: " + (json["responseStatus"]?.ToString() ?? "Bilinmeyen hata");
                }
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

