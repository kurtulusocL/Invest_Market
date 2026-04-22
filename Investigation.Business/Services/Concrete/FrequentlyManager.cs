using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class FrequentlyManager : IFrequentlyService
    {
        readonly IFrequentlyRepository _frequentlyRepository;
        public FrequentlyManager(IFrequentlyRepository frequentlyRepository)
        {
            _frequentlyRepository = frequentlyRepository;
        }

        public async Task<bool> CreateAsync(Frequently entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _frequentlyRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Frequently entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _frequentlyRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _frequentlyRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<Frequently> GetAllForSitemap()
        {
            try
            {
                return _frequentlyRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Frequently>().AsQueryable();
            }
        }

        public IQueryable<Frequently> GetAllAsync()
        {
            try
            {
                var data = _frequentlyRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Frequently>().AsQueryable();
            }
        }

        public IQueryable<Frequently> GetAllForAdminAsync()
        {
            try
            {
                var data = _frequentlyRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Frequently>().AsQueryable();
            }
        }

        public async Task<Frequently> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _frequentlyRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _frequentlyRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _frequentlyRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _frequentlyRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _frequentlyRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(Frequently entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _frequentlyRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public IQueryable<Frequently> GetAllFrequentlyForPublic()
        {
            try
            {
                return _frequentlyRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderBy(i => Guid.NewGuid()).OrderBy(i => i.CreatedDate).Take(5);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Frequently>().AsQueryable();
            }
        }

        public async Task<IEnumerable<Frequently>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _frequentlyRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Frequently>();
            }
        }
    }
}
