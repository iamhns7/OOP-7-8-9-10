using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatAR.Models;
using ChatAR.Services;

namespace ChatAR
{
    public partial class Form1 : Form
    {
        private RichTextBox chatDisplay;
        private TextBox messageInput;
        private Button sendButton;
        private Button clearButton;
        private TextBox apiKeyInput;
        private Label apiKeyLabel;

        private List<ChatMessage> conversationHistory;
        private LLMClient llmClient;
        private ChatHistoryService historyService;

        public Form1()
        {
            InitializeComponent();
            InitializeChatbot();
        }

        private void InitializeChatbot()
        {
            // Servisleri başlat
            historyService = new ChatHistoryService();
            conversationHistory = historyService.LoadHistory();

            // UI bileşenlerini oluştur
            SetupUI();

            // Geçmiş mesajları göster
            DisplayHistory();

            // API key kontrolü
            CheckApiKey();
        }

        private void SetupUI()
        {
            this.Text = "Chatbot - LLM Sohbet Uygulaması";
            this.Size = new Size(800, 600);
            this.MinimumSize = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            // API Key alanı
            apiKeyLabel = new Label
            {
                Text = "Groq API Key:",
                Location = new Point(10, 10),
                Size = new Size(100, 20),
                AutoSize = true
            };

            apiKeyInput = new TextBox
            {
                Location = new Point(120, 8),
                Size = new Size(400, 20),
                PasswordChar = '*',
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            apiKeyInput.TextChanged += ApiKeyInput_TextChanged;

            // Sohbet ekranı
            chatDisplay = new RichTextBox
            {
                Location = new Point(10, 40),
                Size = new Size(760, 450),
                ReadOnly = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };

            // Mesaj girişi
            messageInput = new TextBox
            {
                Location = new Point(10, 500),
                Size = new Size(650, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F)
            };
            messageInput.KeyDown += MessageInput_KeyDown;

            // Gönder butonu
            sendButton = new Button
            {
                Text = "Gönder",
                Location = new Point(670, 500),
                Size = new Size(100, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Enabled = false
            };
            sendButton.Click += SendButton_Click;

            // Temizle butonu
            clearButton = new Button
            {
                Text = "Geçmişi Temizle",
                Location = new Point(530, 8),
                Size = new Size(120, 25),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            clearButton.Click += ClearButton_Click;

            // Kontrolleri forma ekle
            this.Controls.Add(apiKeyLabel);
            this.Controls.Add(apiKeyInput);
            this.Controls.Add(chatDisplay);
            this.Controls.Add(messageInput);
            this.Controls.Add(sendButton);
            this.Controls.Add(clearButton);
        }

        private void CheckApiKey()
        {
            // API key'i dosyadan yükle
            var apiKeyPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ChatAR",
                "apikey.txt"
            );

            if (System.IO.File.Exists(apiKeyPath))
            {
                try
                {
                    var savedKey = System.IO.File.ReadAllText(apiKeyPath).Trim();
                    if (!string.IsNullOrEmpty(savedKey))
                    {
                        apiKeyInput.Text = savedKey;
                    }
                }
                catch
                {
                    // Dosya okunamazsa sessizce devam et
                }
            }
        }

        private void ApiKeyInput_TextChanged(object sender, EventArgs e)
        {
            var apiKey = apiKeyInput.Text.Trim();
            if (!string.IsNullOrEmpty(apiKey))
            {
                try
                {
                    llmClient = new LLMClient(apiKey);
                    sendButton.Enabled = true;

                    // API key'i dosyaya kaydet
                    var apiKeyPath = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "ChatAR",
                        "apikey.txt"
                    );
                    var directory = System.IO.Path.GetDirectoryName(apiKeyPath);
                    if (!string.IsNullOrEmpty(directory) && !System.IO.Directory.Exists(directory))
                    {
                        System.IO.Directory.CreateDirectory(directory);
                    }
                    System.IO.File.WriteAllText(apiKeyPath, apiKey);
                }
                catch
                {
                    sendButton.Enabled = false;
                }
            }
            else
            {
                sendButton.Enabled = false;
            }
        }

        private void MessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                if (sendButton.Enabled)
                {
                    SendButton_Click(sender, e);
                }
            }
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            var message = messageInput.Text.Trim();
            if (string.IsNullOrEmpty(message) || llmClient == null)
            {
                return;
            }

            // UI'ı güncelle
            messageInput.Enabled = false;
            sendButton.Enabled = false;

            // Kullanıcı mesajını ekle
            var userMessage = new ChatMessage
            {
                Role = "user",
                Content = message
            };
            conversationHistory.Add(userMessage);
            AppendToChat("Sen", message, Color.Blue);

            // Mesaj kutusunu temizle
            messageInput.Clear();

            // Bot cevabını bekle
            try
            {
                var botResponse = await llmClient.SendMessageAsync(conversationHistory);
                
                var botMessage = new ChatMessage
                {
                    Role = "assistant",
                    Content = botResponse
                };
                conversationHistory.Add(botMessage);
                AppendToChat("Bot", botResponse, Color.Green);

                // Geçmişi kaydet
                historyService.SaveHistory(conversationHistory);
            }
            catch (TimeoutException ex)
            {
                AppendToChat("Sistem", $"⏱️ Zaman aşımı: {ex.Message}", Color.Red);
            }
            catch (Exception ex)
            {
                AppendToChat("Sistem", $"❌ Hata: {ex.Message}", Color.Red);
            }
            finally
            {
                messageInput.Enabled = true;
                sendButton.Enabled = true;
                messageInput.Focus();
            }
        }

        private void AppendToChat(string sender, string message, Color color)
        {
            if (chatDisplay.InvokeRequired)
            {
                chatDisplay.Invoke(new Action(() => AppendToChat(sender, message, color)));
                return;
            }

            chatDisplay.SelectionStart = chatDisplay.TextLength;
            chatDisplay.SelectionLength = 0;
            chatDisplay.SelectionColor = color;
            chatDisplay.AppendText($"{sender}: {message}\n\n");
            chatDisplay.SelectionColor = chatDisplay.ForeColor;
            chatDisplay.ScrollToCaret();
        }

        private void DisplayHistory()
        {
            foreach (var msg in conversationHistory)
            {
                var color = msg.Role == "user" ? Color.Blue : Color.Green;
                var sender = msg.Role == "user" ? "Sen" : "Bot";
                AppendToChat(sender, msg.Content, color);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Sohbet geçmişini temizlemek istediğinize emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                conversationHistory.Clear();
                chatDisplay.Clear();
                historyService.ClearHistory();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Geçmişi kaydet
            if (conversationHistory != null && conversationHistory.Count > 0)
            {
                historyService.SaveHistory(conversationHistory);
            }

            if (llmClient != null)
            {
                llmClient.Dispose();
            }
            base.OnFormClosing(e);
        }
    }
}
