using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class LayoutInfoManager : ILayoutInfoService
    {
        readonly ILayoutInfoRepository _layoutInfoRepository;
        public LayoutInfoManager(ILayoutInfoRepository layoutInfoRepository)
        {
            _layoutInfoRepository = layoutInfoRepository;
        }

        public async Task<bool> CreateAsync(LayoutInfo entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _layoutInfoRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(LayoutInfo entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _layoutInfoRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _layoutInfoRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<LayoutInfo> GetAllAsync()
        {
            try
            {
                var data = _layoutInfoRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<LayoutInfo>().AsQueryable();
            }
        }

        public IQueryable<LayoutInfo> GetAllForAdminAsync()
        {
            try
            {
                var data = _layoutInfoRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<LayoutInfo>().AsQueryable();
            }
        }

        public IQueryable<LayoutInfo> GetAllForShared()
        {
            try
            {
                return _layoutInfoRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).Take(1);
            }
            catch (Exception)
            {
                return Enumerable.Empty<LayoutInfo>().AsQueryable();
            }
        }

        public async Task<IEnumerable<LayoutInfo>> GetAllForSignalRAsync()
        {
            try
            {
                var data =await _layoutInfoRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<LayoutInfo>();
            }
        }

        public IQueryable<LayoutInfo> GetAllForSitemap()
        {
            try
            {
                return _layoutInfoRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<LayoutInfo>().AsQueryable();
            }
        }

        public async Task<LayoutInfo> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _layoutInfoRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _layoutInfoRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _layoutInfoRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _layoutInfoRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _layoutInfoRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(LayoutInfo entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _layoutInfoRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
