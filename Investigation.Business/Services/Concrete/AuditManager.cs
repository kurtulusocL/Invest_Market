using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Concrete
{
    public class AuditManager : IAuditService
    {
        readonly IAuditRepository _auditRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        public AuditManager(IAuditRepository auditRepository, IHttpContextAccessor httpContextAccessor)
        {
            _auditRepository = auditRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> DeleteAllAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _auditRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Audit entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _auditRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _auditRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Audit>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _auditRepository.GetAllIncludeAsync(new Expression<Func<Audit, bool>>[]
                {

                }, null, y => y.BlackLists);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Audit>();
            }
        }

        public IQueryable<Audit> GetAllIncludingAsync()
        {
            try
            {
                var data = _auditRepository.GetAllInclude(new Expression<Func<Audit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.BlackLists);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Audit>().AsQueryable();
            }
        }

        public IQueryable<Audit> GetAllIncludingByMobileAsync()
        {
            try
            {
                var data = _auditRepository.GetAllInclude(new Expression<Func<Audit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsMobile==true
                }, null, y => y.BlackLists);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Audit>().AsQueryable();
            }
        }

        public IQueryable<Audit> GetAllIncludingByUserIdAsync(string userId)
        {
            try
            {
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("userId");
                userId ??= _httpContextAccessor.HttpContext.Session.GetString("adminId");
                if (userId == null)
                    throw new ArgumentNullException(nameof(userId), "userId was null");

                var data = _auditRepository.GetAllIncludeById(userId, "UserId", new Expression<Func<Audit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.BlackLists);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Audit>().AsQueryable();
            }
        }

        public IQueryable<Audit> GetAllIncludingByVisitorAuditAsync()
        {
            try
            {
                var data = _auditRepository.GetAllInclude(new Expression<Func<Audit, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.UserId==null||i.UserName==null
                }, null, y => y.BlackLists);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Audit>().AsQueryable();
            }
        }

        public IQueryable<Audit> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _auditRepository.GetAllInclude(new Expression<Func<Audit, bool>>[]
                {

                }, null, y => y.BlackLists);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Audit>().AsQueryable();
            }
        }

        public async Task<Audit> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _auditRepository.GetIncludeAsync(i => i.Id == id, y => y.BlackLists);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _auditRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _auditRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _auditRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _auditRepository.SetNotDeletedAsync(id);
            return result;
        }
    }
}
