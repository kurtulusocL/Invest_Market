using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Investigation.Business.Constants.Services
{
    public class PseudonymizationService
    {
        private readonly string _salt;

        public PseudonymizationService(IConfiguration configuration)
        {
            _salt = configuration["HashSettings:Salt"]
                ?? throw new InvalidOperationException("HashSettings:Salt read error!");
        }

        // Email veya UserName'i hash'e çevirir → Identity bu hash ile çalışır
        public string Pseudonymize(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_salt));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value.ToLowerInvariant()));
            var hex = Convert.ToHexString(hash).ToLowerInvariant()[..16]; // 16 karakter yeterli
            return $"u_{hex}";
        }

        // Email için → Identity'nin FindByEmailAsync kullanabilmesi için @internal.local ekler
        public string PseudonymizeEmail(string email)
        {
            var pseudo = Pseudonymize(email);
            return $"{pseudo}@internal.local";
        }
    }
}
