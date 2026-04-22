using Investigation.Shared.Dtos.AuthDtos.AdminAuthDtos;
using Investigation.Shared.ViewModels.RoleVM;

namespace Investigation.Business.Services.Abstract
{
    public interface IAdminAuthService
    {
        Task<bool> LoginAsync(AdminLoginDto login);
        Task<bool> LoginWithConfirmCodeAsync(AdminLoginDto login);
        Task<bool> LoginConfirmMailAsync(AdminConfirmCodeDto model, string value);
        Task<bool> RegisterAsync(AdminRegisterDto model);
        Task<List<RoleAssignVM>> GetRoleAssingAsync(string id);
        Task<bool> RoleAssignAsync(List<RoleAssignVM> modelList, string id);
        Task<AdminChangePasswordDto> GetChangePasswordAsync();
        Task<bool> ChangePasswordAsync(AdminChangePasswordDto model);
        Task<AdminUpdateProfileDto> GetUpdateProfileAsync(AdminUpdateProfileDto model);
        Task<bool> UpdateProfileAsync(AdminUpdateProfileDto model);
        Task<bool> LogoutAsync();
    }
}
