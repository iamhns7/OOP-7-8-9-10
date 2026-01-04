using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Antivirus.Models;

namespace Antivirus.Services
{
    public class ReportService
    {
        public void SaveReport(List<ScanResult> results, string outputPath)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("=== ANTİVİRÜS TARAMA RAPORU ===");
                sb.AppendLine($"Tarih: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($"Toplam Dosya: {results.Count}");
                sb.AppendLine($"Temiz: {results.Count(r => r.Sonuc == ScanResultType.Clean)}");
                sb.AppendLine($"Şüpheli: {results.Count(r => r.Sonuc == ScanResultType.Suspicious)}");
                sb.AppendLine($"Zararlı: {results.Count(r => r.Sonuc == ScanResultType.Malicious)}");
                sb.AppendLine();
                sb.AppendLine("=== DETAYLAR ===");
                sb.AppendLine();

                foreach (var result in results.OrderByDescending(r => r.RiskSkoru))
                {
                    sb.AppendLine($"Dosya: {result.DosyaYolu}");
                    sb.AppendLine($"Boyut: {result.BoyutFormatted}");
                    sb.AppendLine($"Hash: {result.Hash}");
                    sb.AppendLine($"Risk Skoru: {result.RiskSkoru}");
                    sb.AppendLine($"Sonuç: {result.SonucText}");
                    sb.AppendLine($"Aksiyon: {result.AksiyonText}");
                    sb.AppendLine("---");
                }

                File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception)
            {
                // Hata durumunda sessizce geç
            }
        }
    }
}

