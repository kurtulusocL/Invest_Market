using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class AppRoleHub : Hub
    {
        readonly IRoleService _roleService;
        public AppRoleHub(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<IEnumerable<AppRoleDto>> GetAllAsync()
        {
            try
            {
                var data = await _roleService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new AppRoleDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        NormalizedName = i.NormalizedName,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted,
                        ConcurrencyStamp = i.ConcurrencyStamp
                    }).ToList();
                }
                return new List<AppRoleDto>();
            }
            catch (Exception)
            {
                return new List<AppRoleDto>();
            }
        }
    }
}
