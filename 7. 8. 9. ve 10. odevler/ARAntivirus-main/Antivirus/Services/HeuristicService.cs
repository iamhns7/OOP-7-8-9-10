using System;
using System.IO;
using System.Linq;
using Antivirus.Models;

namespace Antivirus.Services
{
    public class HeuristicService
    {
        private readonly string[] _suspiciousKeywords = { "setup_crack", "keygen", "crack", "patch", "serial", "hack", "trojan", "virus" };
        private readonly string[] _suspiciousExtensions = { ".bat", ".cmd", ".vbs", ".ps1", ".scr", ".com" };
        private readonly string[] _systemFolders = { "windows", "program files", "program files (x86)", "system32", "syswow64" };

        public int CalculateRiskScore(string filePath, string hash, bool isKnownBadHash, long fileSize)
        {
            int riskScore = 0;

            // İmza eşleşmesi → direkt 100 (Malicious)
            if (isKnownBadHash)
            {
                return 100;
            }

            string fileName = Path.GetFileName(filePath).ToLowerInvariant();
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            string directory = Path.GetDirectoryName(filePath)?.ToLowerInvariant() ?? "";

            // .exe uzantısı → +20
            if (extension == ".exe")
            {
                riskScore += 20;
            }

            // Çift uzantı kontrolü (örn: file.exe.txt)
            string nameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            if (Path.HasExtension(nameWithoutExt))
            {
                riskScore += 40;
            }

            // .bat/.cmd/.vbs/.ps1 → +30
            if (_suspiciousExtensions.Contains(extension))
            {
                riskScore += 30;
            }

            // Boyut < 30KB ve .exe ise → +15
            if (fileSize < 30 * 1024 && extension == ".exe")
            {
                riskScore += 15;
            }

            // Şüpheli anahtar kelimeler → +25
            foreach (var keyword in _suspiciousKeywords)
            {
                if (fileName.Contains(keyword))
                {
                    riskScore += 25;
                    break; // İlk eşleşmede çık
                }
            }

            return Math.Min(riskScore, 100); // Maksimum 100
        }

        public ScanResultType DetermineResult(int riskScore, int maliciousThreshold = 70, int suspiciousThreshold = 40)
        {
            if (riskScore >= maliciousThreshold)
            {
                return ScanResultType.Malicious;
            }
            else if (riskScore >= suspiciousThreshold)
            {
                return ScanResultType.Suspicious;
            }
            else
            {
                return ScanResultType.Clean;
            }
        }
    }
}

