using System.Linq.Expressions;
using System.Security.Claims;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class LikeManager : ILikeService
    {
        readonly ILikeRepository _likeRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        public LikeManager(ILikeRepository likeRepository, IHttpContextAccessor httpContextAccessor)
        {
            _likeRepository = likeRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> BlogDisLikeAsync(int? blogId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.BlogId == blogId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        BlogId = blogId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue - 1,
                        IsLiked = false
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> BlogLikeAsync(int? blogId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.BlogId == blogId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        BlogId = blogId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue + 1,
                        IsLiked = true
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> CommentAnswerDisLikeAsync(int? commentAnswerId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (commentAnswerId == null)
                    throw new ArgumentNullException(nameof(commentAnswerId), "commentAnswerId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.CommentAnswerId == commentAnswerId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        CommentAnswerId = commentAnswerId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue - 1,
                        IsLiked = false
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> CommentAnswerLikeAsync(int? commentAnswerId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (commentAnswerId == null)
                    throw new ArgumentNullException(nameof(commentAnswerId), "commentAnswerId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.CommentAnswerId == commentAnswerId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        CommentAnswerId = commentAnswerId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue + 1,
                        IsLiked = true
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> CommentDisLikeAsync(int? commentId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.CommentId == commentId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        CommentId = commentId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue - 1,
                        IsLiked = false
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> CommentLikeAsync(int? commentId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.CommentId == commentId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        CommentId = commentId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue + 1,
                        IsLiked = true
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> CompanyDisLikeAsync(int? companyId, string appUserId, int currentValue, bool isLiked)
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

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.CompanyId == companyId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        CompanyId = companyId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue - 1,
                        IsLiked = false
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> CompanyLikeAsync(int? companyId, string appUserId, int currentValue, bool isLiked)
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

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.CompanyId == companyId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        CompanyId = companyId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue + 1,
                        IsLiked = true
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> InvestorDisLikeAsync(int? investorId, string appUserId, int currentValue, bool isLiked)
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

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.InvestorId == investorId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        InvestorId = investorId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue - 1,
                        IsLiked = false
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> InvestorLikeAsync(int? investorId, string appUserId, int currentValue, bool isLiked)
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

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.InvestorId == investorId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        InvestorId = investorId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue + 1,
                        IsLiked = true
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> PostDisLikeAsync(int? postId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.PostId == postId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        PostId = postId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue - 1,
                        IsLiked = false
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> PostLikeAsync(int? postId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.PostId == postId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        PostId = postId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue + 1,
                        IsLiked = true
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> SurveyDisLikeAsync(int? surveyId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                           ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.SurveyId == surveyId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        SurveyId = surveyId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue - 1,
                        IsLiked = false
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public async Task<bool> SurveyLikeAsync(int? surveyId, string appUserId, int currentValue, bool isLiked)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingLike = await _likeRepository.GetAsync(d => d.AppUserId == appUserId && d.SurveyId == surveyId);
                if (existingLike != null)
                {
                    return false;
                }
                else
                {
                    var newLike = new Like
                    {
                        SurveyId = surveyId,
                        AppUserId = appUserId,
                        CurrentValue = currentValue + 1,
                        IsLiked = true
                    };
                    await _likeRepository.AddAsync(newLike);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while likes the entity.", ex);
            }
        }

        public IQueryable<Like> GetAllIncludingAsync()
        {
            try
            {
                var data = _likeRepository.GetAllInclude(new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByLessDisLikedValueAsync()
        {
            try
            {
                var data = _likeRepository.GetAllInclude(new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsLiked==false
                }, null, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderBy(i => i.CurrentValue);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByLessLikedValueAsync()
        {
            try
            {
                var data = _likeRepository.GetAllInclude(new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsLiked==true,
                    i=>i.CurrentValue>0
                }, null, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderBy(i => i.CurrentValue);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByMostDisLikedValueAsync()
        {
            try
            {
                var data = _likeRepository.GetAllInclude(new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsLiked==false
                }, null, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CurrentValue);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByMostLikedValueAsync()
        {
            try
            {
                var data = _likeRepository.GetAllInclude(new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsLiked==true
                }, null, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CurrentValue);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _likeRepository.GetAllInclude(new Expression<Func<Like, bool>>[]
                {

                }, null, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var data = _likeRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByCommentAnswerIdAsync(int? commentAnswerId)
        {
            try
            {
                if (commentAnswerId == null)
                    throw new ArgumentNullException(nameof(commentAnswerId), "commentAnswerId was null");

                var data = _likeRepository.GetAllIncludeById(commentAnswerId, "CommentAnswerId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByCommentIdAsync(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var data = _likeRepository.GetAllIncludeById(commentId, "CommentId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _likeRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _likeRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByPostIdAsync(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var data = _likeRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingBySurveyIdAsync(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var data = _likeRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _likeRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public async Task<Like> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _likeRepository.GetIncludeAsync(i => i.Id == id, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _likeRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Like entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _likeRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _likeRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _likeRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _likeRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _likeRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _likeRepository.SetNotDeletedAsync(id);
            return result;
        }

        public int LikeCounter()
        {
            return _likeRepository.LikeCounter();
        }

        public IQueryable<Like> GetAllIncludingLikesForUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _likeRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<Like, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsLiked==true
                }, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingLikesForLikeOwnerByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var likes = _likeRepository.GetAllInclude(new Expression<Func<Like, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => (i.BlogId != null && i.Blog.AppUserId == userId)
                    || (i.CompanyId != null && i.Company.AppUserId == userId)
                    ||(i.CommentId!= null && i.Comment.AppUserId== userId)
                    ||(i.CommentAnswerId!= null && i.CommentAnswer.AppUserId== userId)
                    || (i.InvestorId != null && i.Investor.AppUserId == userId)
                    || (i.Post != null && i.Post.AppUserId == userId)
                    || (i.SurveyId != null && i.Survey.AppUserId == userId)
                }, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswer, y => y.Comment, y => y.Investor, y => y.Investor.AppUser, y => y.Survey);

                if (likes == null || !likes.Any())
                    return Enumerable.Empty<Like>().AsQueryable();

                var uniqueComments = likes.AsEnumerable().GroupBy(c => new { c.BlogId, c.CompanyId, c.InvestorId, c.PostId, c.SurveyId, c.CommentId, c.CommentAnswerId }).Select(g => g.OrderByDescending(c => c.CreatedDate).First()).OrderByDescending(c => c.CreatedDate);
                return uniqueComments.AsEnumerable().AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingLikedContentsPeopleForOwnerByContentIdAsync(int? blogId = null, int? postId = null, int? companyId = null, int? investorId = null, int? surveyId = null, int? commentId = null, int? commentAnswerId = null)
        {
            try
            {
                if (blogId == null && postId == null && companyId == null && investorId == null && surveyId == null && commentId == null && commentAnswerId == null)
                    throw new ArgumentException("At least one content ID must be provided.", "contentId");

                var currentUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (currentUserId == null)
                    throw new ArgumentNullException(nameof(currentUserId), "currentUserId was null");

                var savedContents = _likeRepository.GetAllInclude(new Expression<Func<Like, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i=>i.IsLiked==true,
                    i => (blogId != null && i.BlogId == blogId&& i.Blog.AppUserId == currentUserId)
                    || (postId != null && i.PostId == postId&& i.Post.AppUserId == currentUserId)
                    || (companyId != null && i.CompanyId == companyId&& i.Company.AppUserId == currentUserId)
                    || (investorId != null && i.InvestorId == investorId&& i.Investor.AppUserId == currentUserId)
                    || (surveyId != null && i.SurveyId == surveyId&& i.Survey.AppUserId == currentUserId)
                    || (commentId != null && i.CommentId == commentId&& i.Comment.AppUserId == currentUserId)
                    || (commentAnswerId != null && i.CommentAnswerId == commentAnswerId&& i.CommentAnswer.AppUserId == currentUserId)
                }, y => y.AppUser);

                if (savedContents == null || !savedContents.Any())
                    return Enumerable.Empty<AppUser>().AsQueryable();

                var users = savedContents.OrderByDescending(i => i.CreatedDate).Select(x => x.AppUser).Distinct();
                return users;
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<Like> GetAllIncludingCompanyLikesPeopleByCompanyId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var likes = _likeRepository.GetAllIncludingByPropertyPath(userId, "Company.AppUserId", new Expression<Func<Like, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.IsLiked == true,
                }, y => y.Company, y => y.Company.AppUser, y => y.AppUser);
                return likes.OrderByDescending(i => i.CreatedDate).Distinct();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Like>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Like>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _likeRepository.GetAllIncludeAsync(new Expression<Func<Like, bool>>[]
                {

                }, null, y => y.AppUser, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Like>();
            }
        }
    }
}
