using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class DataPolicyManager : IDataPolicyService
    {
        readonly IDataPolicyRepository _dataPolicyRepository;
        public DataPolicyManager(IDataPolicyRepository dataPolicyRepository)
        {
            _dataPolicyRepository = dataPolicyRepository;
        }

        public async Task<bool> CreateAsync(DataPolicy entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _dataPolicyRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(DataPolicy entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _dataPolicyRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _dataPolicyRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<DataPolicy> GetAllAsync()
        {
            try
            {
                var data = _dataPolicyRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<DataPolicy>().AsQueryable();
            }
        }

        public IQueryable<DataPolicy> GetAllForAdminAsync()
        {
            try
            {
                var data = _dataPolicyRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<DataPolicy>().AsQueryable();
            }
        }

        public async Task<IEnumerable<DataPolicy>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _dataPolicyRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<DataPolicy>();
            }
        }

        public IQueryable<DataPolicy> GetAllForSitemap()
        {
            try
            {
                return _dataPolicyRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<DataPolicy>().AsQueryable();
            }
        }

        public async Task<DataPolicy> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _dataPolicyRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _dataPolicyRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _dataPolicyRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _dataPolicyRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _dataPolicyRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(DataPolicy entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _dataPolicyRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
