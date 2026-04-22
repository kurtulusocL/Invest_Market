using System.Linq.Expressions;
using System.Security.Claims;
using Ganss.Xss;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class CancelMembershipManager : ICancelMembershipService
    {
        readonly ICancelMembershipRepository _cancelMembershipRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHtmlSanitizer _htmlSanitizer;
        public CancelMembershipManager(ICancelMembershipRepository cancelMembershipRepository, IHttpContextAccessor httpContextAccessor, IHtmlSanitizer htmlSanitizer)
        {
            _cancelMembershipRepository = cancelMembershipRepository;
            _httpContextAccessor = httpContextAccessor;
            _htmlSanitizer = htmlSanitizer;
        }

        public async Task<bool> CreateAsync(string title, string desc, int cancelMembershipCategoryId, string appUserId)
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

                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeDesc = _htmlSanitizer.Sanitize(desc ?? string.Empty);
                var entity = new CancelMembership
                {
                    Title = title,
                    Desc = safeDesc,
                    CancelMembershipCategoryId = cancelMembershipCategoryId,
                    AppUserId = appUserId
                };
                if (entity != null)
                {
                    var result = await _cancelMembershipRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CancelMembership entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _cancelMembershipRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _cancelMembershipRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CancelMembership>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _cancelMembershipRepository.GetAllIncludeAsync(new Expression<Func<CancelMembership, bool>>[]
                {
                    
                }, null, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembership>();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingAsync()
        {
            try
            {
                var data = _cancelMembershipRepository.GetAllInclude(new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingByCancelledMembershipAsync()
        {
            try
            {
                var data = _cancelMembershipRepository.GetAllInclude(new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCancelled==true
                }, null, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.CancelDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingByCancelledRequestAsync()
        {
            try
            {
                var data = _cancelMembershipRepository.GetAllInclude(new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsRequestCancelled==true
                }, null, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.RequestCancelledDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingByCancelMembershipCategoryIdAsync(int cancelMembershipCategoryId)
        {
            try
            {
                var data = _cancelMembershipRepository.GetAllIncludeById(cancelMembershipCategoryId, "CancelMembershipCategoryId", new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingByNotCancelledMembershipAsync()
        {
            try
            {
                var data = _cancelMembershipRepository.GetAllInclude(new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCancelled==false
                }, null, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingByNotSeenRequestAsync()
        {
            try
            {
                var data = _cancelMembershipRepository.GetAllInclude(new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hit<=0
                }, null, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingBySeenRequestAsync()
        {
            try
            {
                var data = _cancelMembershipRepository.GetAllInclude(new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Hit>0
                }, null, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _cancelMembershipRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingCancelMembershipForUserByUserIdAsync(string userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _cancelMembershipRepository.GetAllIncludeById(userId, "AppUserId", new Expression<Func<CancelMembership, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCancelled==false
                }, y => y.CancelMembershipCategory);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public IQueryable<CancelMembership> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _cancelMembershipRepository.GetAllInclude(new Expression<Func<CancelMembership, bool>>[]
                {

                }, null, y => y.CancelMembershipCategory, y => y.AppUser);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembership>().AsQueryable();
            }
        }

        public async Task<CancelMembership> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _cancelMembershipRepository.GetIncludeAsync(i => i.Id == id, y => y.CancelMembershipCategory, y => y.AppUser);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public CancelMembership ReadNonUniqueHit(int id)
        {
            return _cancelMembershipRepository.ReadNonUniqueHit(id);
        }

        public async Task<bool> SetAccountCancelAsync(int id)
        {
            var result = await _cancelMembershipRepository.SetAccountCancelAsync(id);
            return result;
        }

        public async Task<bool> SetAccountNotCancelAsync(int id)
        {
            var result = await _cancelMembershipRepository.SetAccountNotCancelAsync(id);
            return result;
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _cancelMembershipRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _cancelMembershipRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _cancelMembershipRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _cancelMembershipRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetRequestCancelAsync(int id)
        {
            var result = await _cancelMembershipRepository.SetRequestCancelAsync(id);
            return result;
        }

        public async Task<bool> SetRequestNotCancelAsync(int id)
        {
            var result = await _cancelMembershipRepository.SetRequestNotCancelAsync(id);
            return result;
        }
    }
}
