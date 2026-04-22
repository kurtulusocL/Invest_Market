using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IProfileImageService
    {
        IQueryable<ProfileImage> GetAllIncludingAsync();
        IQueryable<ProfileImage> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<ProfileImage> GetAllIncludingForAdminAsync();
        Task<IEnumerable<ProfileImage>> GetAllForSignalRAsync();
        Task<ProfileImage> GetByIdAsync(int? id);
        Task<ProfileImage> GetProfileImageByUserIdAsync(string userId);
        Task<bool> CreateAsync(string appUserId, IFormFile image);
        Task<bool> UpdateAsync(string appUserId, int id, IFormFile image);
        Task<bool> DeleteAsync(ProfileImage entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<ProfileImage> GetAllIncludeProfileImageByUserId(string userId);
        ProfileImage GetProfileImageByUserId(string userId);
    }
}
