using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class HowItWorksManager : IHowItWorksService
    {
        readonly IHowItWorksRepository _howItWorksRepository;
        public HowItWorksManager(IHowItWorksRepository howItWorksRepository)
        {
            _howItWorksRepository = howItWorksRepository;
        }

        public async Task<bool> CreateAsync(HowItWorks entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _howItWorksRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(HowItWorks entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entit was null");

                var data = await _howItWorksRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _howItWorksRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<HowItWorks> GetAllAsync()
        {
            try
            {
                var data = _howItWorksRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<HowItWorks>().AsQueryable();
            }
        }

        public IQueryable<HowItWorks> GetAllForAdminAsync()
        {
            try
            {
                var data = _howItWorksRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<HowItWorks>().AsQueryable();
            }
        }

        public async Task<IEnumerable<HowItWorks>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _howItWorksRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<HowItWorks>();
            }
        }

        public IQueryable<HowItWorks> GetAllForSitemap()
        {
            try
            {
                return _howItWorksRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<HowItWorks>().AsQueryable();
            }
        }

        public IQueryable<HowItWorks> GetAllHowItWorksForPublic()
        {
            try
            {
                return _howItWorksRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderBy(i => i.CreatedDate).OrderBy(i => Guid.NewGuid()).Take(5);
            }
            catch (Exception)
            {
                return Enumerable.Empty<HowItWorks>().AsQueryable();
            }
        }

        public async Task<HowItWorks> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _howItWorksRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _howItWorksRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _howItWorksRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _howItWorksRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _howItWorksRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(HowItWorks entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _howItWorksRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
