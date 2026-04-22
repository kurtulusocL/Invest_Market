using System.Security.Claims;
using System.Text;
using Investigation.Business.Constants.Services;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Dtos.AuthDtos.UserAuthDtos;
using Investigation.Shared.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace Investigation.Business.Services.Concrete
{
    public class UserAuthManager : IUserAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ApplicationDbContext _context;
        readonly IMailService _mailService;
        readonly IWebHelperService _webHelperService;
        private readonly EncryptionService _encryptionService;
        private readonly PseudonymizationService _pseudonymizationService;
        public UserAuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context, IMailService mailService, IWebHelperService webHelperService, EncryptionService encryptionService, PseudonymizationService pseudonymizationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mailService = mailService;
            _webHelperService = webHelperService;
            _encryptionService = encryptionService;
            _pseudonymizationService = pseudonymizationService;
        }

        public async Task<bool> LoginAsync(UserLoginDto login)
        {
            try
            {
                var pseudoEmail = _pseudonymizationService.PseudonymizeEmail(login.Email);
                var user = await _userManager.FindByEmailAsync(pseudoEmail);
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User was null");

                if (user.IsActive == true && user.IsDeleted == false)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);
                    if (result.Succeeded)
                    {
                        string targetRole = user.IsCompany ? "CompanyUsers" : "InvestorUsers";

                        var currentRoles = await _userManager.GetRolesAsync(user);
                        if (!currentRoles.Contains(targetRole))
                        {
                            await _userManager.RemoveFromRolesAsync(user, currentRoles);
                            await _userManager.AddToRoleAsync(user, targetRole);
                        }

                        var currentIp = _webHelperService.GetClientIp();
                        var currentUa = _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();

                        var realEmail = _encryptionService.Decrypt(user.EncryptedEmail);
                        var realUserName = _encryptionService.Decrypt(user.EncryptedUserName);

                        _httpContextAccessor.HttpContext.Session.SetString("userId", user.Id.ToString());
                        _httpContextAccessor.HttpContext.Session.SetString("UserType", user.IsCompany ? "Company" : "Investor");
                        _httpContextAccessor.HttpContext.Session.SetString("UserRole", targetRole);
                        _httpContextAccessor.HttpContext.Session.SetString("UserName", realUserName ?? realEmail);
                        _httpContextAccessor.HttpContext.Session.SetString("Email", realEmail);
                        _httpContextAccessor.HttpContext.Session.SetString("OriginalIP", currentIp ?? "unknown");
                        _httpContextAccessor.HttpContext.Session.SetString("OriginalUA", currentUa);

                        if (user.IsCompany)
                        {
                            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AppUserId == user.Id);
                            if (company != null)
                            {
                                _httpContextAccessor.HttpContext.Session.SetString("companyId", company.Id.ToString());
                            }
                        }
                        else if (user.IsInvestor)
                        {
                            var investor = await _context.Investors.FirstOrDefaultAsync(i => i.AppUserId == user.Id);
                            if (investor != null)
                            {
                                _httpContextAccessor.HttpContext.Session.SetString("investorId", investor.Id.ToString());
                            }
                        }
                        await _httpContextAccessor.HttpContext.Session.CommitAsync();
                        var userSession = new UserSession
                        {
                            AppUserId = user.Id,
                            Username = user.EncryptedUserName,
                            LoginDate = DateTime.Now.ToLocalTime(),
                            IsOnline = true
                        };
                        if (userSession != null)
                        {
                            await _context.UserSessions.AddAsync(userSession);
                            await _context.SaveChangesAsync();

                            if (string.IsNullOrEmpty(login.ReturnUrl))
                                return true;
                            return false;
                        }
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LoginWithConfirmCodeAsync(UserLoginDto login)
        {
            try
            {
                var pseudoEmail = _pseudonymizationService.PseudonymizeEmail(login.Email);
                var user = await _userManager.FindByEmailAsync(pseudoEmail);
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User was null");

                Random random = new Random();
                int code;
                code = random.Next(100000, 1000000);

                if (user.IsActive == true && user.IsDeleted == false)
                {
                    var signInResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: true);
                    if (!signInResult.Succeeded)
                    {
                        return false;
                    }
                    else
                    {
                        if (user.IsLoginConfirmCodeActive == true)
                        {
                            var realEmail = _encryptionService.Decrypt(user.EncryptedEmail);

                            string subject = "investstartup.com Login Verification Code";
                            string body = $"Your confirmation code to complete the login transaction. This code is valid for 5 minutes. " +
                                          $"Do not share this code with anyone. This code has been sent for you to log in to the system: {code}";
                            await _mailService.SendEmail(realEmail, subject, body);
                            var authToken = Guid.NewGuid().ToString();
                            var creationTime = DateTime.Now;

                            _httpContextAccessor.HttpContext.Session.SetString("userId", user.Id);
                            _httpContextAccessor.HttpContext.Session.SetString("userLoginEmail", realEmail);
                            _httpContextAccessor.HttpContext.Session.SetString("confirmCode", code.ToString());
                            _httpContextAccessor.HttpContext.Session.SetString("authToken", authToken);
                            _httpContextAccessor.HttpContext.Session.SetString("tokenCreationTime", creationTime.ToString());

                            return true;
                        }
                    }
                    throw new Exception("Login was not successfull.");
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RegisterCompanyAsync(UserRegisterDto model)
        {
            try
            {
                var httpContext = new HttpContextAccessor().HttpContext;
                var audit = httpContext?.Items["CurrentAudit"] as Audit;

                var existingBlackListEntry = await _context.BlackLists
                 .Where(b =>
                (!string.IsNullOrEmpty(audit.RemoteIpAddress) && b.RemoteIpAddress == audit.RemoteIpAddress) ||
                (!string.IsNullOrEmpty(audit.IpAddressVPN) && b.IpAddressVPN == audit.IpAddressVPN) ||
                (!string.IsNullOrEmpty(audit.DeviceFingerprint) && b.DeviceFingerprint == audit.DeviceFingerprint) ||
                (!string.IsNullOrEmpty(audit.LocalIpAddress) && b.LocalIpAddress == audit.LocalIpAddress))
                 .Where(b => b.ExpirationDate > DateTime.Now).FirstOrDefaultAsync();

                if (existingBlackListEntry != null)
                {
                    return false;
                }
                else
                {
                    if (model == null)
                        throw new ArgumentNullException(nameof(model), "Model was null");

                    var nameSurnameParts = model.NameSurname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (nameSurnameParts.Length < 2)
                    {
                        throw new Exception("Please type to your full Name (ör: Name Surname).");
                    }

                    var firstName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[0]);
                    var lastName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[nameSurnameParts.Length - 1]);
                    var baseUsername = $"{firstName}{lastName}";
                    var username = $"startup.entrepreneur_{baseUsername}";

                    int suffix = 1;
                    while (await _userManager.FindByNameAsync(_pseudonymizationService.Pseudonymize(username)) != null)
                    {
                        username = $"startup.entrepreneur_{baseUsername}{suffix}";
                        suffix++;
                    }

                    Random random = new Random();
                    int code;
                    code = random.Next(100000, 1000000);

                    var pseudoEmail = _pseudonymizationService.PseudonymizeEmail(model.Email);
                    var pseudoUsername = _pseudonymizationService.Pseudonymize(username);

                    var user = new AppUser
                    {
                        NameSurname = _encryptionService.Encrypt(model.NameSurname),
                        Birthdate = model.Birthdate,
                        EncryptedBirthdate = _encryptionService.Encrypt(model.Birthdate.ToString("yyyy-MM-dd")),
                        PhoneNumber = _encryptionService.Encrypt(model.PhoneNumber),
                        Country = _encryptionService.Encrypt(model.Country),
                        Email = pseudoEmail,
                        UserName = pseudoUsername,
                        EncryptedEmail = _encryptionService.Encrypt(model.Email),
                        EncryptedUserName = _encryptionService.Encrypt(username),
                        Title = "Company",
                        IsAdmin = false,
                        IsCompany = true,
                        IsInvestor = false,
                        IsAcceptedPolicies = model.IsAcceptedPolicies,
                        ConfirmCode = code,
                        CreatedDate = DateTime.Now.ToLocalTime()
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "CompanyUsers");
                        _httpContextAccessor.HttpContext.Session.SetString("userEmail", model.Email);

                        string subject = "investstartup.com Company Register Verification Code";
                        string body = $"Your confirmation code to complete the register transaction. This code is valid for 5 minutes. " +
                                      $"Do not share this code with anyone. This code has been sent for you for register to the system: {code}";

                        await _mailService.SendEmail(model.Email, subject, body);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding new company user.", ex);
            }
        }

        public async Task<bool> RegisterInvestorAsync(UserRegisterDto model)
        {
            try
            {
                var httpContext = new HttpContextAccessor().HttpContext;
                var audit = httpContext?.Items["CurrentAudit"] as Audit;

                var existingBlackListEntry = await _context.BlackLists
                    .Where(b =>
                    (!string.IsNullOrEmpty(audit.RemoteIpAddress) && b.RemoteIpAddress == audit.RemoteIpAddress) ||
                    (!string.IsNullOrEmpty(audit.IpAddressVPN) && b.IpAddressVPN == audit.IpAddressVPN) ||
                    (!string.IsNullOrEmpty(audit.DeviceFingerprint) && b.DeviceFingerprint == audit.DeviceFingerprint) ||
                    (!string.IsNullOrEmpty(audit.LocalIpAddress) && b.LocalIpAddress == audit.LocalIpAddress))
                    .Where(b => b.ExpirationDate > DateTime.Now).FirstOrDefaultAsync();

                if (existingBlackListEntry != null)
                {
                    return false;
                }
                else
                {
                    if (model == null)
                        throw new ArgumentNullException(nameof(model), "Model was null");

                    var nameSurnameParts = model.NameSurname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (nameSurnameParts.Length < 2)
                    {
                        throw new Exception("Please type to your full Name (ör: Name Surname).");
                    }

                    var firstName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[0]);
                    var lastName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[nameSurnameParts.Length - 1]);
                    var baseUsername = $"{firstName}{lastName}";
                    var username = $"investor_{baseUsername}";

                    int suffix = 1;
                    while (await _userManager.FindByNameAsync(_pseudonymizationService.Pseudonymize(username)) != null)
                    {
                        username = $"investor_{baseUsername}{suffix}";
                        suffix++;
                    }

                    Random random = new Random();
                    int code;
                    code = random.Next(100000, 1000000);

                    var pseudoEmail = _pseudonymizationService.PseudonymizeEmail(model.Email);
                    var pseudoUsername = _pseudonymizationService.Pseudonymize(username);

                    var user = new AppUser
                    {
                        NameSurname = _encryptionService.Encrypt(model.NameSurname),
                        Birthdate = model.Birthdate,
                        EncryptedBirthdate = _encryptionService.Encrypt(model.Birthdate.ToString("yyyy-MM-dd")),
                        PhoneNumber = _encryptionService.Encrypt(model.PhoneNumber),
                        Country = _encryptionService.Encrypt(model.Country),
                        Email = pseudoEmail,
                        UserName = pseudoUsername,
                        EncryptedEmail = _encryptionService.Encrypt(model.Email),
                        EncryptedUserName = _encryptionService.Encrypt(username),
                        Title = "Investor",
                        IsAdmin = false,
                        IsInvestor = true,
                        IsCompany = false,
                        IsAcceptedPolicies = model.IsAcceptedPolicies,
                        ConfirmCode = code,
                        CreatedDate = DateTime.Now.ToLocalTime()
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "InvestorUsers");
                        _httpContextAccessor.HttpContext.Session.SetString("userEmail", model.Email);


                        string subject = "investstartup.com Investor Register Verification Code";
                        string body = $"Your confirmation code to complete the register transaction. This code is valid for 5 minutes. " +
                                      $"Do not share this code with anyone. This code has been sent for you for register to the system: {code}";

                        await _mailService.SendEmail(model.Email, subject, body);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding new company user.", ex);
            }
        }

        public async Task SendResetPasswordEmail(string email, string callbackUrl)
        {
            string subject = "Forgot My Password...";
            string body = $@"Password Reset Request <br/><br/> 
                     You can reset your password by clicking the link here. 
                     Please click the link: <a href='{callbackUrl}'>Password Reset Link</a>";

            await _mailService.SendEmail(email, subject, body);
        }

        public async Task<UserChangePasswordDto> GetChangePasswordAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(_httpContextAccessor.HttpContext.User)}'.");
                }
                var hasPassword = await _userManager.HasPasswordAsync(user);
                if (!hasPassword)
                {
                    throw new Exception("There is not a current password");
                }
                return new UserChangePasswordDto
                {
                    StatusMessage = "Success"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }
        public async Task<bool> ChangePasswordAsync(UserChangePasswordDto model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(_httpContextAccessor.HttpContext.User)}'.");
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    return false;
                }
                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while chancing password.", ex);
            }
        }
        public async Task<UserUpdateProfileDto> GetUpdateProfileAsync(UserUpdateProfileDto model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                if (user == null)
                    throw new ArgumentNullException(nameof(user), "user was null");

                model.PhoneNumber = _encryptionService.Decrypt(user.PhoneNumber);
                model.Email = _encryptionService.Decrypt(user.EncryptedEmail);
                model.Country = _encryptionService.Decrypt(user.Country);
                model.IsLoginConfirmCodeActive = user.IsLoginConfirmCodeActive;
                if (model != null)
                {
                    return model;
                }
                throw new Exception("Model was null");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }
        public async Task<bool> UpdateProfileAsync(UserUpdateProfileDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(model), "Model was null");

                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "user was null");

                user.PhoneNumber = _encryptionService.Encrypt(model.PhoneNumber);
                user.Country = _encryptionService.Encrypt(model.Country);
                var currentRealEmail = _encryptionService.Decrypt(user.EncryptedEmail);
                if (!string.Equals(currentRealEmail, model.Email, StringComparison.OrdinalIgnoreCase))
                {
                    var newPseudoEmail = _pseudonymizationService.PseudonymizeEmail(model.Email);
                    user.Email = newPseudoEmail;
                    user.NormalizedEmail = newPseudoEmail.ToUpperInvariant();
                    user.EncryptedEmail = _encryptionService.Encrypt(model.Email);
                }

                user.IsLoginConfirmCodeActive = model.IsLoginConfirmCodeActive;
                user.UpdatedDate = DateTime.Now.ToLocalTime();

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating profile.", ex);
            }
        }
        public async Task<bool> LogoutAsync()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var activeSession = await _context.UserSessions.Where(s => s.AppUserId == userId && s.IsActive && !s.IsDeleted).OrderByDescending(s => s.LoginDate).FirstOrDefaultAsync();

                if (activeSession == null)
                {
                    await _signInManager.SignOutAsync();
                    _httpContextAccessor.HttpContext.Session.Clear();
                    return true;
                }
                activeSession.LogoutDate = DateTime.Now.ToLocalTime();
                var duration = activeSession.LogoutDate.Value - activeSession.LoginDate;
                activeSession.OnlineDurationSeconds = (int)Math.Round(duration.TotalSeconds);

                activeSession.IsOnline = false;
                await _context.SaveChangesAsync();
                await _signInManager.SignOutAsync();
                _httpContextAccessor.HttpContext.Session.Clear();

                return true;
            }
            catch (Exception)
            {
                await _signInManager.SignOutAsync();
                _httpContextAccessor.HttpContext.Session.Clear();
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return false;
            }
        }
        public async Task<bool> ConfirmMailAsync(UserConfirmCodeDto model, string value)
        {
            try
            {
                value = _httpContextAccessor.HttpContext.Session.GetString("userEmail");
                if (value != null)
                {
                    var pseudoEmail = _pseudonymizationService.PseudonymizeEmail(value);
                    var user = await _userManager.FindByEmailAsync(pseudoEmail);
                    if (user != null)
                    {
                        if (user.ConfirmCode == model.ConfirmCode)
                        {
                            return true;
                        }
                        throw new Exception("Confirm codes are not same");
                    }
                    throw new ArgumentNullException(nameof(user), "user was null");
                }
                throw new ArgumentNullException(nameof(value), "value was null");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while confirming email.", ex);
            }
        }
        public async Task<bool> LoginConfirmMailAsync(UserLoginConfirmCodeDto model, string value)
        {
            try
            {
                value = _httpContextAccessor.HttpContext.Session.GetString("userLoginEmail");
                var storedCode = _httpContextAccessor.HttpContext.Session.GetString("confirmCode");
                var tokenCreationTime = _httpContextAccessor.HttpContext.Session.GetString("tokenCreationTime");

                if (value != null)
                {
                    var pseudoEmail = _pseudonymizationService.PseudonymizeEmail(value);
                    var user = await _userManager.FindByEmailAsync(pseudoEmail);
                    if (user != null)
                    {
                        if (storedCode != null && tokenCreationTime != null)
                        {
                            var creationTime = DateTime.Parse(tokenCreationTime);
                            if ((DateTime.Now - creationTime).TotalSeconds > 300)
                            {
                                throw new Exception("Token has expired. Please request a new code.");
                            }

                            if (storedCode == Convert.ToString(model.LoginConfirmCode))
                            {
                                var storedToken = _httpContextAccessor.HttpContext.Session.GetString("authToken");
                                if (string.IsNullOrEmpty(storedToken))
                                {
                                    throw new Exception("Authentication token not found in session.");
                                }
                                var userId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                                if (user.Id != userId)
                                {
                                    throw new Exception("Invalid user session.");
                                }
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                _httpContextAccessor.HttpContext.Session.SetString("userId", user.Id);
                                _httpContextAccessor.HttpContext.Session.SetString("userLoginEmail", value);
                                string targetRole = user.IsCompany ? "CompanyUsers" : "InvestorUsers";
                                var currentRoles = await _userManager.GetRolesAsync(user);
                                if (!currentRoles.Contains(targetRole))
                                {
                                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                                    await _userManager.AddToRoleAsync(user, targetRole);
                                }

                                var currentIp = _webHelperService.GetClientIp();
                                var currentUa = _httpContextAccessor.HttpContext.Request.Headers.UserAgent.ToString();
                                var realEmail = _encryptionService.Decrypt(user.EncryptedEmail);
                                var realUserName = _encryptionService.Decrypt(user.EncryptedUserName);

                                _httpContextAccessor.HttpContext.Session.SetString("UserType", user.IsCompany ? "Company" : "Investor");
                                _httpContextAccessor.HttpContext.Session.SetString("UserRole", targetRole);
                                _httpContextAccessor.HttpContext.Session.SetString("UserName", realUserName ?? realEmail);
                                _httpContextAccessor.HttpContext.Session.SetString("Email", realEmail);
                                _httpContextAccessor.HttpContext.Session.SetString("OriginalIP", currentIp ?? "unknown");
                                _httpContextAccessor.HttpContext.Session.SetString("OriginalUA", currentUa);

                                if (user.IsCompany)
                                {
                                    var company = await _context.Companies.FirstOrDefaultAsync(c => c.AppUserId == user.Id);
                                    if (company != null)
                                    {
                                        _httpContextAccessor.HttpContext.Session.SetString("companyId", company.Id.ToString());
                                    }
                                }
                                else if (user.IsInvestor)
                                {
                                    var investor = await _context.Investors.FirstOrDefaultAsync(i => i.AppUserId == user.Id);
                                    if (investor != null)
                                    {
                                        _httpContextAccessor.HttpContext.Session.SetString("investorId", investor.Id.ToString());
                                    }
                                }

                                await _httpContextAccessor.HttpContext.Session.CommitAsync();
                                var userSession = new UserSession
                                {
                                    AppUserId = user.Id,
                                    Username = user.EncryptedUserName,
                                    LoginDate = DateTime.Now.ToLocalTime(),
                                    IsOnline = true
                                };
                                _context.UserSessions.Add(userSession);
                                await _context.SaveChangesAsync();
                                if (string.IsNullOrEmpty(model.ReturnUrl))
                                    return true;
                                return false;
                            }
                            return false;
                        }
                        throw new Exception("Stored code was null");
                    }
                    throw new ArgumentNullException(nameof(user), "user was null");
                }
                throw new ArgumentNullException(nameof(value), "value was null");
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while confirming email.", ex);
            }
        }

        public async Task<bool> ResetPassword(UserResetPasswordDto model, string code)
        {
            try
            {
                if (code == null)
                    throw new ArgumentNullException(nameof(code), "code was null");

                if (model == null)
                    throw new ArgumentNullException(nameof(model), "model was null");

                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    return false;
                }

                AppUser user = new AppUser();
                var pseudoEmail = _pseudonymizationService.PseudonymizeEmail(model.Email);
                if (pseudoEmail == null)
                    throw new ArgumentNullException(nameof(pseudoEmail), "pseudoEmail was null");

                user = await _userManager.FindByEmailAsync(pseudoEmail);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "user was null");
                }
                else
                {
                    var decodedCodeBytes = WebEncoders.Base64UrlDecode(code);
                    var originalToken = Encoding.UTF8.GetString(decodedCodeBytes);
                    var result = await _userManager.ResetPasswordAsync(user, originalToken, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while reset password.", ex);
            }
        }

        public async Task<string?> GetUserCompanyIdAsync(string userId)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.AppUserId == userId);
            return company?.Id.ToString();
        }

        public async Task<string?> GetUserInvestorIdAsync(string userId)
        {
            var investor = await _context.Investors.FirstOrDefaultAsync(i => i.AppUserId == userId);
            return investor?.Id.ToString();
        }

        public async Task UpdateHeartbeatAsync(string userId)
        {
            await _context.UserSessions.Where(s => s.AppUserId == userId && s.IsOnline && s.IsActive && !s.IsDeleted).ExecuteUpdateAsync(s => s.SetProperty(x => x.LastHeartbeat, DateTime.UtcNow));
        }
    }
}
