using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface ISocialMediaService
    {
        IQueryable<SocialMedia> GetAllAsync();
        IQueryable<SocialMedia> GetAllForAdminAsync();
        Task<IEnumerable<SocialMedia>> GetAllForSignalRAsync();
        Task<SocialMedia> GetByIdAsync(int? id);
        Task<bool> CreateAsync(SocialMedia entity, IFormFile image);
        Task<bool> UpdateAsync(SocialMedia entity, IFormFile image);
        Task<bool> DeleteAsync(SocialMedia entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<SocialMedia> GetAllForSitemap();
        IQueryable<SocialMedia> GetAllSocialMedia();
    }
}
