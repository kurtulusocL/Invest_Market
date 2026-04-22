using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IEventsParticipantService
    {
        IQueryable<EventsParticipant> GetAllIncludingAsync();
        IQueryable<EventsParticipant> GetAllIncludingByEventsIdAsync(int? eventsId);
        IQueryable<EventsParticipant> GetAllIncludingByJoinDateAsync();
        IQueryable<EventsParticipant> GetAllIncludingForAdminAsync();
        Task<IEnumerable<EventsParticipant>> GetAllForSignalRAsync();
        Task<EventsParticipant> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string nameSurname, string title, DateTime joinTime, string shortDescription, int? eventsId, IFormFile image);
        Task<bool> UpdateAsync(string nameSurname, string title, DateTime joinTime, string shortDescription, int? eventsId, IFormFile image, int id);
        Task<bool> DeleteAsync(EventsParticipant entity, int id);
        Task<bool> DeleteAllAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<EventsParticipant> GetAllIncludingEventsParticipantsRandom();
        IQueryable<EventsParticipant> GetAllIncludingEventsParticipantByEventsId(int? eventsId);
        IQueryable<EventsParticipant> GetAllIncludingForSitemap();
    }
}
