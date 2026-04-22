using System.Linq.Expressions;
using System.Security.Claims;
using Ganss.Xss;
using Investigation.Business.Constants.Helpers;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class InvestorManager : IInvestorService
    {
        readonly IInvestorRepository _investorRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public InvestorManager(IInvestorRepository investorRepository, IHttpContextAccessor httpContextAccessor, IHtmlSanitizer htmlSanitizer)
        {
            _investorRepository = investorRepository;
            _httpContextAccessor = httpContextAccessor;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<bool> CreateAsync(string bio, string investArea, DateTime sinceWhen, bool isLookingForCompany, string? emailAddress, string? phoneNumber, int investorCategoryId, int countryId, string appUserId, IFormFile image)
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
                        string savedFileName = await ServiceImageHelper.InvestorImageResize(image);

                        ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                        string safeInvestArea = _htmlSanitizer.Sanitize(investArea ?? string.Empty);
                        string safeBio = _htmlSanitizer.Sanitize(bio ?? string.Empty);
                        var entity = new Investor
                        {
                            Bio = safeBio,
                            InvestArea = safeInvestArea,
                            SinceWhen = sinceWhen,
                            IsLookingForCompany = isLookingForCompany,
                            EmailAddress = emailAddress,
                            PhoneNumber = phoneNumber,
                            InvestorCategoryId = investorCategoryId,
                            CountryId = countryId,
                            AppUserId = appUserId,
                            CoverImageUrl = savedFileName
                        };

                        var results = await _investorRepository.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(Investor entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _investorRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _investorRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Investor> GetAllForSitemap()
        {
            try
            {
                return _investorRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingByCountryIdAsync(int countryId)
        {
            try
            {
                var data =  _investorRepository.GetAllIncludeById(countryId, "CountryId", new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingByInvesterCategoryIdAsync(int investorCategoryId)
        {
            try
            {
                var data =  _investorRepository.GetAllIncludeById(investorCategoryId, "InvestorCategoryId", new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingByInvestorDateAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.SinceWhen);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingByLookingForCompanyAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsLookingForCompany==true
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingByMostHitAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.Hits.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingByMostLikedAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias);
                return data.OrderByDescending(i => i.Likes.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingByMostInvestedAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.RecentlyInvests.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data =  _investorRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {

                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }
        public async Task<Investor?> GetBySlugAsync(string slug)
        {
            var match = await _investorRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetByIdAsync(match.Id);
        }
        public async Task<Investor> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _investorRepository.GetIncludeAsync(i => i.Id == id, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetInvestorLookingForCompanyAsync(int id)
        {
            var result = await _investorRepository.SetInvestorLookingForCompanyAsync(id);
            return result;
        }

        public async Task<bool> SetInvestorNotLookingForCompanyAsync(int id)
        {
            var result = await _investorRepository.SetInvestorNotLookingForCompanyAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _investorRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _investorRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _investorRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _investorRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string bio, string investArea, DateTime sinceWhen, bool isLookingForCompany, string? emailAddress, string? phoneNumber, int investorCategoryId, int countryId, string appUserId, IFormFile image, int id)
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
                        string savedFileName = await ServiceImageHelper.InvestorImageResize(image);

                        ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                        string safeInvestArea = _htmlSanitizer.Sanitize(investArea ?? string.Empty);
                        string safeBio = _htmlSanitizer.Sanitize(bio ?? string.Empty);
                        var entity = new Investor
                        {
                            Bio = safeBio,
                            InvestArea = safeInvestArea,
                            SinceWhen = sinceWhen,
                            IsLookingForCompany = isLookingForCompany,
                            EmailAddress = emailAddress,
                            PhoneNumber = phoneNumber,
                            InvestorCategoryId = investorCategoryId,
                            CountryId = countryId,
                            AppUserId = appUserId,
                            CoverImageUrl = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };

                        var results = await _investorRepository.UpdateAsync(entity);
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

        public int InvestorCounter()
        {
            return _investorRepository.InvestorCounter();
        }

        public IEnumerable<Investor> GetAllIncludeLastJoinedInvestorForAdmin()
        {
            try
            {
                return _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.InvestorCategory, y => y.AppUser, y => y.Country).OrderByDescending(i => i.CreatedDate).Take(20).ToList();
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }

        public Investor GetInvestorIdForInvestorHeader(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _investorRepository.GetInclude(i => i.AppUserId == userId, y => y.Hits, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<Investor> GetInvestorForProfileByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return await _investorRepository.GetIncludeAsync(i => i.AppUserId == userId, y => y.AppUser, y => y.Country, y => y.InvestorCategory, y => y.Announcements, y => y.Blogs, y => y.RecentlyInvests, y => y.Posts, y => y.Surveys, y => y.UserSocialMedias, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SavedContents, y => y.AppUser.Blogs, y => y.AppUser.Comments, y => y.AppUser.CommentAnswers, y => y.AppUser.Hits, y => y.AppUser.Likes, y => y.AppUser.Posts, y => y.AppUser.Reports, y => y.AppUser.SavedContents, y => y.AppUser.UserProfileImages, y => y.AppUser.Surveys, y => y.AppUser.SurveyAnswers, y => y.AppUser.SurveyResponses);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public IEnumerable<Investor> GetAllIncludingLastInvestor()
        {
            try
            {
                return _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                 {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.AppUser.IsInvestor==true,
                    i=>i.AppUser.Investors.Count()>0
                 }, null, y => y.InvestorCategory, y => y.AppUser, y => y.AppUser.Investors, y => y.Country).OrderByDescending(i => i.CreatedDate).Take(15).ToList();
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }

        public IEnumerable<Investor> GetAllIncludingMostPopularInvestors()
        {
            return _investorRepository.GetAllIncludingMostPopularInvestors();
        }

        public async Task<IEnumerable<Investor>> GetAllIncludingMostPopularInvestorsAsync()
        {
            return await _investorRepository.GetAllIncludingMostPopularInvestorsAsync();
        }

        public IEnumerable<Investor> GetAllIncludingInvestorsRandomForInvestorDetail()
        {
            try
            {
                return _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                 {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.AppUser.IsInvestor==true
                 }, null, y => y.InvestorCategory, y => y.Country, y => y.AppUser).OrderByDescending(i => Guid.NewGuid()).Take(4).ToList();
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }

        public async Task<IEnumerable<Investor>> GetAllIncludingInvestorsForPublicUser()
        {
            try
            {
                var data = await _investorRepository.GetAllIncludeAsync(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Country, y => y.InvestorCategory, y => y.Posts, y => y.Blogs, y => y.AppUser);
                return data.Take(140).OrderByDescending(i => i.CreatedDate).OrderBy(i => Guid.NewGuid()).ToList();
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }

        public IEnumerable<Investor> GetAllIncludingInvestorForPublicUser()
        {
            try
            {
                return _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                 {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                 }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser).Take(140).OrderByDescending(i => i.CreatedDate).OrderBy(i => Guid.NewGuid()).ToList();
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }

        public async Task<IEnumerable<Investor>> GetAllIncludingUnPopularInvestorsAsync()
        {
            var data = await _investorRepository.GetAllIncludingUnPopularInvestorsAsync();
            return data;
        }

        public IQueryable<Investor> GetAllIncludingMostLikedInvestorsAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderByDescending(i => i.Likes.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingMostSavedInvestorsAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SavedContents.Count()>0
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderByDescending(i => i.SavedContents.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingMostHitInvestorsAsync()
        {
            try
            {
                var data =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderByDescending(i => i.Hits.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingLessLikedInvestorsAsync()
        {
            try
            {
                var data = _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>=0
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderBy(i => i.Likes.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingLessSavedInvestorsAsync()
        {
            try
            {
                var data = _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SavedContents.Count()>=0
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderBy(i => i.SavedContents.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public IQueryable<Investor> GetAllIncludingLessHitInvestorsAsync()
        {
            try
            {
                var data = _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>=0
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.SavedContents, y => y.Blogs);
                return data.OrderBy(i => i.Hits.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Investor>> GetAllIncludingInvestorTodayAsync()
        {
            try
            {
                var today = DateTime.Now.Date;
                var data = await _investorRepository.GetAllIncludeAsync(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i => i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.SavedContents, y => y.Surveys, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }

        public IQueryable<Investor> GetAllIncludingInvestorSearchResult(string investArea, string? sinceWhen, bool isLookingForCompany, int? countryId, int? investorCategoryId)
        {
            try
            {
                var allInvestors =  _investorRepository.GetAllInclude(new Expression<Func<Investor, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.SavedContents);

                var filtered = allInvestors.AsQueryable();

                if (!string.IsNullOrWhiteSpace(investArea))
                {
                    string trimmed = investArea.Trim();
                    filtered = filtered.Where(c => c.InvestArea.Contains(trimmed, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(sinceWhen) && int.TryParse(sinceWhen.Trim(), out int year))
                {
                    filtered = filtered.Where(c =>
                        c.SinceWhen != null &&
                        c.SinceWhen.Year == year);
                }

                if (isLookingForCompany)
                {
                    filtered = filtered.Where(c => c.IsLookingForCompany == isLookingForCompany);
                }

                if (countryId.HasValue && countryId.Value > 0)
                {
                    filtered = filtered.Where(c => c.CountryId == countryId.Value);
                }

                if (investorCategoryId.HasValue && investorCategoryId.Value > 0)
                {
                    filtered = filtered.Where(c => c.InvestorCategoryId == investorCategoryId.Value);
                }
                return filtered.OrderByDescending(c => c.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Investor>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Investor>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _investorRepository.GetAllIncludeAsync(new Expression<Func<Investor, bool>>[]
                {

                }, null, y => y.Country, y => y.InvestorCategory, y => y.AppUser, y => y.Announcements, y => y.Hits, y => y.Likes, y => y.RecentlyInvests, y => y.Posts, y => y.Reports, y => y.SavedContents, y => y.Surveys, y => y.UserSocialMedias, y => y.Blogs);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Investor>();
            }
        }
    }
}
