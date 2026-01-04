using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace Antivirus.Services
{
    public class HashService
    {
        public async Task<string> CalculateSHA256Async(string filePath, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var sha256 = SHA256.Create())
                    using (var stream = File.OpenRead(filePath))
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        var hash = sha256.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    return "ACCESS_DENIED";
                }
                catch (Exception)
                {
                    return "ERROR";
                }
            }, cancellationToken);
        }
    }
}

