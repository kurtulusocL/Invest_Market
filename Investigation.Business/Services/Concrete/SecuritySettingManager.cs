using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class SecuritySettingManager : ISecuritySettingService
    {
        readonly ISecuritySettingRepository _securitySettingRepository;
        public SecuritySettingManager(ISecuritySettingRepository securitySettingRepository)
        {
            _securitySettingRepository = securitySettingRepository;
        }

        public async Task<bool> CreateAsync(SecuritySetting entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _securitySettingRepository.AddAsync(entity);
                return result;
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

                var result = await _securitySettingRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(SecuritySetting entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _securitySettingRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _securitySettingRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<SecuritySetting> GetAllAsync()
        {
            try
            {
                var data = _securitySettingRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SecuritySetting>().AsQueryable();
            }
        }

        public IQueryable<SecuritySetting> GetAllByBlockedAgentAsync()
        {
            try
            {
                var data = _securitySettingRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.Type == "BlockedAgent");
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SecuritySetting>().AsQueryable();
            }
        }

        public IQueryable<SecuritySetting> GetAllByStaticExtensionsAsync()
        {
            try
            {
                var data = _securitySettingRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.Type == "StaticExtension");
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SecuritySetting>().AsQueryable();
            }
        }

        public IQueryable<SecuritySetting> GetAllForAdminAsync()
        {
            try
            {
                var data = _securitySettingRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SecuritySetting>().AsQueryable();
            }
        }

        public async Task<IEnumerable<SecuritySetting>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _securitySettingRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SecuritySetting>();
            }
        }

        public async Task<SecuritySetting> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _securitySettingRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _securitySettingRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _securitySettingRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _securitySettingRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _securitySettingRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(SecuritySetting entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.Now;
                var result = await _securitySettingRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
