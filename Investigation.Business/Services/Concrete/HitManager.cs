using System.Linq.Expressions;
using System.Security.Claims;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class HitManager : IHitService
    {
        readonly IHitRepository _hitRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        public HitManager(IHitRepository hitRepository, IHttpContextAccessor httpContextAccessor)
        {
            _hitRepository = hitRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Hit AdHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "adId was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.AdId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        AdId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit AnnouncementHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.AnnouncementId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        AnnouncementId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit BlogHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.BlogId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        BlogId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit CommentAnswerHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "commentAnswerId was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.CommentAnswerId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        CommentAnswerId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit CommentHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "commentId was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.CommentId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        CommentId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit CompanyFinanceHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.CompanyFinanceId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        CompanyFinanceId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit CompanyHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.CompanyId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        CompanyId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit CompanyPintechHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.CompanyPintechId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        CompanyPintechId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit CompanyStageHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "companyStageId was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.CompanyStageId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        CompanyStageId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit InvestorHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.InvestorId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        InvestorId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit PostHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }
                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.PostId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        PostId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public Hit SurveyHit(int? id, string appUserId, int currentValue)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                //appUserId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value
                          ?? _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var sessionUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");

                appUserId = userIdClaim ?? sessionUserId;

                if (string.IsNullOrEmpty(appUserId))
                {
                    throw new UnauthorizedAccessException("User not authenticated. UserId not found in claims or session.");
                }

                var existingHit = _hitRepository.Get(d => d.AppUserId == appUserId && d.SurveyId == id);
                if (existingHit != null)
                {
                    return existingHit;
                }
                else
                {
                    var newHit = new Hit
                    {
                        SurveyId = id,
                        AppUserId = string.IsNullOrEmpty(appUserId) ? "VISITOR" : appUserId,
                        CurrentValue = currentValue + 1,
                    };
                    _hitRepository.Add(newHit);
                    return newHit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving Hit value the entity.", ex);
            }
        }

        public IQueryable<Hit> GetAllIncludingAsync()
        {
            try
            {
                var data = _hitRepository.GetAllInclude(new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByAdIdAsync(int? adId)
        {
            try
            {
                if (adId == null)
                    throw new ArgumentNullException(nameof(adId), "adId was null");

                var data = _hitRepository.GetAllIncludeById(adId, "AdId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByAnnouncementIdAsync(int? announcementId)
        {
            try
            {
                if (announcementId == null)
                    throw new ArgumentNullException(nameof(announcementId), "announcementId was null");

                var data = _hitRepository.GetAllIncludeById(announcementId, "AnnouncementId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var data = _hitRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByCommentAnswerIdAsync(int? commentAnswerId)
        {
            try
            {
                if (commentAnswerId == null)
                    throw new ArgumentNullException(nameof(commentAnswerId), "commentAnswerId was null");

                var data = _hitRepository.GetAllIncludeById(commentAnswerId, "CommentAnswerId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByCommentIdAsync(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var data = _hitRepository.GetAllIncludeById(commentId, "CommentId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByCompanyFinanceIdAsync(int? companyFinanceId)
        {
            try
            {
                if (companyFinanceId == null)
                    throw new ArgumentNullException(nameof(companyFinanceId), "companyFinanceId was null");

                var data = _hitRepository.GetAllIncludeById(companyFinanceId, "CompanyFinanceId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _hitRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByCompanyPintechIdAsync(int? companyPintechId)
        {
            try
            {
                if (companyPintechId == null)
                    throw new ArgumentNullException(nameof(companyPintechId), "companyPintechId was null");

                var data = _hitRepository.GetAllIncludeById(companyPintechId, "CompanyPintechId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByCompanyStageIdAsync(int? companyStageId)
        {
            try
            {
                if (companyStageId == null)
                    throw new ArgumentNullException(nameof(companyStageId), "companyStageId was null");

                var data = _hitRepository.GetAllIncludeById(companyStageId, "CompanyStageId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _hitRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByPostIdAsync(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var data = _hitRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingBySurveyIdAsync(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var data = _hitRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _hitRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByLessHitValueAsync()
        {
            try
            {
                var data = _hitRepository.GetAllInclude(new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CurrentValue>=20
                }, null, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderBy(i => i.CurrentValue);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingByMostHitValueAsync()
        {
            try
            {
                var data = _hitRepository.GetAllInclude(new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CurrentValue);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _hitRepository.GetAllInclude(new Expression<Func<Hit, bool>>[]
                {

                }, null, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public async Task<Hit> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _hitRepository.GetIncludeAsync(i => i.Id == id, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
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

                var result = await _hitRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Hit entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _hitRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _hitRepository.DeleteAsync(data);
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
            var result = await _hitRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _hitRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _hitRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _hitRepository.SetNotDeletedAsync(id);
            return result;
        }

        public int HitCounter()
        {
            return _hitRepository.HitCounter();
        }

        public IQueryable<Hit> GetAllIncludingHitsForUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _hitRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<Hit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.CompanyFinance.Company, y => y.CompanyStage.Company, y => y.CompanyPintech.Company, y => y.Investor, y => y.Investor.AppUser, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingHitsForHitOwnerByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var hits = _hitRepository.GetAllInclude(new Expression<Func<Hit, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => (i.BlogId != null && i.Blog.AppUserId == userId)
                    || (i.CompanyId != null && i.Company.AppUserId == userId)
                    ||(i.CommentId!= null && i.Comment.AppUserId== userId)
                    ||(i.CommentAnswerId!= null && i.CommentAnswer.AppUserId== userId)
                    ||(i.CompanyFinanceId!= null && i.CompanyFinance.Company.AppUserId== userId)
                    ||(i.AnnouncementId!= null && i.Announcement.Company.AppUserId== userId)
                    ||(i.AnnouncementId!= null && i.Announcement.Investor.AppUserId== userId)
                    ||(i.CompanyPintechId!= null && i.CompanyPintech.Company.AppUserId== userId)
                    ||(i.CompanyStageId!= null && i.CompanyStage.Company.AppUserId== userId)
                    || (i.InvestorId != null && i.Investor.AppUserId == userId)
                    || (i.Post != null && i.Post.AppUserId == userId)
                    || (i.SurveyId != null && i.Survey.AppUserId == userId)
                }, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswer, y => y.Comment, y => y.Announcement, y => y.Announcement.Company, y => y.Announcement.Investor, y => y.Company.CompanyFinances, y => y.Company.CompanyStages, y => y.Company.CompanyPinteches, y => y.Investor, y => y.Survey);

                if (hits == null || !hits.Any())
                    return Enumerable.Empty<Hit>().AsQueryable();

                var uniqueComments = hits.AsEnumerable().GroupBy(c => new { c.BlogId, c.CompanyId, c.InvestorId, c.PostId, c.SurveyId, c.CommentId, c.CommentAnswerId, c.CompanyFinanceId, c.CompanyPintechId, c.CompanyStageId, c.AnnouncementId }).Select(g => g.OrderByDescending(c => c.CreatedDate).First()).OrderByDescending(c => c.CreatedDate);
                return uniqueComments.AsEnumerable().AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public IQueryable<AppUser> GetAllIncludingHitContentsPeopleForOwnerByContentIdAsync(int? blogId = null, int? postId = null, int? companyId = null, int? investorId = null, int? surveyId = null, int? commentId = null, int? commentAnswerId = null, int? announcementId = null, int? companyFinanceId = null, int? companyStageId = null, int? companyPintechId = null)
        {
            try
            {
                if (blogId == null && postId == null && companyId == null && investorId == null && surveyId == null && commentId == null && commentAnswerId == null && announcementId == null && companyFinanceId == null && companyPintechId == null && companyStageId == null)
                    throw new ArgumentException("At least one content ID must be provided.", "contentId");

                var currentUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (currentUserId == null)
                    throw new ArgumentNullException(nameof(currentUserId), "currentUserId was null");

                var hitContents = _hitRepository.GetAllInclude(new Expression<Func<Hit, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => (blogId != null && i.BlogId == blogId&& i.Blog.AppUserId == currentUserId)
                    || (postId != null && i.PostId == postId&& i.Post.AppUserId == currentUserId)
                    || (companyId != null && i.CompanyId == companyId&& i.Company.AppUserId == currentUserId)
                    || (investorId != null && i.InvestorId == investorId&& i.Investor.AppUserId == currentUserId)
                    || (surveyId != null && i.SurveyId == surveyId&& i.Survey.AppUserId == currentUserId)
                    || (commentId != null && i.CommentId == commentId&& i.Comment.AppUserId == currentUserId)
                    || (commentAnswerId != null && i.CommentAnswerId == commentAnswerId&& i.CommentAnswer.AppUserId == currentUserId)
                    || (announcementId != null && i.AnnouncementId == announcementId && (i.Announcement.Company.AppUserId == currentUserId
                    || i.Announcement.Investor.AppUserId == currentUserId))
                    || (companyFinanceId != null && i.CompanyFinanceId == companyFinanceId&& i.CompanyFinance.Company.AppUserId == currentUserId)
                    || (companyPintechId != null && i.CompanyPintechId == companyPintechId&& i.CompanyPintech.Company.AppUserId == currentUserId)
                    || (companyStageId != null && i.CompanyStageId == companyStageId&& i.CompanyStage.Company.AppUserId == currentUserId)
                }, y => y.AppUser);

                if (hitContents == null || !hitContents.Any())
                    return Enumerable.Empty<AppUser>().AsQueryable();

                var users = hitContents.OrderByDescending(i => i.CreatedDate).Select(x => x.AppUser).Distinct();
                return users;
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppUser>().AsQueryable();
            }
        }

        public IQueryable<Hit> GetAllIncludingCompanyHitsPeopleByCompanyId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var hits = _hitRepository.GetAllInclude(new Expression<Func<Hit, bool>>[] {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => i.CompanyId != null && i.Company.AppUserId == userId
                }, y => y.Company, y => y.Company.AppUser, y => y.AppUser).OrderByDescending(i => i.CreatedDate);

                if (hits == null || !hits.Any())
                    return Enumerable.Empty<Hit>().AsQueryable();

                var uniqueHits = hits.AsEnumerable()
                    .Where(h => h.AppUserId != null)
                    .GroupBy(c => new { c.CompanyId, c.AppUserId })
                    .Select(g => g.OrderByDescending(c => c.CreatedDate).First()).OrderByDescending(c => c.CreatedDate);

                return uniqueHits.AsEnumerable().AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Hit>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Hit>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _hitRepository.GetAllIncludeAsync(new Expression<Func<Hit, bool>>[]
                {

                }, null, y => y.AppUser, y => y.Ad, y => y.Announcement, y => y.Blog, y => y.Comment, y => y.CommentAnswer, y => y.Company, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage, y => y.Investor, y => y.Post, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Hit>();
            }
        }
    }
}
