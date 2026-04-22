using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class CancelMembershipCategoryManager : ICancelMembershipCategoryService
    {
        readonly ICancelMembershipCategoryRepository _cancelMembershipCategoryRepository;

        public CancelMembershipCategoryManager(ICancelMembershipCategoryRepository cancelMembershipCategoryRepository)
        {
            _cancelMembershipCategoryRepository = cancelMembershipCategoryRepository;
        }

        public async Task<bool> CreateAsync(CancelMembershipCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _cancelMembershipCategoryRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CancelMembershipCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _cancelMembershipCategoryRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _cancelMembershipCategoryRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<CancelMembershipCategory>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _cancelMembershipCategoryRepository.GetAllIncludeAsync(new Expression<Func<CancelMembershipCategory, bool>>[]
                {
                    
                }, null, y => y.CancelMemberships);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CancelMembershipCategory>();
            }
        }

        public IQueryable<CancelMembershipCategory> GetAllIncludingAsync()
        {
            try
            {
                var data = _cancelMembershipCategoryRepository.GetAllInclude(new Expression<Func<CancelMembershipCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CancelMemberships);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembershipCategory>().AsQueryable();
            }
        }

        public IQueryable<CancelMembershipCategory> GetAllIncludingByCancelMembershipQuantityAsync()
        {
            try
            {
                var data = _cancelMembershipCategoryRepository.GetAllInclude(new Expression<Func<CancelMembershipCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CancelMemberships);
                return data.OrderByDescending(i => i.CancelMemberships.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembershipCategory>().AsQueryable();
            }
        }

        public IQueryable<CancelMembershipCategory> GetAllIncludingForAddCancelMembershipAsync()
        {
            try
            {
                var data = _cancelMembershipCategoryRepository.GetAllInclude(new Expression<Func<CancelMembershipCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CancelMemberships);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembershipCategory>().AsQueryable();
            }
        }

        public IQueryable<CancelMembershipCategory> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _cancelMembershipCategoryRepository.GetAllInclude(new Expression<Func<CancelMembershipCategory, bool>>[]
                {

                }, null, y => y.CancelMemberships);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembershipCategory>().AsQueryable();
            }
        }

        public IQueryable<CancelMembershipCategory> GetAllIncludingForAdminHome()
        {
            try
            {
                return _cancelMembershipCategoryRepository.GetAllInclude(new Expression<Func<CancelMembershipCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CancelMemberships).OrderByDescending(i => i.CancelMemberships.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<CancelMembershipCategory>().AsQueryable();
            }
        }

        public async Task<CancelMembershipCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _cancelMembershipCategoryRepository.GetIncludeAsync(i => i.Id == id, y => y.CancelMemberships);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _cancelMembershipCategoryRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _cancelMembershipCategoryRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _cancelMembershipCategoryRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _cancelMembershipCategoryRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(CancelMembershipCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _cancelMembershipCategoryRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
