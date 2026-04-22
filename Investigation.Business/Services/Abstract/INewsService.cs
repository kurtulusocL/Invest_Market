using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface INewsService
    {
        IQueryable<News> GetAllIncludingAsync();
        IQueryable<News> GetAllIncludingByMostLikedAsync();
        IQueryable<News> GetAllIncludingByMostHitAsync();
        IQueryable<News> GetAllIncludingForAdminAsync();
        Task<IEnumerable<News>> GetAllForSignalRAsync();
        Task<News> GetByIdAsync(int? id);
        Task<News?> GetBySlugAsync(string slug);
        Task<bool> CreateAsync(News entity, IFormFile image);
        Task<bool> UpdateAsync(News entity, IFormFile image);
        Task<bool> DeleteAsync(News entity, int id);
        Task<bool> LikeAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<News> GetAllNewsForPublicUser();
        IQueryable<News> GetAllForSitemap();
        News HitRead(int id);
    }
}
