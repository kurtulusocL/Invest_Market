using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class MessageHub : Hub
    {
        readonly IMessageService _messageService;
        public MessageHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<IEnumerable<MessageDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _messageService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new MessageDto
                    {
                        Id = i.Id,
                        Content = i.Content,
                        IsRead = i.IsRead,
                        SentAt = i.SentAt,
                        SenderDtoId = i.SenderId,
                        ReceiverDtoId = i.ReceiverId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<MessageDto>();
            }
            catch (Exception)
            {
                return new List<MessageDto>();
            }
        }
    }
}
