using System.Linq.Expressions;
using System.Security.Claims;
using Ganss.Xss;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class ReportManager : IReportService
    {
        readonly IReportRepository _reportRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public ReportManager(IReportRepository reportRepository, IHttpContextAccessor httpContextAccessor, IHtmlSanitizer htmlSanitizer)
        {
            _reportRepository = reportRepository;
            _httpContextAccessor = httpContextAccessor;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<bool> CreateAnnouncementReportAsync(string title, string subject, int? announcementId, string appUserId, int reportCategoryId)
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

                if (announcementId == null)
                    throw new ArgumentNullException(nameof(announcementId), "announcementId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    AnnouncementId = announcementId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateBlogReportAsync(string title, string subject, int? blogId, string appUserId, int reportCategoryId)
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

                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    BlogId = blogId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateCommentAnswerReportAsync(string title, string subject, int? commentAnswerId, string appUserId, int reportCategoryId)
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

                if (commentAnswerId == null)
                    throw new ArgumentNullException(nameof(commentAnswerId), "commentAnswerId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    CommentAnswerId = commentAnswerId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateCommentReportAsync(string title, string subject, int? commentId, string appUserId, int reportCategoryId)
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

                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    CommentId = commentId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateCompanyReportAsync(string title, string subject, int? companyId, string appUserId, int reportCategoryId)
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

                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    CompanyId = companyId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateInvestorReportAsync(string title, string subject, int? investorId, string appUserId, int reportCategoryId)
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

                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    InvestorId = investorId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateNewsReportAsync(string title, string subject, int? newsId, string appUserId, int reportCategoryId)
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

                if (newsId == null)
                    throw new ArgumentNullException(nameof(newsId), "newsId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    NewsId = newsId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreatePostReportAsync(string title, string subject, int? postId, string appUserId, int reportCategoryId)
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

                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    PostId = postId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateSectorNewsReportAsync(string title, string subject, int? sectorNewsId, string appUserId, int reportCategoryId)
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

                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    SectorNewsId = sectorNewsId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateSurveyReportAsync(string title, string subject, int? surveyId, string appUserId, int reportCategoryId)
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

                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeSubject = _htmlSanitizer.Sanitize(subject ?? string.Empty);
                var entity = new Report
                {
                    Title = title,
                    Subject = safeSubject,
                    SurveyId = surveyId,
                    AppUserId = appUserId,
                    ReportCategoryId = reportCategoryId
                };
                if (entity != null)
                {
                    var result = await _reportRepository.AddAsync(entity);
                    return result;
                }
                return false;
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

                var result = await _reportRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Report entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _reportRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _reportRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Report>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _reportRepository.GetAllIncludeAsync(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Report>();
            }
        }

        public IQueryable<Report> GetAllIncludingAsync()
        {
            try
            {
                var data = _reportRepository.GetAllInclude(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByAnnouncementIdAsync(int? announcementId)
        {
            try
            {
                if (announcementId == null)
                    throw new ArgumentNullException(nameof(announcementId), "announcementId was null");

                var data = _reportRepository.GetAllIncludeById(announcementId, "AnnouncementId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var data = _reportRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByCommentAnswerIdAsync(int? commentAnswerId)
        {
            try
            {
                if (commentAnswerId == null)
                    throw new ArgumentNullException(nameof(commentAnswerId), "commentAnswerId was null");

                var data = _reportRepository.GetAllIncludeById(commentAnswerId, "CommentAnswerId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByCommentIdAsync(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var data = _reportRepository.GetAllIncludeById(commentId, "CommentId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _reportRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByFixedReportAsync()
        {
            try
            {
                var data = _reportRepository.GetAllInclude(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFixed==true,
                    i=>i.FixedDate!=null
                }, null, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.FixedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByInvestorIdAsync(int? investorId)
        {
            try
            {
                if (investorId == null)
                    throw new ArgumentNullException(nameof(investorId), "investorId was null");

                var data = _reportRepository.GetAllIncludeById(investorId, "InvestorId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByNewsIdAsync(int? newsId)
        {
            try
            {
                if (newsId == null)
                    throw new ArgumentNullException(nameof(newsId), "newsId was null");

                var data = _reportRepository.GetAllIncludeById(newsId, "NewsId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByNotFixedReportAsync()
        {
            try
            {
                var data = _reportRepository.GetAllInclude(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsFixed==false
                }, null, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByPostIdAsync(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var data = _reportRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByReportCategoryIdAsync(int reportCategoryId)
        {
            try
            {
                var data = _reportRepository.GetAllIncludeById(reportCategoryId, "ReportCategoryId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingBySectorNewsIdAsync(int? sectorNewsId)
        {
            try
            {
                if (sectorNewsId == null)
                    throw new ArgumentNullException(nameof(sectorNewsId), "sectorNewsId was null");

                var data = _reportRepository.GetAllIncludeById(sectorNewsId, "SectorNewsId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingBySurveyIdAsync(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var data = _reportRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByTodaysReportAsync()
        {
            try
            {
                var today = DateTime.Today;
                var data = _reportRepository.GetAllInclude(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                     i=>i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _reportRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _reportRepository.GetAllInclude(new Expression<Func<Report, bool>>[]
                {

                }, null, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingReportsForReportOwnerByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var reports = _reportRepository.GetAllInclude(new Expression<Func<Report, bool>>[]
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
                    || (i.AnnouncementId!=null&&i.Announcement.Company.AppUserId==userId)
                    ||(i.AnnouncementId!=null&&i.Announcement.Investor.AppUserId==userId)
                }, y => y.ReportCategory, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswer, y => y.Comment, y => y.Investor, y => y.Investor.AppUser, y => y.Survey, y => y.Announcement.Company, y => y.Announcement.Investor);

                if (reports == null || !reports.Any())
                    return Enumerable.Empty<Report>().AsQueryable();

                var uniqueComments = reports.AsEnumerable().GroupBy(c => new { c.BlogId, c.CompanyId, c.CommentId, c.CommentAnswerId, c.InvestorId, c.PostId, c.SurveyId, c.AnnouncementId }).Select(g => g.OrderByDescending(c => c.CreatedDate).First()).OrderByDescending(c => c.CreatedDate);
                return uniqueComments.AsEnumerable().AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingReportsForUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _reportRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.Investor.AppUser, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public IQueryable<Report> GetAllIncludingTodaysReportsForAdminHeader()
        {
            try
            {
                var today = DateTime.Today;
                return _reportRepository.GetAllInclude(new Expression<Func<Report, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                     i=>i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey).OrderByDescending(i => i.CreatedDate).Take(25);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Report>().AsQueryable();
            }
        }

        public async Task<Report> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _reportRepository.GetIncludeAsync(i => i.Id == id, y => y.AppUser, y => y.ReportCategory, y => y.Announcement, y => y.Blog, y => y.Company, y => y.Comment, y => y.CommentAnswer, y => y.Investor, y => y.News, y => y.Post, y => y.SectorNews, y => y.Survey);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public int ReportCounter()
        {
            return _reportRepository.ReportCounter();
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _reportRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _reportRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _reportRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetFixedReportAsync(int id)
        {
            var result = await _reportRepository.SetFixedReportAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _reportRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotFixedReportAsync(int id)
        {
            var result = await _reportRepository.SetNotFixedReportAsync(id);
            return result;
        }
    }
}
