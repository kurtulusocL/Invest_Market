using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class SectorManager : ISectorService
    {
        readonly ISectorRepository _sectorRepository;
        public SectorManager(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<bool> CreateAsync(Sector entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _sectorRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Sector entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _sectorRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _sectorRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Sector>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _sectorRepository.GetAllIncludeAsync(new Expression<Func<Sector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.RecentlyInvests, y => y.SubSectors);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Sector>();
            }
        }

        public IQueryable<Sector> GetAllForSitemap()
        {
            try
            {
                return _sectorRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllIncludingAsync()
        {
            try
            {
                var data = _sectorRepository.GetAllInclude(new Expression<Func<Sector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.RecentlyInvests, y => y.SubSectors);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllIncludingByCompanyQuantityAsync()
        {
            try
            {
                var data = _sectorRepository.GetAllInclude(new Expression<Func<Sector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.RecentlyInvests, y => y.SubSectors);
                return data.OrderByDescending(i => i.Companies.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllIncludingByRecentlyInvestQuantityAsync()
        {
            try
            {
                var data = _sectorRepository.GetAllInclude(new Expression<Func<Sector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.RecentlyInvests, y => y.SubSectors);
                return data.OrderByDescending(i => i.RecentlyInvests.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllIncludingCompanySectors()
        {
            try
            {
                return _sectorRepository.GetAllInclude(new Expression<Func<Sector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null, y => y.Companies).OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllIncludingForAddCompanyAsync()
        {
            try
            {
                var data = _sectorRepository.GetAllInclude(new Expression<Func<Sector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllIncludingForAddRecentlyInvestAsync()
        {
            try
            {
                var data = _sectorRepository.GetAllInclude(new Expression<Func<Sector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.RecentlyInvests, y => y.SubSectors);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllIncludingForAddSubsectorAsync()
        {
            try
            {
                var data = _sectorRepository.GetAllInclude(new Expression<Func<Sector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.SubSectors);
                return data.OrderByDescending(i => i.SubSectors.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _sectorRepository.GetAllInclude(new Expression<Func<Sector, bool>>[]
                {

                }, null, y => y.Companies, y => y.RecentlyInvests, y => y.SubSectors);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllIncludingForAdminHome()
        {
            try
            {
                return _sectorRepository.GetAllInclude(new Expression<Func<Sector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.RecentlyInvests, y => y.SubSectors).OrderByDescending(i => i.Companies.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public IQueryable<Sector> GetAllSectorsForCompanySearch()
        {
            try
            {
                return _sectorRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.Companies.Count() > 0).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Sector>().AsQueryable();
            }
        }

        public async Task<Sector> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _sectorRepository.GetIncludeAsync(i => i.Id == id, y => y.Companies, y => y.RecentlyInvests, y => y.SubSectors);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _sectorRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _sectorRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _sectorRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _sectorRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(Sector entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _sectorRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
