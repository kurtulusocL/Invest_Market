using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Dtos.ResultDtos;
using Microsoft.AspNetCore.Identity;

namespace Investigation.Business.Constants.Services
{
    public class DataMigrationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EncryptionService _encryptionService;
        private readonly PseudonymizationService _pseudonymizationService;

        public DataMigrationService(UserManager<AppUser> userManager, EncryptionService encryptionService, PseudonymizationService pseudonymizationService)
        {
            _userManager = userManager;
            _encryptionService = encryptionService;
            _pseudonymizationService = pseudonymizationService;
        }

        public async Task<DataMigrationResultDto> MigrateExistingUsersAsync()
        {
            var result = new DataMigrationResultDto();
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                if (user.IsAdmin == true)
                {
                    result.SkippedCount++;
                    continue;
                }
                try
                {
                    bool needsUpdate = false;

                    // 1. Email kontrolü — @internal.local ile bitmiyorsa migrate et
                    if (!user.Email.EndsWith("@internal.local", StringComparison.OrdinalIgnoreCase))
                    {
                        var realEmail = user.Email;
                        var pseudoEmail = _pseudonymizationService.PseudonymizeEmail(realEmail);

                        user.EncryptedEmail = _encryptionService.Encrypt(realEmail);
                        user.Email = pseudoEmail;
                        user.NormalizedEmail = pseudoEmail.ToUpperInvariant();

                        needsUpdate = true;
                    }

                    // 2. UserName kontrolü — "u_" ile başlamıyorsa migrate et
                    if (!user.UserName.StartsWith("u_", StringComparison.OrdinalIgnoreCase))
                    {
                        var realUserName = user.UserName;
                        var pseudoUserName = _pseudonymizationService.Pseudonymize(realUserName);

                        user.EncryptedUserName = _encryptionService.Encrypt(realUserName);
                        user.UserName = pseudoUserName;
                        user.NormalizedUserName = pseudoUserName.ToUpperInvariant();

                        needsUpdate = true;
                    }

                    // 3. NameSurname kontrolü — decrypt "[Non-encripted key]" dönüyorsa düz metin
                    if (!string.IsNullOrEmpty(user.NameSurname))
                    {
                        var decrypted = _encryptionService.Decrypt(user.NameSurname);
                        if (decrypted == "[Non-encripted key]")
                        {
                            user.NameSurname = _encryptionService.Encrypt(user.NameSurname);
                            needsUpdate = true;
                        }
                    }

                    // 4. PhoneNumber kontrolü
                    if (!string.IsNullOrEmpty(user.PhoneNumber))
                    {
                        var decrypted = _encryptionService.Decrypt(user.PhoneNumber);
                        if (decrypted == "[Non-encripted key]")
                        {
                            user.PhoneNumber = _encryptionService.Encrypt(user.PhoneNumber);
                            needsUpdate = true;
                        }
                    }

                    // 5. Country kontrolü
                    if (!string.IsNullOrEmpty(user.Country))
                    {
                        var decrypted = _encryptionService.Decrypt(user.Country);
                        if (decrypted == "[Non-encripted key]")
                        {
                            user.Country = _encryptionService.Encrypt(user.Country);
                            needsUpdate = true;
                        }
                    }

                    // 6. EncryptedBirthdate kontrolü
                    if (string.IsNullOrEmpty(user.EncryptedBirthdate))
                    {
                        user.EncryptedBirthdate = _encryptionService.Encrypt(
                            user.Birthdate.ToString("yyyy-MM-dd"));
                        needsUpdate = true;
                    }

                    // Değişiklik varsa kaydet
                    if (needsUpdate)
                    {
                        await _userManager.UpdateAsync(user);
                        await _userManager.UpdateSecurityStampAsync(user);
                        result.MigratedCount++;
                    }
                    else
                    {
                        result.SkippedCount++;
                    }
                }
                catch (Exception ex)
                {
                    result.FailedCount++;
                    result.Errors.Add($"UserId: {user.Id} — {ex.Message}");
                }
            }

            result.TotalCount = users.Count;
            return result;
        }
    }
}
