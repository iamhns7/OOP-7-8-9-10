using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ChatAR.Models;

namespace ChatAR.Services
{
    public class ChatHistoryService
    {
        private readonly string _historyFilePath;

        public ChatHistoryService(string customPath = null)
        {
            _historyFilePath = customPath ?? Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ChatAR",
                "chat_history.json"
            );

            // Klasörü oluştur
            var directory = Path.GetDirectoryName(_historyFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void SaveHistory(List<ChatMessage> messages)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var json = JsonSerializer.Serialize(messages, options);
                File.WriteAllText(_historyFilePath, json);
            }
            catch (Exception ex)
            {
                // Hata durumunda sessizce devam et (kullanıcıya göstermeyiz)
                System.Diagnostics.Debug.WriteLine($"Geçmiş kaydedilemedi: {ex.Message}");
            }
        }

        public List<ChatMessage> LoadHistory()
        {
            try
            {
                if (!File.Exists(_historyFilePath))
                {
                    return new List<ChatMessage>();
                }

                var json = File.ReadAllText(_historyFilePath);
                var messages = JsonSerializer.Deserialize<List<ChatMessage>>(json);

                return messages ?? new List<ChatMessage>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Geçmiş yüklenemedi: {ex.Message}");
                return new List<ChatMessage>();
            }
        }

        public void ClearHistory()
        {
            try
            {
                if (File.Exists(_historyFilePath))
                {
                    File.Delete(_historyFilePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Geçmiş silinemedi: {ex.Message}");
            }
        }
    }
}

