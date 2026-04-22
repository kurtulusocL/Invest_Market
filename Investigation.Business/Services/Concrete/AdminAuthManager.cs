using System.Security.Claims;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Dtos.AuthDtos.AdminAuthDtos;
using Investigation.Shared.Helpers;
using Investigation.Shared.ViewModels.RoleVM;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace Investigation.Business.Services.Concrete
{
    public class AdminAuthManager : IAdminAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ApplicationDbContext _context;
        public AdminAuthManager(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<bool> LoginAsync(AdminLoginDto login)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User was null");

                if (user.IsActive == true && user.IsDeleted == false)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, true);

                    if (result.Succeeded)
                    {
                        string targetRole;
                        var currentRoles = await _userManager.GetRolesAsync(user);

                        if (currentRoles.Contains("Admin"))
                            targetRole = "Admin";
                        else if (currentRoles.Contains("WorkerAdmin"))
                            targetRole = "WorkerAdmin";
                        else if (currentRoles.Contains("AsistantAdmin"))
                            targetRole = "AsistantAdmin";
                        else if (currentRoles.Contains("HelperAdmin"))
                            targetRole = "HelperAdmin";
                        else
                            targetRole = "Admin";

                        if (!currentRoles.Contains(targetRole))
                        {
                            await _userManager.RemoveFromRolesAsync(user, currentRoles);
                            await _userManager.AddToRoleAsync(user, targetRole);
                        }
                        _httpContextAccessor.HttpContext.Session.SetString("adminId", user.Id.ToString());
                        _httpContextAccessor.HttpContext.Session.SetString("UserName", "Admin");
                        _httpContextAccessor.HttpContext.Session.SetString("UserRole", targetRole);

                        var userSession = new UserSession
                        {
                            AppUserId = user.Id,
                            Username = user.UserName,
                            LoginDate = DateTime.Now.ToLocalTime(),
                            IsOnline = true
                        };
                        if (userSession != null)
                        {
                            _context.UserSessions.Add(userSession);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        return false;
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

        public async Task<bool> LoginWithConfirmCodeAsync(AdminLoginDto login)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

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
                    if (user.IsLoginConfirmCodeActive == true)
                    {
                        MimeMessage mimeMessage = new MimeMessage();
                        MailboxAddress mailboxAddressFrom = new MailboxAddress("bi2bi.com", "registermail.activation@gmail.com");
                        MailboxAddress mailboxAddressTo = new MailboxAddress(user.UserName, user.Email);

                        mimeMessage.From.Add(mailboxAddressFrom);
                        mimeMessage.To.Add(mailboxAddressTo);

                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.TextBody = "Kayıt işlemini gerçekleştirmek için onay kodunuz. Bu kod 5 dakika geçerlidir. Bu kodu kimse ile paylaşmayınız. Bu kod sisteme giriş yapabilmeniz için gönderilmiştir. :" + code;
                        mimeMessage.Body = bodyBuilder.ToMessageBody();
                        mimeMessage.Subject = "bi2bi.com Giriş Onay Kodu";

                        SmtpClient client = new SmtpClient();
                        client.Connect("smtp.gmail.com", 587, false);
                        client.Authenticate("registermail.activation@gmail.com", "occi ounf bymu gqgv");
                        await client.SendAsync(mimeMessage);
                        client.Disconnect(true);

                        var authToken = Guid.NewGuid().ToString();
                        var creationTime = DateTime.Now;
                        _httpContextAccessor.HttpContext.Session.SetString("adminId", user.Id);
                        _httpContextAccessor.HttpContext.Session.SetString("adminLoginEmail", user.Email);
                        _httpContextAccessor.HttpContext.Session.SetString("confirmCode", code.ToString());
                        _httpContextAccessor.HttpContext.Session.SetString("authToken", authToken);
                        _httpContextAccessor.HttpContext.Session.SetString("tokenCreationTime", creationTime.ToString());

                        return true;
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

        public async Task<bool> LoginConfirmMailAsync(AdminConfirmCodeDto model, string value)
        {
            try
            {
                value = _httpContextAccessor.HttpContext.Session.GetString("adminLoginEmail");
                var storedCode = _httpContextAccessor.HttpContext.Session.GetString("confirmCode");
                var tokenCreationTime = _httpContextAccessor.HttpContext.Session.GetString("tokenCreationTime");

                if (value != null)
                {
                    var user = await _userManager.FindByEmailAsync(value);
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
                                var userId = _httpContextAccessor.HttpContext.Session.GetString("adminId");
                                if (user.Id != userId)
                                {
                                    throw new Exception("Invalid user session.");
                                }
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                _httpContextAccessor.HttpContext.Session.SetString("adminId", user.Id);
                                _httpContextAccessor.HttpContext.Session.SetString("adminLoginEmail", user.Email);
                                var userSession = new UserSession
                                {
                                    AppUserId = user.Id,
                                    Username = user.UserName,
                                    LoginDate = DateTime.Now.ToLocalTime(),
                                    IsOnline = true
                                };
                                if (userSession != null)
                                {
                                    _context.UserSessions.Add(userSession);
                                    await _context.SaveChangesAsync();

                                    if (string.IsNullOrEmpty(model.ReturnUrl))
                                        return true;
                                    return false;
                                }
                                return true;
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

        public async Task<bool> RegisterAsync(AdminRegisterDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(model), "model was null");

                var nameSurnameParts = model.NameSurname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nameSurnameParts.Length < 2)
                {
                    throw new Exception("Please type full Name (ör: Name Surname).");
                }

                var firstName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[0]);
                var lastName = ConvertTurkishToEnglishHelper.ConvertTurkishToEnglish(nameSurnameParts[nameSurnameParts.Length - 1]);
                var baseUsername = $"{firstName}{lastName}";
                var username = $"admin_{baseUsername}";

                int suffix = 1;
                while (await _userManager.FindByNameAsync(username) != null)
                {
                    username = $"admin_{baseUsername}{suffix}";
                    suffix++;
                }

                var user = new AppUser
                {
                    NameSurname = model.NameSurname,
                    UserName = username,
                    Email = model.Email,
                    Country = model.Country,
                    PhoneNumber = model.PhoneNumber,
                    Title = model.Title,
                    Birthdate = model.Birthdate,
                    IsAdmin = true,
                    IsAcceptedPolicies = true,
                    IsRegisterConfirmCodeActive = false,
                    IsLoginConfirmCodeActive = false,
                    CreatedDate = DateTime.Now.ToLocalTime()
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding new admin.", ex);
            }
        }
        public async Task<List<RoleAssignVM>> GetRoleAssingAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "Id was null");

                AppUser user = await _userManager.FindByIdAsync(id);
                List<AppRole> allRoles = _roleManager.Roles.ToList();
                List<string>? userRoles = await _userManager.GetRolesAsync(user) as List<string>;
                List<RoleAssignVM> assignRoles = new List<RoleAssignVM>();
                allRoles.ForEach(role => assignRoles.Add(new RoleAssignVM
                {
                    HasAssign = userRoles.Contains(role.Name),
                    RoleId = role.Id,
                    RoleName = role.Name
                }));
                return assignRoles;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
        public async Task<bool> RoleAssignAsync(List<RoleAssignVM> modelList, string id)
        {
            try
            {
                AppUser user = await _userManager.FindByIdAsync(id);
                foreach (RoleAssignVM role in modelList)
                {
                    if (role.HasAssign)
                        await _userManager.AddToRoleAsync(user, role.RoleName);
                    else
                        await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting admin role.", ex);
            }
        }
        public async Task<AdminChangePasswordDto> GetChangePasswordAsync()
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
                return new AdminChangePasswordDto
                {
                    StatusMessage = "Success"
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> ChangePasswordAsync(AdminChangePasswordDto model)
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

        public async Task<AdminUpdateProfileDto> GetUpdateProfileAsync(AdminUpdateProfileDto model)
        {
            try
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                if (user == null)
                    throw new ArgumentNullException(nameof(user), "user was null");

                model.PhoneNumber = user.PhoneNumber;
                model.Email = user.Email;
                model.Country = user.Country;
                model.Title = user.Title;
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

        public async Task<bool> UpdateProfileAsync(AdminUpdateProfileDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(model), "Model was null");

                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "user was null");

                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.Country = model.Country;
                user.Title = model.Title;
                user.IsLoginConfirmCodeActive = model.IsLoginConfirmCodeActive;
                user.UpdatedDate = DateTime.Now.ToLocalTime();

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return true;
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
            catch (Exception ex)
            {
                await _signInManager.SignOutAsync();
                _httpContextAccessor.HttpContext.Session.Clear();
                throw new Exception("An unexpected error occurred while doing logout.", ex);
            }
        }
    }
}
