using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Investigation.Shared.Helpers
{
    public static class UrlHashHelperByRedirectId
    {
        private static IConfiguration _configuration;
        private static string Salt => _configuration["HashSettings:Salt"];
        private static int HashLength => int.Parse(_configuration["HashSettings:HashLength"]);

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string ComputeShortHash(object? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            string idStr = id.ToString();
            string input = $"{idStr}{Salt}{DateTime.UtcNow.Year}";

            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            string base64 = Convert.ToBase64String(hashBytes)
                .Replace("+", "-").Replace("/", "_").TrimEnd('=');

            string alphanum = Regex.Replace(base64, @"[^a-zA-Z0-9]", "");

            return alphanum.Length >= HashLength
                ? alphanum.Substring(0, HashLength).ToLower()
                : alphanum.PadLeft(HashLength, '0');
        }

        public static string? ValidateAndExtractId(string idWithHash)
        {
            if (string.IsNullOrWhiteSpace(idWithHash))
            {
                return null;
            }

            var parts = idWithHash.Split('-');
            if (parts.Length != 2)
            {
                return null;
            }

            string incomingId = parts[0].Trim();
            string incomingHash = parts[1].Trim();

            string expectedHash = ComputeShortHash(incomingId);
            if (incomingHash != expectedHash)
            {
                return null;
            }
            return incomingId;
        }
        public static string ComputeShortHashForString(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "id was null or empty");
            }

            string input = $"{id}{Salt}{DateTime.UtcNow.Year}";

            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            string base64 = Convert.ToBase64String(hashBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');

            string alphanum = Regex.Replace(base64, @"[^a-zA-Z0-9]", "");

            return alphanum.Length >= HashLength
                ? alphanum.Substring(0, HashLength).ToLower()
                : alphanum.PadLeft(HashLength, '0');
        }
        public static string? ValidateAndExtractStringId(string idWithHash)
        {
            if (string.IsNullOrWhiteSpace(idWithHash))
            {
                return null;
            }

            var parts = idWithHash.Split('-');
            if (parts.Length != 2)
            {
                return null;
            }

            string incomingId = parts[0].Trim();
            string incomingHash = parts[1].Trim();

            string expectedHash = ComputeShortHashForString(incomingId);
            if (incomingHash != expectedHash)
            {
                return null;
            }
            return incomingId;
        }
        public static string ComputeShortHashForString(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            return $"{id}-{ComputeShortHash(id)}";
        }
    }
}
