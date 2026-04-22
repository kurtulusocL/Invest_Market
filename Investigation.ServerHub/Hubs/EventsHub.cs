using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class EventsHub : Hub
    {
        readonly IEventsService _eventsService;
        public EventsHub(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }
        public async Task<IEnumerable<EventsDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _eventsService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new EventsDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Subtitle = i.Subtitle,
                        StartedDate = i.StartedDate,
                        EndDate = i.EndDate,
                        IsOnline = i.IsOnline,
                        DurationDay = i.DurationDay,
                        Content = i.Content,
                        Location = i.Location,
                        RedirectUrl = i.RedirectUrl,
                        ImageUrl = i.ImageUrl,
                        Hit = i.Hit,
                        Like = i.Like,
                        EventsCategoryDtoId = i.EventsCategoryId,
                        ParticipantCount = i.EventsParticipants?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<EventsDto>();
            }
            catch (Exception)
            {
                return new List<EventsDto>();
            }
        }
    }
}
