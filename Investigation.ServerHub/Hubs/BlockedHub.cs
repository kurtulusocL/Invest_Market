using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class BlockedHub : Hub
    {
        readonly IBlockedService _blockedService;
        public BlockedHub(IBlockedService blockedService)
        {
            _blockedService = blockedService;
        }
        public async Task<IEnumerable<BlockedDto>> GetAllAsync()
        {
            try
            {
                var data = await _blockedService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new BlockedDto
                    {
                        Id = i.Id,
                        RemoteIpAddress = i.RemoteIpAddress,
                        IpAddressVPN = i.IpAddressVPN,
                        Host = i.Host,
                        Note = i.Note,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<BlockedDto>();
            }
            catch (Exception)
            {
                return new List<BlockedDto>();
            }
        }
    }
}
