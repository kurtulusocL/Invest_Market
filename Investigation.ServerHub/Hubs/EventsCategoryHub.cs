using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class EventsCategoryHub : Hub
    {
        readonly IEventsCategoryService _eventsCategoryService;
        public EventsCategoryHub(IEventsCategoryService eventsCategoryService)
        {
            _eventsCategoryService = eventsCategoryService;
        }
        public async Task<IEnumerable<EventsCategoryDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _eventsCategoryService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new EventsCategoryDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        EventsCount = i.Events?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<EventsCategoryDto>();
            }
            catch (Exception)
            {
                return new List<EventsCategoryDto>();
            }
        }
    }
}
