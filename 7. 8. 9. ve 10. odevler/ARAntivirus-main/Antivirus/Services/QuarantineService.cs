using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Antivirus.Models;

namespace Antivirus.Services
{
    public class QuarantineItem
    {
        public string OriginalPath { get; set; }
        public string QuarantinePath { get; set; }
        public string Hash { get; set; }
        public DateTime QuarantineDate { get; set; }
        public long Size { get; set; }
    }

    public class QuarantineService
    {
        private readonly string _quarantineFolder;
        private readonly string _manifestPath;

        public QuarantineService()
        {
            _quarantineFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Quarantine");
            _manifestPath = Path.Combine(_quarantineFolder, "quarantine_manifest.json");
            
            if (!Directory.Exists(_quarantineFolder))
            {
                Directory.CreateDirectory(_quarantineFolder);
            }
        }

        public bool QuarantineFile(string filePath, string hash, long size)
        {
            try
            {
                if (!File.Exists(filePath))
                    return false;

                // GUID ile benzersiz isim oluştur
                string fileName = Path.GetFileName(filePath);
                string guid = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(fileName);
                string quarantineFileName = $"{guid}{extension}";
                string quarantinePath = Path.Combine(_quarantineFolder, quarantineFileName);

                // Dosyayı taşı
                File.Move(filePath, quarantinePath);

                // Manifest'e ekle
                var items = LoadManifest();
                items.Add(new QuarantineItem
                {
                    OriginalPath = filePath,
                    QuarantinePath = quarantinePath,
                    Hash = hash,
                    QuarantineDate = DateTime.Now,
                    Size = size
                });
                SaveManifest(items);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RestoreFile(string quarantinePath)
        {
            try
            {
                var items = LoadManifest();
                var item = items.FirstOrDefault(i => i.QuarantinePath == quarantinePath);
                
                if (item == null || !File.Exists(quarantinePath))
                    return false;

                // Orijinal klasör yoksa oluştur
                string originalDir = Path.GetDirectoryName(item.OriginalPath);
                if (!string.IsNullOrEmpty(originalDir) && !Directory.Exists(originalDir))
                {
                    Directory.CreateDirectory(originalDir);
                }

                // Dosyayı geri taşı
                File.Move(quarantinePath, item.OriginalPath);

                // Manifest'ten kaldır
                items.Remove(item);
                SaveManifest(items);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFile(string quarantinePath)
        {
            try
            {
                var items = LoadManifest();
                var item = items.FirstOrDefault(i => i.QuarantinePath == quarantinePath);
                
                if (item == null)
                    return false;

                // Dosyayı sil
                if (File.Exists(quarantinePath))
                {
                    File.Delete(quarantinePath);
                }

                // Manifest'ten kaldır
                items.Remove(item);
                SaveManifest(items);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<QuarantineItem> GetQuarantinedFiles()
        {
            return LoadManifest();
        }

        private List<QuarantineItem> LoadManifest()
        {
            try
            {
                if (File.Exists(_manifestPath))
                {
                    string json = File.ReadAllText(_manifestPath);
                    var items = JsonSerializer.Deserialize<List<QuarantineItem>>(json);
                    return items ?? new List<QuarantineItem>();
                }
            }
            catch (Exception)
            {
                // Hata durumunda boş liste
            }

            return new List<QuarantineItem>();
        }

        private void SaveManifest(List<QuarantineItem> items)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(items, options);
                File.WriteAllText(_manifestPath, json);
            }
            catch (Exception)
            {
                // Hata durumunda sessizce geç
            }
        }
    }
}

