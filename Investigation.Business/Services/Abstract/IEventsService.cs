using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IEventsService
    {
        IQueryable<Events> GetAllIncludingAsync();
        IQueryable<Events> GetAllIncludingByStartDateAsync();
        IQueryable<Events> GetAllIncludingByEndDateAsync();
        IQueryable<Events> GetAllIncludingByOnlineEventsAsync();
        IQueryable<Events> GetAllIncludingByOfflineEventsAsync();
        IQueryable<Events> GetAllIncludingForEventsCategoryIdAsync(int eventsCategoryId);
        IQueryable<Events> GetAllIncludingOpenEventsByCategoryIdAsync(int eventsCategoryId);
        IQueryable<Events> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Events>> GetAllForSignalRAsync();
        Task<Events> GetByIdAsync(int? id);
        Task<Events?> GetBySlugAsync(string slug);
        Task<bool> LikeAsync(int id);
        Task<bool> CreateAsync(Events entity, IFormFile? image);
        Task<bool> UpdateAsync(Events entity, IFormFile? image);
        Task<bool> DeleteAsync(Events entity, int id);
        Task<bool> DeleteAllAsync(List<int> ids);       
        Task<bool> SetOnlineAsync(int id);
        Task<bool> SetOfflineAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Events> GetAllIncludingEventsForSitemap();
        IQueryable<Events> GetAllIncludingUpComingEvents();
        IQueryable<Events> GetAllIncludingRandomOpenEvents();
        Events HitRead(int id);
    }
}
