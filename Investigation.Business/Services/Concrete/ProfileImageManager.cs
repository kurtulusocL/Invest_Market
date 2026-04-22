using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class ProfileImageManager : IProfileImageService
    {
        readonly IProfileImageRepository _profileImageRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        public ProfileImageManager(IProfileImageRepository profileImageRepository, IHttpContextAccessor httpContextAccessor)
        {
            _profileImageRepository = profileImageRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(string appUserId, IFormFile image)
        {
            try
            {
                appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("adminId");
                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new ArgumentNullException(nameof(appUserId), "userId was null");
                }

                var entity = new ProfileImage
                {
                    AppUserId = appUserId
                };
                if (entity != null)
                {
                    var errors = new List<string>();
                    if (image != null)
                    {
                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/profileImage/");
                        if (!Directory.Exists(directoryPath))
                        {
                            Console.WriteLine($"Path is preparing: {directoryPath}");
                            Directory.CreateDirectory(directoryPath);
                        }
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(directoryPath, fileName);
                        try
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }
                            entity.ImageUrl = fileName;
                            var result = await _profileImageRepository.AddAsync(entity);
                            if (!result)
                            {
                                errors.Add($"Error {fileName}.");
                            }
                            return true;
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Error {fileName} : {ex.Message}");
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

        public async Task<bool> DeleteAsync(ProfileImage entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _profileImageRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _profileImageRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ProfileImage>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _profileImageRepository.GetAllIncludeAsync(new Expression<Func<ProfileImage, bool>>[]
                {
                   
                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ProfileImage>();
            }
        }

        public IQueryable<ProfileImage> GetAllIncludeProfileImageByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _profileImageRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<ProfileImage, bool>>[]
                {
                   i=>i.IsActive==true,
                   i=>i.IsDeleted==false
                }, y => y.AppUser).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ProfileImage>().AsQueryable();
            }
        }

        public IQueryable<ProfileImage> GetAllIncludingAsync()
        {
            try
            {
                var data = _profileImageRepository.GetAllInclude(new Expression<Func<ProfileImage, bool>>[]
                {
                   i=>i.IsActive==true,
                   i=>i.IsDeleted==false
                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ProfileImage>().AsQueryable();
            }
        }

        public IQueryable<ProfileImage> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _profileImageRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<ProfileImage, bool>>[]
                {
                   i=>i.IsActive==true,
                   i=>i.IsDeleted==false
                }, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ProfileImage>().AsQueryable();
            }
        }

        public IQueryable<ProfileImage> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _profileImageRepository.GetAllInclude(new Expression<Func<ProfileImage, bool>>[]
                {

                }, null, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ProfileImage>().AsQueryable();
            }
        }

        public async Task<ProfileImage> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _profileImageRepository.GetIncludeAsync(i => i.Id == id, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public ProfileImage GetProfileImageByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _profileImageRepository.Get(i => i.AppUserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<ProfileImage> GetProfileImageByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return await _profileImageRepository.GetIncludeAsync(i => i.AppUserId == userId, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _profileImageRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _profileImageRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _profileImageRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _profileImageRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string appUserId, int id, IFormFile image)
        {
            try
            {
                appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("adminId");
                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new ArgumentNullException(nameof(appUserId), "userId was null");
                }

                var entity = new ProfileImage
                {
                    AppUserId = appUserId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                if (entity != null)
                {
                    var errors = new List<string>();
                    if (image != null)
                    {
                        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/profileImage/");
                        if (!Directory.Exists(directoryPath))
                        {
                            Console.WriteLine($"Path is preparing: {directoryPath}");
                            Directory.CreateDirectory(directoryPath);
                        }
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(directoryPath, fileName);
                        try
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(stream);
                            }
                            entity.ImageUrl = fileName;
                            var result = await _profileImageRepository.UpdateAsync(entity);
                            if (!result)
                            {
                                errors.Add($"Error {fileName}.");
                            }
                            return true;
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Error {fileName} : {ex.Message}");
                        }
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