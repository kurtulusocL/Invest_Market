using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.WebUtilities;

namespace Investigation.Shared.Helpers
{
    public class SecureIdHelper
    {
        private readonly IDataProtector _protector;

        public SecureIdHelper(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("Investigation.SecureId");
        }

        public string Encrypt(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            var protectedText = _protector.Protect(id);
            var protectedBytes = System.Text.Encoding.UTF8.GetBytes(protectedText);
            return WebEncoders.Base64UrlEncode(protectedBytes);
        }
       
        public string Encrypt(int id)
        {
            return Encrypt(id.ToString());
        }

        public string Decrypt(string encryptedId)
        {
            if (string.IsNullOrEmpty(encryptedId)) return null;

            try
            {
                var protectedBytes = WebEncoders.Base64UrlDecode(encryptedId);
                var protectedText = System.Text.Encoding.UTF8.GetString(protectedBytes);
                return _protector.Unprotect(protectedText);
            }
            catch
            {
                return null;
            }
        }
        
        public int? DecryptToInt(string encryptedId)
        {
            var decrypted = Decrypt(encryptedId);
            if (decrypted == null) return null;

            if (int.TryParse(decrypted, out int result))
                return result;

            return null;
        }
    }
}
