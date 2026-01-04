using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Antivirus.Models;

namespace Antivirus.Services
{
    public class ScannerService
    {
        private readonly HashService _hashService;
        private readonly HeuristicService _heuristicService;
        private readonly SignatureService _signatureService;
        private readonly int _maliciousThreshold;
        private readonly int _suspiciousThreshold;

        public ScannerService(int maliciousThreshold = 70, int suspiciousThreshold = 40)
        {
            _hashService = new HashService();
            _heuristicService = new HeuristicService();
            _signatureService = new SignatureService();
            _maliciousThreshold = maliciousThreshold;
            _suspiciousThreshold = suspiciousThreshold;
        }

        public async Task<List<ScanResult>> ScanAsync(string folderPath, bool fullScan, IProgress<(int scanned, int total, ScanResult result)> progress, CancellationToken cancellationToken)
        {
            var results = new List<ScanResult>();
            var files = GetFiles(folderPath, fullScan);
            int total = files.Count;
            int scanned = 0;

            foreach (var filePath in files)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var fileInfo = new FileInfo(filePath);
                    if (!fileInfo.Exists)
                        continue;

                    // Hash hesapla
                    string hash = await _hashService.CalculateSHA256Async(filePath, cancellationToken);
                    
                    if (hash == "ACCESS_DENIED" || hash == "ERROR")
                    {
                        scanned++;
                        continue;
                    }

                    // İmza kontrolü
                    bool isKnownBad = _signatureService.IsKnownBadHash(hash);

                    // Heuristik puan
                    int riskScore = _heuristicService.CalculateRiskScore(filePath, hash, isKnownBad, fileInfo.Length);

                    // Sonuç belirleme
                    ScanResultType resultType = _heuristicService.DetermineResult(riskScore, _maliciousThreshold, _suspiciousThreshold);

                    var scanResult = new ScanResult
                    {
                        DosyaYolu = filePath,
                        Boyut = fileInfo.Length,
                        Hash = hash,
                        RiskSkoru = riskScore,
                        Sonuc = resultType,
                        Aksiyon = ActionType.None
                    };

                    results.Add(scanResult);
                    scanned++;
                    progress?.Report((scanned, total, scanResult));
                }
                catch (UnauthorizedAccessException)
                {
                    // Erişim hatası - sessizce geç
                    scanned++;
                }
                catch (Exception)
                {
                    // Diğer hatalar - sessizce geç
                    scanned++;
                }
            }

            return results;
        }

        private List<string> GetFiles(string folderPath, bool fullScan)
        {
            var files = new List<string>();
            
            try
            {
                if (fullScan)
                {
                    // Tam tarama: Tüm alt klasörler
                    files.AddRange(Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories));
                }
                else
                {
                    // Hızlı tarama: Sadece seçili klasör
                    files.AddRange(Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly));
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Erişim hatası
            }
            catch (Exception)
            {
                // Diğer hatalar
            }

            return files;
        }
    }
}

