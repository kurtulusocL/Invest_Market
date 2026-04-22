using Investigation.Shared.Dtos.AuthDtos.UserAuthDtos;

namespace Investigation.Business.Services.Abstract
{
    public interface IUserAuthService
    {
        Task<bool> LoginAsync(UserLoginDto login);
        Task<bool> LoginWithConfirmCodeAsync(UserLoginDto login);
        Task<bool> RegisterCompanyAsync(UserRegisterDto model);
        Task<bool> RegisterInvestorAsync(UserRegisterDto model);
        Task<bool> ConfirmMailAsync(UserConfirmCodeDto model, string value);
        Task<bool> LoginConfirmMailAsync(UserLoginConfirmCodeDto model, string value);
        Task<UserChangePasswordDto> GetChangePasswordAsync();
        Task<bool> ChangePasswordAsync(UserChangePasswordDto model);
        Task<bool> ResetPassword(UserResetPasswordDto model, string code);
        Task SendResetPasswordEmail(string email, string callbackUrl);
        Task<UserUpdateProfileDto> GetUpdateProfileAsync(UserUpdateProfileDto model);
        Task<bool> UpdateProfileAsync(UserUpdateProfileDto model);
        Task<bool> LogoutAsync();
        Task UpdateHeartbeatAsync(string userId);
        Task<string?> GetUserCompanyIdAsync(string userId);
        Task<string?> GetUserInvestorIdAsync(string userId);
    }
}
