using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class BlackListHub : Hub
    {
        readonly IBlackListService _blackListService;
        public BlackListHub(IBlackListService blackListService)
        {
            _blackListService = blackListService;
        }
        public async Task<IEnumerable<BlackListDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _blackListService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new BlackListDto
                    {
                        Id = i.Id,
                        RemoteIpAddress = i.RemoteIpAddress,
                        IpAddressVPN = i.IpAddressVPN,
                        ExpirationDate = i.ExpirationDate,
                        AuditDtoId = i.AuditId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<BlackListDto>();
            }
            catch (Exception)
            {
                return new List<BlackListDto>();
            }
        }
    }
}
