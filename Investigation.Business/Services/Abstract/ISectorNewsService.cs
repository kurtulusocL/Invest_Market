using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface ISectorNewsService
    {
        IQueryable<SectorNews> GetAllIncludingAsync();
        IQueryable<SectorNews> GetAllIncludingByMostLikeAsync();
        IQueryable<SectorNews> GetAllIncludingByMostHitAsync();
        IQueryable<SectorNews> GetAllIncludingForAdminAsync();
        Task<IEnumerable<SectorNews>> GetAllForSignalRAsync();
        Task<SectorNews> GetByIdAsync(int? id);
        Task<SectorNews?> GetBySlugAsync(string slug);
        Task<bool> CreateAsync(SectorNews entity, IFormFile image);
        Task<bool> UpdateAsync(SectorNews entity, IFormFile image);
        Task<bool> DeleteAsync(SectorNews entity, int id);
        Task<bool> LikeAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<SectorNews> GetAllForSitemap();
        IQueryable<SectorNews> GetAllIncludingLastSectorNews();
        IQueryable<SectorNews> GetAllIncludingLastSectorNewsForIndex();
        IQueryable<SectorNews> GetAllIncludingLastSectorNewsForTimeline();
        IQueryable<SectorNews> GetAllIncludingSectorNewsRandom();
        IQueryable<SectorNews> GetAllIncludingSectorNewsPopular();
        IQueryable<SectorNews> GetAllSectorNewsForPublicUser();
        SectorNews HitRead(int id);
    }
}
