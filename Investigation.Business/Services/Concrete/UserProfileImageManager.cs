using System.Linq.Expressions;
using System.Security.Claims;
using Investigation.Business.Constants.Helpers;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class UserProfileImageManager : IUserProfileImageService
    {
        readonly IUserProfileImageRepository _userProfileImageRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        public UserProfileImageManager(IUserProfileImageRepository userProfileImageRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userProfileImageRepository = userProfileImageRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string appUserId, IFormFile image)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value
                        ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var entity = new UserProfileImage
                {
                    AppUserId = appUserId
                };
                if (entity != null)
                {
                    if (image != null && image.Length > 0)
                    {
                        ServiceImageHelper.ImageValidation(image);
                        try
                        {
                            string savedFileName = await ServiceImageHelper.UserProfileImageResize(image);

                            entity.ImageUrl = savedFileName;
                            var result = await _userProfileImageRepository.AddAsync(entity);
                            if (!result)
                            {
                                return false;
                            }
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAllAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _userProfileImageRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(UserProfileImage entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _userProfileImageRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userProfileImageRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<UserProfileImage>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _userProfileImageRepository.GetAllIncludeAsync(new Expression<Func<UserProfileImage, bool>>[]
                {
                   
                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserProfileImage>();
            }
        }

        public IQueryable<UserProfileImage> GetAllIncludingAsync()
        {
            try
            {
                var data = _userProfileImageRepository.GetAllInclude(new Expression<Func<UserProfileImage, bool>>[]
                {
                   i=>i.IsActive==true,
                   i=>i.IsDeleted==false
                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserProfileImage>().AsQueryable();
            }
        }

        public IQueryable<UserProfileImage> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _userProfileImageRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<UserProfileImage, bool>>[]
                {
                   i=>i.IsActive==true,
                   i=>i.IsDeleted==false
                }, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserProfileImage>().AsQueryable();
            }
        }

        public IQueryable<UserProfileImage> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _userProfileImageRepository.GetAllInclude(new Expression<Func<UserProfileImage, bool>>[]
                {

                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserProfileImage>().AsQueryable();
            }
        }

        public IQueryable<UserProfileImage> GetAllIncludingProfileImageForUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _userProfileImageRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<UserProfileImage, bool>>[]
                {
                   i=>i.IsActive==true,
                   i=>i.IsDeleted==false
                }, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserProfileImage>().AsQueryable();
            }
        }

        public async Task<UserProfileImage> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _userProfileImageRepository.GetIncludeAsync(i => i.Id == id, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public UserProfileImage GetProfileImageByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _userProfileImageRepository.GetInclude(i => i.AppUserId == userId, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<UserProfileImage> GetProfileImageByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return await _userProfileImageRepository.GetIncludeAsync(i => i.AppUserId == userId, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _userProfileImageRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _userProfileImageRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _userProfileImageRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _userProfileImageRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string appUserId, int id, IFormFile image)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.UserProfileImageResize(image);
                        var entity = new UserProfileImage
                        {
                            AppUserId = appUserId,
                            ImageUrl = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };

                        var result = await _userProfileImageRepository.UpdateAsync(entity);
                        if (!result)
                        {
                            return false;
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
