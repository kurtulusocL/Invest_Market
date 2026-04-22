using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using Ganss.Xss;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class CommentManager : ICommentService
    {
        readonly ICommentRepository _commentRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public CommentManager(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IHtmlSanitizer htmlSanitizer)
        {
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _htmlSanitizer = htmlSanitizer;
        }

        public int CommentCounter()
        {
            return _commentRepository.CommentCounter();
        }

        public async Task<bool> CreateBlogCommentAsync(string text, int? blogId, string appUserId)
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

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                var entity = new Comment
                {
                    Text = safeText,
                    BlogId = blogId,
                    AppUserId = appUserId
                };
                var result = await _commentRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateCompanyCommentAsync(string text, int? companyId, string appUserId)
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
                string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                var entity = new Comment
                {
                    Text = safeText,
                    CompanyId = companyId,
                    AppUserId = appUserId
                };
                var result = await _commentRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreatePostCommentAsync(string text, int? postId, string appUserId)
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

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                var entity = new Comment
                {
                    Text = safeText,
                    PostId = postId,
                    AppUserId = appUserId
                };
                var result = await _commentRepository.AddAsync(entity);
                return result;
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

                var result = await _commentRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Comment entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _commentRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _commentRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Comment>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _commentRepository.GetAllIncludeAsync(new Expression<Func<Comment, bool>>[]
                {

                }, null, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public IQueryable<Comment> GetAllIncludingAsync()
        {
            try
            {
                var data = _commentRepository.GetAllInclude(new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingByBlogIdAsync(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                var data = _commentRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _commentRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingByPostIdAsync(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                var data = _commentRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingByTodaysCommentsAsync()
        {
            try
            {
                var today = DateTime.Today;
                var data = _commentRepository.GetAllInclude(new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _commentRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingCommentForUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _commentRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingCommentsForCommentOwnerByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var comments = _commentRepository.GetAllInclude(new Expression<Func<Comment, bool>>[]
                {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => (i.PostId != null && i.Post.AppUserId == userId) || (i.BlogId != null && i.Blog.AppUserId == userId) || (i.CompanyId != null && i.Company.AppUserId == userId)
                }, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);

                if (comments == null || !comments.Any())
                    return Enumerable.Empty<Comment>().AsQueryable();

                var uniqueComments = comments.AsEnumerable().GroupBy(c => new { c.PostId, c.BlogId, c.CompanyId }).Select(g => g.OrderByDescending(c => c.CreatedDate).First()).OrderByDescending(c => c.CreatedDate);
                return uniqueComments.AsEnumerable().AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingCompanyBlogCommentByBlogId(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                return _commentRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingCompanyCommentByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _commentRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingCompanyPostCommentByPostId(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                return _commentRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _commentRepository.GetAllInclude(new Expression<Func<Comment, bool>>[]
                {

                }, null, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingInvestorBlogCommentsByBlogId(int? blogId)
        {
            try
            {
                if (blogId == null)
                    throw new ArgumentNullException(nameof(blogId), "blogId was null");

                return _commentRepository.GetAllIncludeById(blogId, "BlogId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Blog, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingInvestorPostCommentByPostId(int? postId)
        {
            try
            {
                if (postId == null)
                    throw new ArgumentNullException(nameof(postId), "postId was null");

                return _commentRepository.GetAllIncludeById(postId, "PostId", new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public IQueryable<Comment> GetAllIncludingTodaysCommentForAdminHeader()
        {
            try
            {
                var today = DateTime.Today;
                return _commentRepository.GetAllInclude(new Expression<Func<Comment, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.CreatedDate >= today && i.CreatedDate < today.AddDays(1)
                }, null, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports).OrderByDescending(i => i.CreatedDate).Take(25);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Comment>().AsQueryable();
            }
        }

        public async Task<Comment> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _commentRepository.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.Blog, y => y.Post, y => y.AppUser, y => y.CommentAnswers, y => y.Hits, y => y.Likes, y => y.Reports);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Comment GetCommentForFormById(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                return _commentRepository.GetInclude(i => i.Id == commentId, y => y.CommentAnswers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _commentRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _commentRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _commentRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _commentRepository.SetNotDeletedAsync(id);
            return result;
        }
    }
}
