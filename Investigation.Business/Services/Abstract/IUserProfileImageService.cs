using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IUserProfileImageService
    {
        IQueryable<UserProfileImage> GetAllIncludingAsync();
        IQueryable<UserProfileImage> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<UserProfileImage> GetAllIncludingForAdminAsync();
        IQueryable<UserProfileImage> GetAllIncludingProfileImageForUserByUserIdAsync(string userId);
        Task<IEnumerable<UserProfileImage>> GetAllForSignalRAsync();
        Task<UserProfileImage> GetByIdAsync(int? id);
        Task<UserProfileImage> GetProfileImageByUserIdAsync(string userId);
        Task<bool> CreateAsync(string appUserId, IFormFile image);
        Task<bool> UpdateAsync(string appUserId, int id, IFormFile image);
        Task<bool> DeleteAsync(UserProfileImage entity, int id);
        Task<bool>DeleteAllAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        UserProfileImage GetProfileImageByUserId(string userId);
    }
}
