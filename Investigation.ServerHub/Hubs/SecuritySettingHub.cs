using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SecuritySettingHub : Hub
    {
        readonly ISecuritySettingService _securitySettingService;
        public SecuritySettingHub(ISecuritySettingService securitySettingService)
        {
            _securitySettingService = securitySettingService;
        }
        public async Task<IEnumerable<SecuritySettingDto>> GetAllAsync()
        {
            try
            {
                var data = await _securitySettingService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SecuritySettingDto
                    {
                        Id = i.Id,
                        Value = i.Value,
                        Type = i.Type,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SecuritySettingDto>();
            }
            catch (Exception)
            {
                return new List<SecuritySettingDto>();
            }
        }
    }
}
