using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class UserAgreementManager : IUserAgreementService
    {
        readonly IUserAgreementRepository _userAgreementRepository;
        public UserAgreementManager(IUserAgreementRepository userAgreementRepository)
        {
            _userAgreementRepository = userAgreementRepository;
        }

        public async Task<bool> CreateAsync(UserAgreement entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _userAgreementRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(UserAgreement entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _userAgreementRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _userAgreementRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<UserAgreement> GetAllAsync()
        {
            try
            {
                var data = _userAgreementRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserAgreement>().AsQueryable();
            }
        }

        public IQueryable<UserAgreement> GetAllForAdminAsync()
        {
            try
            {
                var data = _userAgreementRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserAgreement>().AsQueryable();
            }
        }

        public async Task<IEnumerable<UserAgreement>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _userAgreementRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<UserAgreement>();
            }
        }

        public IQueryable<UserAgreement> GetAllForSitemap()
        {
            try
            {
                return _userAgreementRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<UserAgreement>().AsQueryable();
            }
        }

        public async Task<UserAgreement> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _userAgreementRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _userAgreementRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _userAgreementRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _userAgreementRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _userAgreementRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(UserAgreement entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _userAgreementRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
