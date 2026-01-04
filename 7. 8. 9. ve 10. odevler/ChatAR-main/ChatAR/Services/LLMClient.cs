using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ChatAR.Models;

namespace ChatAR.Services
{
    public class LLMClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private const string DEFAULT_MODEL = "llama-3.1-8b-instant"; // Groq'un ücretsiz hızlı modeli

        public LLMClient(string apiKey, string customUrl = null)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _apiUrl = customUrl ?? "https://api.groq.com/openai/v1/chat/completions";
            
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30) // 30 saniye timeout
            };
            
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> SendMessageAsync(List<ChatMessage> conversationHistory, string systemPrompt = "Sen yardımsever bir asistansın. Türkçe cevap ver.")
        {
            try
            {
                // Mesajları API formatına çevir
                var messages = new List<object>();
                
                // System mesajını ekle
                if (!string.IsNullOrWhiteSpace(systemPrompt))
                {
                    messages.Add(new { role = "system", content = systemPrompt });
                }

                // Conversation history'yi ekle
                foreach (var msg in conversationHistory)
                {
                    messages.Add(new { role = msg.Role, content = msg.Content });
                }

                // Request body
                var requestBody = new
                {
                    model = DEFAULT_MODEL,
                    messages = messages,
                    temperature = 0.7,
                    max_tokens = 1024
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // API'ye istek at
                var response = await _httpClient.PostAsync(_apiUrl, content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"API Hatası: {response.StatusCode} - {errorContent}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseJson = JsonDocument.Parse(responseContent);

                // Cevabı parse et
                var choices = responseJson.RootElement.GetProperty("choices");
                if (choices.GetArrayLength() == 0)
                {
                    throw new Exception("API'den cevap alınamadı.");
                }

                var message = choices[0].GetProperty("message").GetProperty("content").GetString();
                return message ?? "Cevap alınamadı.";
            }
            catch (TaskCanceledException)
            {
                throw new TimeoutException("İstek zaman aşımına uğradı. İnternet bağlantınızı kontrol edin.");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Ağ hatası: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Beklenmeyen hata: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

