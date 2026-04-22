using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class AuditHub : Hub
    {
        readonly IAuditService _auditService;
        public AuditHub(IAuditService auditService)
        {
            _auditService = auditService;
        }
        public async Task<IEnumerable<AuditDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _auditService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new AuditDto
                    {
                        Id = i.Id,
                        UserName = i.UserName,
                        UserId = i.UserId,
                        BlackListCount = i.BlackLists?.Count ?? 0,
                        Port = i.Port,
                        AreaAccessed = i.AreaAccessed,
                        Browser = i.Browser,
                        Device = i.Device,
                        Language = i.Language,
                        BrowserVersion = i.BrowserVersion,
                        IsMobile = i.IsMobile,
                        DeviceModel = i.DeviceModel,
                        Platform = i.Platform,
                        RemoteIpAddress = i.RemoteIpAddress,
                        IpAddressVPN = i.IpAddressVPN,
                        Host = i.Host,
                        ProxyConnection = i.ProxyConnection,
                        InternetServiceProvider = i.InternetServiceProvider,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<AuditDto>();
            }
            catch (Exception)
            {
                return new List<AuditDto>();
            }
        }
    }
}
