using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class BlockedMessageUserManager : IBlockedMessageUserService
    {
        readonly IBlockedMessageUserRepository _blockedMessageUserRepository;
        public BlockedMessageUserManager(IBlockedMessageUserRepository blockedMessageUserRepository)
        {
            _blockedMessageUserRepository = blockedMessageUserRepository;
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _blockedMessageUserRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(MessageUserBlockList entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _blockedMessageUserRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _blockedMessageUserRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<MessageUserBlockList>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _blockedMessageUserRepository.GetAllIncludeAsync(new Expression<Func<MessageUserBlockList, bool>>[]
                {
                   
                }, null, y => y.Blocked, y => y.Blocker);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<MessageUserBlockList>();
            }
        }

        public IQueryable<MessageUserBlockList> GetAllIncludingAsync()
        {
            try
            {
                var data = _blockedMessageUserRepository.GetAllInclude(new Expression<Func<MessageUserBlockList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Blocked, y => y.Blocker);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<MessageUserBlockList>().AsQueryable();
            }
        }

        public IQueryable<MessageUserBlockList> GetAllIncludingByBlockedAsync()
        {
            try
            {
                var data = _blockedMessageUserRepository.GetAllInclude(new Expression<Func<MessageUserBlockList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsBlocked==true
                }, null, y => y.Blocked, y => y.Blocker);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<MessageUserBlockList>().AsQueryable();
            }
        }

        public IQueryable<MessageUserBlockList> GetAllIncludingByBlockedIdAsync(string blockedId)
        {
            try
            {
                if (blockedId == null)
                    throw new ArgumentNullException(nameof(blockedId), "blockedId was null");

                var data = _blockedMessageUserRepository.GetAllIncludeById(blockedId, "BlockedId", new Expression<Func<MessageUserBlockList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blocked, y => y.Blocker);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<MessageUserBlockList>().AsQueryable();
            }
        }

        public IQueryable<MessageUserBlockList> GetAllIncludingByBlockerIdAsync(string blockerId)
        {
            try
            {
                if (blockerId == null)
                    throw new ArgumentNullException(nameof(blockerId), "blockerId was null");

                var data = _blockedMessageUserRepository.GetAllIncludeById(blockerId, "BlockerId", new Expression<Func<MessageUserBlockList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Blocked, y => y.Blocker);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<MessageUserBlockList>().AsQueryable();
            }
        }

        public IQueryable<MessageUserBlockList> GetAllIncludingByRemovedMessageUserAsync()
        {
            try
            {
                var data = _blockedMessageUserRepository.GetAllInclude(new Expression<Func<MessageUserBlockList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsRemoved==true
                }, null, y => y.Blocked, y => y.Blocker);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<MessageUserBlockList>().AsQueryable();
            }
        }

        public IQueryable<MessageUserBlockList> GetAllIncludingByUnblockedAsync()
        {
            try
            {
                var data = _blockedMessageUserRepository.GetAllInclude(new Expression<Func<MessageUserBlockList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsBlocked==false
                }, null, y => y.Blocked, y => y.Blocker);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<MessageUserBlockList>().AsQueryable();
            }
        }

        public IQueryable<MessageUserBlockList> GetAllIncludingByUnRemovedMessageUserAsync()
        {
            try
            {
                var data = _blockedMessageUserRepository.GetAllInclude(new Expression<Func<MessageUserBlockList, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsRemoved==false
                }, null, y => y.Blocked, y => y.Blocker);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<MessageUserBlockList>().AsQueryable();
            }
        }

        public IQueryable<MessageUserBlockList> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _blockedMessageUserRepository.GetAllInclude(new Expression<Func<MessageUserBlockList, bool>>[]
                {

                }, null, y => y.Blocked, y => y.Blocker);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<MessageUserBlockList>().AsQueryable();
            }
        }

        public async Task<MessageUserBlockList> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _blockedMessageUserRepository.GetIncludeAsync(i => i.Id == id, y => y.Blocked, y => y.Blocker);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _blockedMessageUserRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _blockedMessageUserRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _blockedMessageUserRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _blockedMessageUserRepository.SetNotDeletedAsync(id);
            return result;
        }
    }
}
