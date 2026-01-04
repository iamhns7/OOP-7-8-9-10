using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Antivirus.Services
{
    public class SignatureService
    {
        private HashSet<string> _knownBadHashes;
        private readonly string _signaturesPath;

        public SignatureService()
        {
            string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            _signaturesPath = Path.Combine(dataFolder, "signatures.json");
            _knownBadHashes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            LoadSignatures();
        }

        private void LoadSignatures()
        {
            try
            {
                if (File.Exists(_signaturesPath))
                {
                    string json = File.ReadAllText(_signaturesPath);
                    var data = JsonSerializer.Deserialize<SignatureData>(json);
                    if (data?.KnownBadHashes != null)
                    {
                        _knownBadHashes = new HashSet<string>(data.KnownBadHashes, StringComparer.OrdinalIgnoreCase);
                    }
                }
            }
            catch (Exception)
            {
                // Hata durumunda boş liste kullan
                _knownBadHashes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        public bool IsKnownBadHash(string hash)
        {
            return _knownBadHashes.Contains(hash);
        }

        private class SignatureData
        {
            public List<string> KnownBadHashes { get; set; }
        }
    }
}

