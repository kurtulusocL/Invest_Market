using System.Linq.Expressions;
using System.Security.Claims;
using Ganss.Xss;
using Investigation.Business.Constants.Helpers;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Investigation.Business.Services.Concrete
{
    public class CompanyManager : ICompanyService
    {
        readonly ICompanyRepository _companyRepository;
        readonly ISectorService _sectorService;
        readonly ISubSectorService _subSectorService;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public CompanyManager(ICompanyRepository companyRepository, ISectorService sectorService, ISubSectorService subSectorService, IHttpContextAccessor httpContextAccessor, IHtmlSanitizer htmlSanitizer)
        {
            _companyRepository = companyRepository;
            _sectorService = sectorService;
            _subSectorService = subSectorService;
            _httpContextAccessor = httpContextAccessor;
            _htmlSanitizer = htmlSanitizer;
        }

        public List<SelectListItem> SectorSelectSystem(int? sectorId, string tip)
        {
            try
            {
                var result = new List<SelectListItem>();

                switch (tip)
                {
                    case "getSectors":
                        var sectors = _sectorService.GetAllIncludingForAddCompanyAsync();
                        result = sectors.Select(sector => new SelectListItem
                        {
                            Text = sector.Name,
                            Value = sector.Id.ToString()
                        }).ToList();
                        break;

                    case "getSubSectors":
                        if (sectorId == null)
                        {
                            throw new ArgumentNullException(nameof(sectorId), "Sector ID can not be empty.");
                        }

                        var subSectors = _subSectorService.GetAllIncludingForAddCompanyBySectorIdAsync(sectorId.Value);
                        result = subSectors.Select(subsector => new SelectListItem
                        {
                            Text = subsector.Name,
                            Value = subsector.Id.ToString()
                        }).ToList();
                        break;

                    default:
                        throw new ArgumentException($"Unsupported type: {tip}");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error", ex);
            }
        }

        public async Task<bool> CreateAsync(string name, string slogan, string shortBio, string desc, DateTime foundationDate, bool isLookingForInvest, string linkedIn, string? gitHub, int companyCategoryId, int countryId, int sectorId, int? subSectorId, string appUserId, IFormFile image)
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
                        string savedFileName = await ServiceImageHelper.CompanyImageResize(image);

                        ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                        string safeShortBio = _htmlSanitizer.Sanitize(shortBio ?? string.Empty);
                        string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                        var entity = new Company
                        {
                            Name = name,
                            Slogan = slogan,
                            ShortBio = safeShortBio,
                            Desc = safeDesc,
                            FoundationDate = foundationDate,
                            IsLookingForInvest = isLookingForInvest,
                            LinkedIn = linkedIn,
                            GitHub = gitHub,
                            CompanyCategoryId = companyCategoryId,
                            CountryId = countryId,
                            SectorId = sectorId,
                            SubSectorId = subSectorId,
                            AppUserId = appUserId,
                            LogoUrl = savedFileName
                        };

                        var results = await _companyRepository.AddAsync(entity);
                        if (!results)
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
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Company entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _companyRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Company> GetAllForSitemap()
        {
            try
            {
                return _companyRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingByCompanyCategoryIdAsync(int companyCategoryId)
        {
            try
            {
                var data = _companyRepository.GetAllIncludeById(companyCategoryId, "CompanyCategoryId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingByCountryIdAsync(int countryId)
        {
            try
            {
                var data = _companyRepository.GetAllIncludeById(countryId, "CountryId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingByFoundationDateAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.FoundationDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingByLookingForInvestAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsLookingForInvest==true
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingBySectorIdAsync(int sectorId)
        {
            try
            {
                var data = _companyRepository.GetAllIncludeById(sectorId, "SectorId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingBySubSectorIdAsync(int? subSectorId)
        {
            try
            {
                if (subSectorId == null)
                    throw new ArgumentNullException(nameof(subSectorId), "subSectorId was null");

                var data = _companyRepository.GetAllIncludeById(subSectorId, "SubSectorId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _companyRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {

                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }
        public async Task<Company?> GetBySlugAsync(string slug)
        {
            var match = await _companyRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetByIdAsync(match.Id);
        }
        public async Task<Company> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _companyRepository.GetIncludeAsync(i => i.Id == id, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetFollowableAsync(int id)
        {
            var result = await _companyRepository.SetFollowableAsync(id);
            return result;
        }

        public async Task<bool> SetNotFollowableAsync(int id)
        {
            var result = await _companyRepository.SetNotFollowableAsync(id);
            return result;
        }
        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetLookingForInvestAsync(int id)
        {
            var result = await _companyRepository.SetLookingForInvestAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotLookingForInvestAsync(int id)
        {
            var result = await _companyRepository.SetNotLookingForInvestAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string name, string slogan, string shortBio, string desc, DateTime foundationDate, bool isLookingForInvest, string linkedIn, string? gitHub, int companyCategoryId, int countryId, int sectorId, int? subSectorId, string appUserId, IFormFile image, int id)
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
                        string savedFileName = await ServiceImageHelper.CompanyImageResize(image);

                        ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                        string safeShortBio = _htmlSanitizer.Sanitize(shortBio ?? string.Empty);
                        string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                        var entity = new Company
                        {
                            Name = name,
                            Slogan = slogan,
                            ShortBio = safeShortBio,
                            Desc = safeDesc,
                            FoundationDate = foundationDate,
                            IsLookingForInvest = isLookingForInvest,
                            LinkedIn = linkedIn,
                            GitHub = gitHub,
                            CompanyCategoryId = companyCategoryId,
                            CountryId = countryId,
                            SectorId = sectorId,
                            SubSectorId = subSectorId,
                            AppUserId = appUserId,
                            LogoUrl = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };

                        var results = await _companyRepository.UpdateAsync(entity);
                        if (!results)
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

        public int CompanyCounter()
        {
            return _companyRepository.CompanyCounter();
        }

        public IEnumerable<Company> GetAllIncludeLastJoinedCompaniesForAdminHome()
        {
            try
            {
                return _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyCategory, y => y.Sector, y => y.Country, y => y.AppUser).OrderByDescending(i => i.CreatedDate).Take(20).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public IEnumerable<Company> GetAllIncludingLastCompanies()
        {
            try
            {
                return _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.AppUser.IsCompany==true
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector).OrderByDescending(i => i.CreatedDate).Take(15).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public IEnumerable<Company> GetAllIncludingMostPopularCompanies()
        {
            return _companyRepository.GetAllIncludingMostPopularCompanies();
        }

        public async Task<IEnumerable<Company>> GetAllIncludingMostPopularCompaniesAsync()
        {
            return await _companyRepository.GetAllIncludingMostPopularCompaniesAsync();
        }

        public IEnumerable<Company> GetAllIncludingRandomCompaniesForCompanyDetail()
        {
            try
            {
                return _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, null, y => y.CompanyCategory, y => y.Country, y => y.Sector, y => y.SubSector).OrderByDescending(i => Guid.NewGuid()).Take(4).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public Company GetCompanyForCommentFormByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _companyRepository.Get(i => i.Id == companyId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Company GetCompanyIdForCompanyHeader(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _companyRepository.GetInclude(i => i.AppUserId == userId, y => y.Hits, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public IQueryable<Company> GetAllIncludingCompeniesForCompanyHomeByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _companyRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public Company GetCompanyLogoByCompanyUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _companyRepository.Get(i => i.AppUserId == userId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingCompaniesForPublicUser()
        {
            try
            {
                var data = await _companyRepository.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyCategory, y => y.Country, y => y.Sector, y => y.SubSector, y => y.Posts, y => y.Blogs);
                return data.Take(140).OrderByDescending(i => i.CreatedDate).OrderBy(i => Guid.NewGuid()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public IEnumerable<Company> GetAllIncludingCompanyForPublicUser()
        {
            try
            {
                return _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                 {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                 }, null, y => y.Country, y => y.CompanyCategory).Take(100).OrderByDescending(i => i.CreatedDate).OrderBy(i => Guid.NewGuid()).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }

        public async Task<IEnumerable<Company>> GetAllIncludingUnPopularCompaniesAsync()
        {
            var data = await _companyRepository.GetAllIncludingUnPopularCompaniesAsync();
            return data;
        }

        public IQueryable<Company> GetAllIncludingMostLikedCompaniesAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Hits, y => y.Likes, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderByDescending(i => i.Likes.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingMostSavedCompaniesAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SavedContents.Count()>0
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Hits, y => y.Likes, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderByDescending(i => i.SavedContents.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingMostHitCompaniesAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Hits, y => y.Likes, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderByDescending(i => i.Hits.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingLessLikedCompaniesAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>=0
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Hits, y => y.Likes, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderBy(i => i.Likes.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingLessSavedCompaniesAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SavedContents.Count()>=0
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Hits, y => y.Likes, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderBy(i => i.SavedContents.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingLessHitCompaniesAsync()
        {
            try
            {
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>=0
                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Hits, y => y.Likes, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderBy(i => i.Hits.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingCompanyTodayAsync()
        {
            try
            {
                var today = DateTime.Now.Date;
                var data = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                   i => i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.CompanyCategory, y => y.Country, y => y.Sector, y => y.SubSector, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.SavedContents, y => y.Surveys, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public IQueryable<Company> GetAllIncludingCompanyFinderSearchResults(string? companyName = null, string? foundationYear = null, bool isLookingForInvest = true, string? hasGithubAccount = null, int? countryId = null, int? companyCategoryId = null, int? sectorId = null)
        {
            try
            {
                var allCompanies = _companyRepository.GetAllInclude(new Expression<Func<Company, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyCategory, y => y.Country, y => y.Sector, y => y.SubSector, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.SavedContents, y => y.Surveys, y => y.Blogs);

                var filtered = allCompanies.AsQueryable();

                if (!string.IsNullOrWhiteSpace(companyName))
                {
                    string trimmed = companyName.Trim();
                    filtered = filtered.Where(c => c.Name.Contains(trimmed, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(foundationYear) && int.TryParse(foundationYear.Trim(), out int year))
                {
                    filtered = filtered.Where(c =>
                        c.FoundationDate != null &&
                        c.FoundationDate.Year == year);
                }

                if (isLookingForInvest)
                {
                    filtered = filtered.Where(c => c.IsLookingForInvest == isLookingForInvest);
                }

                if (!string.IsNullOrWhiteSpace(hasGithubAccount))
                {
                    bool wantsGithub = hasGithubAccount.Trim().Equals("true", StringComparison.OrdinalIgnoreCase);

                    filtered = filtered.Where(c =>
                        wantsGithub
                            ? !string.IsNullOrWhiteSpace(c.GitHub)
                            : string.IsNullOrWhiteSpace(c.GitHub));
                }

                if (countryId.HasValue && countryId.Value > 0)
                {
                    filtered = filtered.Where(c => c.CountryId == countryId.Value);
                }

                if (companyCategoryId.HasValue && companyCategoryId.Value > 0)
                {
                    filtered = filtered.Where(c => c.CompanyCategoryId == companyCategoryId.Value);
                }

                if (sectorId.HasValue && sectorId.Value > 0)
                {
                    filtered = filtered.Where(c => c.SectorId == sectorId.Value);
                }
                return filtered.OrderByDescending(c => c.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Company>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Company>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _companyRepository.GetAllIncludeAsync(new Expression<Func<Company, bool>>[]
                {

                }, null, y => y.CompanyCategory, y => y.Country, y => y.AppUser, y => y.Sector, y => y.SubSector, y => y.Announcements, y => y.CompanyContacts, y => y.CompanyFinances, y => y.CompanyPinteches, y => y.CompanyStages, y => y.CompanyTeams, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs, y => y.CompanyFollowers, y => y.CompanyFollowings);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Company>();
            }
        }
    }
}
