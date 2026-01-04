using System;

namespace Antivirus.Models
{
    public enum ScanResultType
    {
        Clean,
        Suspicious,
        Malicious
    }

    public enum ActionType
    {
        None,
        Quarantine,
        Deleted
    }

    public class ScanResult
    {
        public string DosyaYolu { get; set; }
        public long Boyut { get; set; }
        public string Hash { get; set; }
        public int RiskSkoru { get; set; }
        public ScanResultType Sonuc { get; set; }
        public ActionType Aksiyon { get; set; }

        public string BoyutFormatted => FormatBoyut(Boyut);
        public string SonucText => Sonuc.ToString();
        public string AksiyonText => Aksiyon.ToString();

        private string FormatBoyut(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}

