using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SendMessageHub : Hub
    {
        readonly ISendMessageService _sendMessageService;
        public SendMessageHub(ISendMessageService sendMessageService)
        {
            _sendMessageService = sendMessageService;
        }
        public async Task<IEnumerable<SendMessageDto>> GetAllAsync()
        {
            try
            {
                var data = await _sendMessageService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SendMessageDto
                    {
                        Id = i.Id,
                        NameSurname = i.NameSurname,
                        Email = i.Email,
                        PhoneNumber = i.PhoneNumber,
                        MessageTitle = i.MessageTitle,
                        MessageSubject = i.MessageSubject,
                        MessageContent = i.MessageContent,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SendMessageDto>();
            }
            catch (Exception)
            {
                return new List<SendMessageDto>();
            }
        }
    }
}
