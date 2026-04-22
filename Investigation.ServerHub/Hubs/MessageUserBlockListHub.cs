using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class MessageUserBlockListHub : Hub
    {
        readonly IBlockedMessageUserService _blockedMessageUserService;
        public MessageUserBlockListHub(IBlockedMessageUserService blockedMessageUserService)
        {
            _blockedMessageUserService = blockedMessageUserService;
        }
        public async Task<IEnumerable<MessageUserBlockListDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _blockedMessageUserService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new MessageUserBlockListDto
                    {
                        Id = i.Id,
                        IsBlocked = true,
                        IsRemoved = true,
                        BlockedUserName = i.BlockedUserName,
                        BlockedAt = i.BlockedAt,
                        BlockerDtoId = i.BlockerId,
                        BlockedDtoId = i.BlockedId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<MessageUserBlockListDto>();
            }
            catch (Exception)
            {
                return new List<MessageUserBlockListDto>();
            }
        }
    }
}
