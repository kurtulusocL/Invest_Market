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
    public class CommentAnswerManager : ICommentAnswerService
    {
        readonly ICommentAnswerRepository _commentAnswerRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public CommentAnswerManager(ICommentAnswerRepository commentAnswerRepository, IHttpContextAccessor httpContextAccessor, IHtmlSanitizer htmlSanitizer)
        {
            _commentAnswerRepository = commentAnswerRepository;
            _httpContextAccessor = httpContextAccessor;
            _htmlSanitizer = htmlSanitizer;
        }

        public int CommentAnswerCounter()
        {
            return _commentAnswerRepository.CommentAnswerCounter();
        }

        public bool Create(string text, int? commentId, string appUserId)
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

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                var entity = new CommentAnswer
                {
                    Text = safeText,
                    CommentId = commentId,
                    AppUserId = appUserId
                };
                if (entity != null)
                {
                    var result = _commentAnswerRepository.Add(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> CreateAsync(string text, int? commentId, string appUserId)
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

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeText = _htmlSanitizer.Sanitize(text ?? string.Empty);
                var entity = new CommentAnswer
                {
                    Text = safeText,
                    CommentId = commentId,
                    AppUserId = appUserId
                };
                if (entity != null)
                {
                    var result = await _commentAnswerRepository.AddAsync(entity);
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

                var result = await _commentAnswerRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CommentAnswer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _commentAnswerRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _commentAnswerRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CommentAnswer>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _commentAnswerRepository.GetAllIncludeAsync(new Expression<Func<CommentAnswer, bool>>[]
                {
                   
                }, null, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CommentAnswer>();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingAsync()
        {
            try
            {
                var data =  _commentAnswerRepository.GetAllInclude(new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingByCommentIdAsync(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var data =  _commentAnswerRepository.GetAllIncludeById(commentId, "CommentId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data =  _commentAnswerRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingCommentAnswersByCommentId(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                return _commentAnswerRepository.GetAllIncludeById(commentId, "CommentId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForCommentAnswerOwnerByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var currentUserId = _httpContextAccessor.HttpContext.Session.GetString("userId");
                if (currentUserId == null)
                    throw new ArgumentNullException(nameof(currentUserId), "currentUserId was null");

                var commentAnswers =  _commentAnswerRepository.GetAllInclude(new Expression<Func<CommentAnswer, bool>>[]
               {
                    i => i.IsActive == true,
                    i => i.IsDeleted == false,
                    i => (i.Comment.PostId != null && i.Comment.Post.AppUserId == userId && userId == currentUserId) ||
                    (i.Comment.BlogId != null && i.Comment.Blog.AppUserId == userId && userId == currentUserId) ||
                    (i.Comment.CompanyId != null && i.Comment.Company.AppUserId == userId && userId == currentUserId)
               }, y => y.Comment, y => y.Comment.Company, y => y.Comment.Blog, y => y.Comment.Post, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports);

                if (commentAnswers == null || !commentAnswers.Any())
                    return Enumerable.Empty<CommentAnswer>().AsQueryable();

                var uniqueComments = commentAnswers.AsEnumerable().GroupBy(c => new { c.Comment.PostId, c.Comment.BlogId, c.Comment.CompanyId }).Select(g => g.OrderByDescending(c => c.CreatedDate).First()).OrderByDescending(c => c.CreatedDate);
                return uniqueComments.AsEnumerable().AsQueryable();
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForUserByCommentId(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                return _commentAnswerRepository.GetAllIncludingByPropertyPath(commentId, "CommentId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForUserByCommentIdAsync(int? commentId)
        {
            try
            {
                if (commentId == null)
                    throw new ArgumentNullException(nameof(commentId), "commentId was null");

                var data =  _commentAnswerRepository.GetAllIncludeById(commentId, "CommentId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForUserByUserId(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                return _commentAnswerRepository.GetAllIncludingByPropertyPath(userId, "AppUserId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingCommentAnswersForUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data =  _commentAnswerRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<CommentAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Comment, y => y.Comment.Company, y => y.Comment.Post, y => y.Comment.Blog, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public IQueryable<CommentAnswer> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data =  _commentAnswerRepository.GetAllInclude(new Expression<Func<CommentAnswer, bool>>[]
                {

                }, null, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CommentAnswer>().AsQueryable();
            }
        }

        public async Task<CommentAnswer> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _commentAnswerRepository.GetIncludeAsync(i => i.Id == id, y => y.Comment, y => y.AppUser, y => y.Hits, y => y.Likes, y => y.Reports);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public Task<bool> SetActiveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetDeActiveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetDeletedAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetNotDeletedAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
