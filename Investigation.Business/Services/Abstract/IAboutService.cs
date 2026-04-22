using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IAboutService
    {
        IQueryable<About> GetAllAsync();
        IQueryable<About> GetAllForAdminAsync();
        Task<IEnumerable<About>> GetAllForSignalRAsync();
        Task<About> GetByIdAsync(int? id);
        Task<bool> CreateAsync(About entity, IFormFile image);
        Task<bool> UpdateAsync(About entity, IFormFile image);
        Task<bool> DeleteAsync(About entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<About> GetAllAboutForPublicUser();
        IQueryable<About> GetAllAboutSiteMap();
    }
}
