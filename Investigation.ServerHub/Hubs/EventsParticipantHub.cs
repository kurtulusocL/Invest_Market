using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class EventsParticipantHub : Hub
    {
        readonly IEventsParticipantService _eventsParticipantService;
        public EventsParticipantHub(IEventsParticipantService eventsParticipantService)
        {
            _eventsParticipantService = eventsParticipantService;
        }
        public async Task<IEnumerable<EventsParticipantDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _eventsParticipantService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new EventsParticipantDto
                    {
                        Id = i.Id,
                        NameSurname = i.NameSurname,
                        Title = i.Title,
                        JoinTime = i.JoinTime,
                        ShortDescription = i.ShortDescription,
                        ImageUrl = i.ImageUrl,
                        EventsDtoId = i.EventsId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<EventsParticipantDto>();
            }
            catch (Exception)
            {
                return new List<EventsParticipantDto>();
            }
        }
    }
}
