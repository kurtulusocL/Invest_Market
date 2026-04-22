using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class PersonalDataManager : IPersonalDataService
    {
        readonly IPersonalDataRepository _personalDataRepository;
        public PersonalDataManager(IPersonalDataRepository personalDataRepository)
        {
            _personalDataRepository = personalDataRepository;
        }

        public async Task<bool> CreateAsync(PersonalData entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _personalDataRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(PersonalData entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _personalDataRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _personalDataRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<PersonalData> GetAllAsync()
        {
            try
            {
                var data =  _personalDataRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<PersonalData>().AsQueryable();
            }
        }

        public IQueryable<PersonalData> GetAllForAdminAsync()
        {
            try
            {
                var data =  _personalDataRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<PersonalData>().AsQueryable();
            }
        }

        public async Task<IEnumerable<PersonalData>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _personalDataRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<PersonalData>();
            }
        }

        public IQueryable<PersonalData> GetAllForSitemap()
        {
            try
            {
                return _personalDataRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<PersonalData>().AsQueryable();
            }
        }

        public async Task<PersonalData> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _personalDataRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _personalDataRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _personalDataRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _personalDataRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _personalDataRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(PersonalData entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _personalDataRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
