using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Dtos.UserDto;

namespace Investigation.Business.Services.Concrete
{
    public class RoleManager : IRoleService
    {
        readonly IRoleRepository _roleRepository;
        public RoleManager(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> CreateAsync(AppRole entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _roleRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(AppRole entity, string id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _roleRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _roleRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<AppRole> GetAllAsync()
        {
            try
            {
                var data = _roleRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppRole>().AsQueryable();
            }
        }

        public IQueryable<AppRole> GetAllForAdminAsync()
        {
            try
            {
                var data = _roleRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<AppRole>().AsQueryable();
            }
        }

        public async Task<IEnumerable<AppRole>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _roleRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<AppRole>();
            }
        }

        public async Task<IEnumerable<RoleUserCountDto>> GetAllUserCountsByRoleAsync()
        {
            try
            {
                return await _roleRepository.GetAllUserCountsByRoleAsync();
            }
            catch (Exception)
            {
                return new List<RoleUserCountDto>();
            }
        }

        public async Task<AppRole> GetByIdAsync(string id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _roleRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(string id)
        {
            var result = await _roleRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(string id)
        {
            var result = await _roleRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(string id)
        {
            var result = await _roleRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(string id)
        {
            var result = await _roleRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(AppRole entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _roleRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
