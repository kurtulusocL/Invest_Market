using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IAdService
    {
        IQueryable<Ad> GetAllIncludingAsync();
        IQueryable<Ad> GetAllIncludingTargetfullAdAsync();
        IQueryable<Ad> GetAllIncludingNoTargetAdAsync();
        IQueryable<Ad> GetAllIncludingByStartDateAsync();
        IQueryable<Ad> GetAllIncludingByFinisheDateAsync();
        IQueryable<Ad> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Ad>> GetAllForSignalRAsync();
        Task<Ad> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Ad entity, IFormFile image);
        Task<bool> UpdateAsync(Ad entity, IFormFile image);
        Task<bool> DeleteAsync(Ad entity, int id);       
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        Ad ReadNonUniqueHit(int? id);
        IEnumerable<Ad> GetAllIncludingAdForRightSidebar1(string userId);
        IEnumerable<Ad> GetAllIncludingAdForRightSidebar2(string userId);
        IEnumerable<Ad> GetAllIncludingAdForRightSidebar3(string userId);
        IEnumerable<Ad> GetAllIncludingAdForLeftSidebar1(string userId);
        IQueryable<Ad> GetAllIncludingPublicAdRandom1();
        IQueryable<Ad> GetAllIncludingPublicAdRandom2();
    }
}
