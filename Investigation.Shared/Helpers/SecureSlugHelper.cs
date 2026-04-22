using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Investigation.Shared.Helpers
{
    public static class SecureSlugHelper
    {
        public static string Generate(object id, DateTime createdAt, int length = 8)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            string idStr = id.ToString();
            string input = $"{idStr}{createdAt:yyyyMMddHHmmssfff}";

            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            string base64 = Convert.ToBase64String(hashBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');

            string alphanum = Regex.Replace(base64, @"[^a-zA-Z0-9]", "");

            if (alphanum.Length >= length)
            {
                return alphanum.Substring(0, length);
            }
            return alphanum.PadLeft(length, '0');
        }
        //public static string TestExample(int id, DateTime dt) => Generate(id, dt);
    }
}
