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
    public class PostManager : IPostService
    {
        readonly IPostRepository _postRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public PostManager(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor, IHtmlSanitizer htmlSanitizer)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<bool> CreateCompanyPostAsync(string text, bool isCommentable, int? companyId, string appUserId, IFormFile? image)
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

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.PostImageResize(image);

                        string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                        var entity = new Post
                        {
                            Text = safeText,
                            IsCommentable = isCommentable,
                            CompanyId = companyId,
                            AppUserId = appUserId,
                            ImageUrl = savedFileName
                        };

                        var results = await _postRepository.AddAsync(entity);
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
                else
                {
                    string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                    var entity = new Post
                    {
                        Text = safeText,
                        IsCommentable = isCommentable,
                        CompanyId = companyId,
                        AppUserId = appUserId
                    };
                    var result = await _postRepository.AddAsync(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateInvestorPostAsync(string text, bool isCommentable, int? investorId, string appUserId, IFormFile? image)
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

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.PostImageResize(image);

                        string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                        var entity = new Post
                        {
                            Text = safeText,
                            IsCommentable = isCommentable,
                            InvestorId = investorId,
                            AppUserId = appUserId,
                            ImageUrl = savedFileName
                        };

                        var results = await _postRepository.AddAsync(entity);
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
                else
                {
                    ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                    string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                    var entity = new Post
                    {
                        Text = safeText,
                        IsCommentable = isCommentable,
                        InvestorId = investorId,
                        AppUserId = appUserId
                    };
                    var result = await _postRepository.AddAsync(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _postRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Post entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _postRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _postRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Post> GetAllIncludingAsync()
        {
            try
            {
                var data = _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _postRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _postRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _postRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingCommentablesAsync()
        {
            try
            {
                var data = _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCommentable==true
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {

                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingLastPostForIndex()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.AppUser, y => y.Investor, y => y.Investor.AppUser, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SavedContents).OrderByDescending(i => Guid.NewGuid()).Take(65);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingLastPostForTimeline()
        {
            try
            {
                //var today = DateTime.Today;
                //var tomorrow = today.AddDays(1);

                return _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                    //i => i.CreatedDate >= today && i.CreatedDate < tomorrow
                }, null, y => y.AppUser, y => y.Investor, y => y.Investor.AppUser, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SavedContents).OrderByDescending(i => Guid.NewGuid()).Take(50);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingMostHitPostsAsync()
        {
            try
            {
                var data = _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hits.Count()>0
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return data.OrderByDescending(i => i.Hits.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingMostLikedPostsAsync()
        {
            try
            {
                var data = _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Likes.Count()>0
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return data.OrderByDescending(i => i.Likes.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingMostSavedPostsAsync()
        {
            try
            {
                var data = _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SavedContents.Count()>0
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return data.OrderByDescending(i => i.SavedContents.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingNotCommentablesAsync()
        {
            try
            {
                var data = _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCommentable==false
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingPopularPosts()
        {
            try
            {
                return _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    (i=>i.Hits.Count()>25&&i.Likes.Count()>45&&i.Reports.Count()<20&&i.SavedContents.Count()>10&&i.Comments.Count()>40)
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Reports, y => y.SavedContents).OrderByDescending(i => i.Likes.Count()).Take(8);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingPostForCompanyByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _postRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingPostForCompanyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _postRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingPostForCompanyDetail(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _postRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SavedContents).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingPostForInvestorByInvestorId(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                return _postRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingPostForInvestorByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _postRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingPostForInvestorDetail(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                //var today = DateTime.Today;
                //var twoWeeksAgo = today.AddDays(-14);

                return _postRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                    //i => i.CreatedDate >= twoWeeksAgo && i.CreatedDate < today.AddDays(1)
                }, y => y.Comments, y => y.Hits, y => y.Likes, y => y.SavedContents).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }

        public IQueryable<Post> GetAllIncludingPostTodayAsync()
        {
            try
            {
                var today = DateTime.Now.Date;
                var data = _postRepository.GetAllInclude(new Expression<Func<Post, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i => i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Pictures, y => y.Hits, y => y.Likes, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Post>().AsQueryable();
            }
        }
        public async Task<Post?> GetBySlugAsync(string slug)
        {
            var match = await _postRepository.GetBySlugAsync(slug);
            if (match == null)
            {
                return null;
            }
            return await GetByIdAsync(match.Id);
        }
        public async Task<Post> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _postRepository.GetIncludeAsync(i => i.Id == id, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Post GetPostForFormById(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return _postRepository.Get(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public int PostCounter()
        {
            return _postRepository.PostCounter();
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _postRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetCommentablePostAsync(int id)
        {
            var result = await _postRepository.SetCommentablePostAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _postRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _postRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotCommentablePostAsync(int id)
        {
            var result = await _postRepository.SetNotCommentablePostAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _postRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateCompanyPostAsync(string text, bool isCommentable, int? companyId, string appUserId, IFormFile? image, int id)
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

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.PostImageResize(image);

                        string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                        var entity = new Post
                        {
                            Text = safeText,
                            IsCommentable = isCommentable,
                            CompanyId = companyId,
                            AppUserId = appUserId,
                            ImageUrl = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };
                        var results = await _postRepository.UpdateAsync(entity);
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
                else
                {
                    string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                    var entity = new Post
                    {
                        Text = safeText,
                        IsCommentable = isCommentable,
                        CompanyId = companyId,
                        AppUserId = appUserId,
                        Id = id,
                        UpdatedDate = DateTime.UtcNow
                    };
                    var result = await _postRepository.UpdateAsync(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<bool> UpdateInvestorPostAsync(string text, bool isCommentable, int? investorId, string appUserId, IFormFile? image, int id)
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

                if (image != null && image.Length > 0)
                {
                    ServiceImageHelper.ImageValidation(image);
                    try
                    {
                        string savedFileName = await ServiceImageHelper.PostImageResize(image);

                        string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                        var entity = new Post
                        {
                            Text = safeText,
                            IsCommentable = isCommentable,
                            InvestorId = investorId,
                            AppUserId = appUserId,
                            ImageUrl = savedFileName,
                            Id = id,
                            UpdatedDate = DateTime.UtcNow
                        };

                        var results = await _postRepository.UpdateAsync(entity);
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
                else
                {
                    string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                    var entity = new Post
                    {
                        Text = safeText,
                        IsCommentable = isCommentable,
                        InvestorId = investorId,
                        AppUserId = appUserId,
                        Id = id,
                        UpdatedDate = DateTime.UtcNow
                    };
                    var result = await _postRepository.UpdateAsync(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<IEnumerable<Post>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _postRepository.GetAllIncludeAsync(new Expression<Func<Post, bool>>[]
                {

                }, null, y => y.AppUser, y => y.Company, y => y.Investor, y => y.Comments, y => y.Hits, y => y.Likes, y => y.Pictures, y => y.Reports, y => y.SavedContents);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Post>();
            }
        }
    }
}
