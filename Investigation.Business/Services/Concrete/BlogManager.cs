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
    public class BlogManager : IBlogService
    {
        readonly IBlogRepository _blogRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public BlogManager(IBlogRepository blogRepository, IHttpContextAccessor httpContextAccessor, IHtmlSanitizer htmlSanitizer)
        {
            _blogRepository = blogRepository;
            _httpContextAccessor = httpContextAccessor;
            _htmlSanitizer = htmlSanitizer;
        }

        public int BlogCounter()
        {
            return _blogRepository.BlogCounter();
        }

        public async Task<bool> CreateCompanyBlogAsync(string title, string subtitle, string? detail, string content, int blogCategoryId, int? companyId, string appUserId, IFormFile image)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeDetail = _htmlSanitizer.Sanitize(detail ?? string.Empty);
                string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.BlogImageResize(image);
                        var entity = new Blog
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Detail = safeDetail,
                            Content = safeContent,
                            BlogCategoryId = blogCategoryId,
                            CompanyId = companyId,
                            AppUserId = appUserId,
                            CoverImage = savedFileName
                        };

                        var results = await _blogRepository.AddAsync(entity);
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

        public async Task<bool> CreateInvestorBlogAsync(string title, string subtitle, string? detail, string content, int blogCategoryId, int? investorId, string appUserId, IFormFile image)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }
                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeDetail = _htmlSanitizer.Sanitize(detail ?? string.Empty);
                string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);

                if (image != null && image.Length > 0)
                {

                    ServiceImageHelper.ImageValidation(image);

                    try
                    {
                        string savedFileName = await ServiceImageHelper.BlogImageResize(image);

                        var entity = new Blog
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Detail = safeDetail,
                            Content = safeContent,
                            BlogCategoryId = blogCategoryId,
                            InvestorId = investorId,
                            AppUserId = appUserId,
                            CoverImage = savedFileName
                        };
                        var results = await _blogRepository.AddAsync(entity);
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

        public async Task<bool> DeleteAsync(Blog entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _blogRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _blogRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Blog> GetAllForSitemap()
        {
            try
            {
                return _blogRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingAsync()
        {
            try
            {
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingBlogByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _blogRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SavedContents).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingBlogForCompanyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _blogRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingBlogForInvestorByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data =  _blogRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Investor.IsActive==true&&i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingBlogForInvestorDetail(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                //var today = DateTime.Today;
                //var twoWeeksAgo = today.AddDays(-14);

                return _blogRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                    //i => i.CreatedDate >= twoWeeksAgo && i.CreatedDate < today.AddDays(1)
                }, y => y.BlogCategory, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SavedContents).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingBlogsForPublicUser()
        {
            try
            {
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Likes, y => y.Comments);
                return data.Take(140).OrderByDescending(i => i.CreatedDate).OrderBy(i => Guid.NewGuid());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingBlogTodayAsync()
        {
            try
            {
                var today = DateTime.Now.Date;
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i => i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingByBlogCategoryIdAsync(int blogCategoryId)
        {
            try
            {
                var data =  _blogRepository.GetAllIncludeById(blogCategoryId, "BlogCategoryId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data =  _blogRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data =  _blogRepository.GetAllIncludeById(investorId, "Investor", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingByMostLikedBlogAsync()
        {
            try
            {
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.Likes.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data =  _blogRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {

                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingLastBlogForIndex()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SavedContents).OrderByDescending(i => Guid.NewGuid()).Take(45);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingLastBlogForTimeline()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SavedContents).OrderByDescending(i => Guid.NewGuid()).Take(35);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingMostByHitBlogAsync()
        {
            try
            {
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.Hits.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingMostHitBlogsAsync()
        {
            try
            {
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return data.OrderByDescending(i => i.Hits.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingMostLikedBlogsAsync()
        {
            try
            {
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return data.OrderByDescending(i => i.Likes.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingMostSavedBlogsAsync()
        {
            try
            {
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SavedContents.Count()>0
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return data.OrderByDescending(i => i.SavedContents.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingPopularBlog()
        {
            try
            {
                return _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    (i=>i.Hits.Count()>25&&i.Likes.Count()>45&&i.Reports.Count()<20&&i.Comments.Count()>40&&i.SavedContents.Count()>10)
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SavedContents).OrderByDescending(i => i.Likes.Count()).Take(8);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public IQueryable<Blog> GetAllIncludingyMostSavedBlogAsync()
        {
            try
            {
                var data =  _blogRepository.GetAllInclude(new Expression<Func<Blog, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.SavedContents.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Blog>().AsQueryable();
            }
        }

        public Blog GetBlogForFormById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return _blogRepository.Get(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }
        public async Task<Blog?> GetBySlugAsync(string slug)
        {
            var match = await _blogRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetByIdAsync(match.Id);
        }
        public async Task<Blog?> GetBySlugForPublicBlogDetailAsync(string slug)
        {
            var match = await _blogRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetPublicBlogByIdAsync(match.Id);
        }
        public async Task<Blog> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _blogRepository.GetIncludeAsync(i => i.Id == id, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<Blog> GetPublicBlogByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _blogRepository.GetIncludeAsync(i => i.Id == id, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.SavedContents);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _blogRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _blogRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _blogRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _blogRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateCompanyBlogAsync(string title, string subtitle, string? detail, string content, int blogCategoryId, int? companyId, string appUserId, IFormFile image, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeDetail = _htmlSanitizer.Sanitize(detail ?? string.Empty);
                string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.BlogImageResize(image);

                        var entity = new Blog
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Detail = safeDetail,
                            Content = safeContent,
                            BlogCategoryId = blogCategoryId,
                            CompanyId = companyId,
                            AppUserId = appUserId,
                            CoverImage = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow,
                        };

                        var results = await _blogRepository.UpdateAsync(entity);
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

        public async Task<bool> UpdateInvestorBlogAsync(string title, string subtitle, string? detail, string content, int blogCategoryId, int? investorId, string appUserId, IFormFile image, int id)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeDetail = _htmlSanitizer.Sanitize(detail ?? string.Empty);
                string safeContent = _htmlSanitizer.Sanitize(content ?? string.Empty);

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.BlogImageResize(image);

                        var entity = new Blog
                        {
                            Title = title,
                            Subtitle = subtitle,
                            Detail = safeDetail,
                            Content = safeContent,
                            BlogCategoryId = blogCategoryId,
                            InvestorId = investorId,
                            AppUserId = appUserId,
                            CoverImage = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow,
                        };

                        var results = await _blogRepository.UpdateAsync(entity);
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

        public async Task<IEnumerable<Blog>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _blogRepository.GetAllIncludeAsync(new Expression<Func<Blog, bool>>[]
                {

                }, null, y => y.BlogCategory, y => y.Company, y => y.Investor, y => y.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Blog>();
            }
        }
    }
}
