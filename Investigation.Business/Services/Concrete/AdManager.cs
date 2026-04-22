using System.Linq.Expressions;
using System.Security.Claims;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class AdManager : IAdService
    {
        readonly IAdRepository _adrepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        public AdManager(IAdRepository adRepository, IHttpContextAccessor httpContextAccessor)
        {
            _adrepository = adRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateAsync(Ad entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/ad/");
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
                        var result = await _adrepository.AddAsync(entity);
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
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Ad entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _adrepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _adrepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Ad>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _adrepository.GetAllIncludeAsync(new Expression<Func<Ad, bool>>[]
                {

                }, null, y => y.Hits, y => y.AdTargets);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Ad>();
            }
        }

        public IEnumerable<Ad> GetAllIncludingAdForLeftSidebar1(string userId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                userId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var data = _adrepository.GetAllPersonalizedAdsForUser(userId);
                return data.DistinctBy(a => a.Id).OrderByDescending(_ => Guid.NewGuid()).Take(1).ToList();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IEnumerable<Ad> GetAllIncludingAdForRightSidebar1(string userId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                userId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var personalizedAds = _adrepository.GetAllPersonalizedAdsForUser(userId).DistinctBy(a => a.Id).OrderByDescending(_ => Guid.NewGuid()).Skip(1).Take(1).ToList();

                if (!personalizedAds.Any())
                {
                    personalizedAds = _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                    {
                        i => i.IsActive == true,
                        i => i.IsDeleted == false
                    }).DistinctBy(a => a.Id).OrderBy(_ => Guid.NewGuid()).Take(1).ToList();
                }
                return personalizedAds;
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IEnumerable<Ad> GetAllIncludingAdForRightSidebar2(string userId)
        {
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                userId = userIdClaim ?? sessionUserId;
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var personalizedAds = _adrepository.GetAllPersonalizedAdsForUser(userId).DistinctBy(a => a.Id).OrderByDescending(_ => Guid.NewGuid()).Skip(2).Take(1).ToList();

                if (!personalizedAds.Any())
                {
                    personalizedAds = _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                    {
                        i => i.IsActive == true,
                        i => i.IsDeleted == false
                    })
                    .DistinctBy(a => a.Id).OrderBy(_ => Guid.NewGuid()).Take(1).ToList();
                }
                return personalizedAds;
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IEnumerable<Ad> GetAllIncludingAdForRightSidebar3(string userId)
        {
            const int count = 5;
            try
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                userId = userIdClaim ?? sessionUserId;
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var personalizedAds = _adrepository.GetAllPersonalizedAdsForUser(userId).DistinctBy(a => a.Id).OrderBy(_ => Guid.NewGuid()).Skip(3).Take(1).ToList();

                if (!personalizedAds.Any())
                {
                    personalizedAds = _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                    {
                        i => i.IsActive == true,
                        i => i.IsDeleted == false
                    }).DistinctBy(a => a.Id).OrderByDescending(_ => Guid.NewGuid()).Take(1).ToList();
                }
                return personalizedAds;
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IQueryable<Ad> GetAllIncludingAsync()
        {
            try
            {
                var data = _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Hits, y => y.AdTargets);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IQueryable<Ad> GetAllIncludingByFinisheDateAsync()
        {
            try
            {
                var data = _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Hits, y => y.AdTargets);
                return data.OrderByDescending(i => i.FinishDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IQueryable<Ad> GetAllIncludingByStartDateAsync()
        {
            try
            {
                var data = _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Hits, y => y.AdTargets);
                return data.OrderBy(i => i.StartDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IQueryable<Ad> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {

                }, null, y => y.Hits, y => y.AdTargets);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IQueryable<Ad> GetAllIncludingNoTargetAdAsync()
        {
            try
            {
                var data = _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.HasTarget==false
                }, null, y => y.Hits, y => y.AdTargets);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IQueryable<Ad> GetAllIncludingPublicAdRandom1()
        {
            try
            {
                return _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null).OrderByDescending(i => Guid.NewGuid()).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IQueryable<Ad> GetAllIncludingPublicAdRandom2()
        {
            try
            {
                return _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null).OrderByDescending(i => Guid.NewGuid()).Skip(1).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public IQueryable<Ad> GetAllIncludingTargetfullAdAsync()
        {
            try
            {
                var data = _adrepository.GetAllInclude(new Expression<Func<Ad, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.HasTarget==true
                }, null, y => y.Hits, y => y.AdTargets);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Ad>().AsQueryable();
            }
        }

        public async Task<Ad> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _adrepository.GetIncludeAsync(i => i.Id == id, y => y.Hits, y => y.AdTargets);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Ad ReadNonUniqueHit(int? id)
        {
            return _adrepository.ReadNonUniqueHit(id);
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _adrepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _adrepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _adrepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _adrepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(Ad entity, IFormFile image)
        {
            try
            {
                var errors = new List<string>();
                if (image != null)
                {
                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/img/ad/");
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
                        entity.UpdatedDate = DateTime.UtcNow;
                        var result = await _adrepository.UpdateAsync(entity);
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
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
