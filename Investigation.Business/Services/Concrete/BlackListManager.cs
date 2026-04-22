using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class BlackListManager : IBlackListService
    {
        readonly IBlackListRepository _blackListRepository;
        public BlackListManager(IBlackListRepository blackListRepository)
        {
            _blackListRepository = blackListRepository;
        }

        public async Task<bool> CreateAsync(string remoteIpAddress, string ipAddressWithVPN, string? deviceFingerprint, string localIpAddress, DateTime expirationDate, int? auditId)
        {
            try
            {
                if (auditId == null)
                    throw new ArgumentNullException(nameof(auditId), "auditId was null");

                var entity = new BlackList
                {
                    RemoteIpAddress = remoteIpAddress,
                    IpAddressVPN = ipAddressWithVPN,
                    DeviceFingerprint = deviceFingerprint,
                    LocalIpAddress = localIpAddress,
                    ExpirationDate = expirationDate,
                    AuditId = auditId
                };
                if (entity != null)
                {
                    var result = await _blackListRepository.AddAsync(entity);
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

                var result = await _blackListRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(BlackList entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _blackListRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _blackListRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<BlackList>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _blackListRepository.GetAllIncludeAsync(new Expression<Func<BlackList, bool>>[]
                {
                    
                }, null, y => y.Audit);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<BlackList>();
            }
        }

        public IQueryable<BlackList> GetAllIncludingAsync()
        {
            try
            {
                var data = _blackListRepository.GetAllInclude(new Expression<Func<BlackList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Audit);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlackList>().AsQueryable();
            }
        }

        public IQueryable<BlackList> GetAllIncludingByAuditIdAsync(int? auditId)
        {
            try
            {
                if (auditId == null)
                    throw new ArgumentNullException(nameof(auditId), "auditId was null");

                var data = _blackListRepository.GetAllIncludeById(auditId, "AuditId", new Expression<Func<BlackList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Audit);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlackList>().AsQueryable();
            }
        }

        public IQueryable<BlackList> GetAllIncludingByExpirationDateAsync()
        {
            try
            {
                var data = _blackListRepository.GetAllInclude(new Expression<Func<BlackList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Audit);
                return data.OrderByDescending(i => i.ExpirationDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlackList>().AsQueryable();
            }
        }

        public IQueryable<BlackList> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _blackListRepository.GetAllInclude(new Expression<Func<BlackList, bool>>[]
                {

                }, null, y => y.Audit);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<BlackList>().AsQueryable();
            }
        }

        public async Task<BlackList> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _blackListRepository.GetIncludeAsync(i => i.Id == id, y => y.Audit);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _blackListRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _blackListRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _blackListRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _blackListRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string remoteIpAddress, string ipAddressWithVPN, string? deviceFingerprint, string localIpAddress, DateTime expirationDate, int? auditId, int id)
        {
            try
            {
                if (auditId == null)
                    throw new ArgumentNullException(nameof(auditId), "auditId was null");

                var entity = new BlackList
                {
                    RemoteIpAddress = remoteIpAddress,
                    IpAddressVPN = ipAddressWithVPN,
                    DeviceFingerprint = deviceFingerprint,
                    LocalIpAddress = localIpAddress,
                    ExpirationDate = expirationDate,
                    AuditId = auditId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                if (entity != null)
                {
                    var result = await _blackListRepository.UpdateAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
