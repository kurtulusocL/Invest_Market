using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class SubSectorManager : ISubSectorService
    {
        readonly ISubSectorRepository _subSectorRepository;
        public SubSectorManager(ISubSectorRepository sectorRepository)
        {
            _subSectorRepository = sectorRepository;
        }

        public async Task<bool> CreateAsync(SubSector entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _subSectorRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(SubSector entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _subSectorRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _subSectorRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<SubSector>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _subSectorRepository.GetAllIncludeAsync(new Expression<Func<SubSector, bool>>[]
                {
                   
                }, null, y => y.Sector, y => y.Companies, y => y.RecentlyInvests);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SubSector>();
            }
        }

        public IQueryable<SubSector> GetAllForSitemap()
        {
            try
            {
                return _subSectorRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public IQueryable<SubSector> GetAllIncludingAsync()
        {
            try
            {
                var data = _subSectorRepository.GetAllInclude(new Expression<Func<SubSector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Sector, y => y.Companies, y => y.RecentlyInvests);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public IQueryable<SubSector> GetAllIncludingByCompanyQuantityAsync()
        {
            try
            {
                var data = _subSectorRepository.GetAllInclude(new Expression<Func<SubSector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Sector, y => y.Companies, y => y.RecentlyInvests);
                return data.OrderByDescending(i => i.Companies.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public IQueryable<SubSector> GetAllIncludingByRecentlyInvestQuantityAsync()
        {
            try
            {
                var data = _subSectorRepository.GetAllInclude(new Expression<Func<SubSector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Sector, y => y.Companies, y => y.RecentlyInvests);
                return data.OrderByDescending(i => i.RecentlyInvests.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public IQueryable<SubSector> GetAllIncludingBySectorIdAsync(int? sectorId)
        {
            try
            {
                if (sectorId == null)
                    throw new ArgumentNullException(nameof(sectorId), "sectorId was null");

                var data = _subSectorRepository.GetAllIncludeById(sectorId, "SectorId", new Expression<Func<SubSector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Sector, y => y.Companies, y => y.RecentlyInvests);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public IQueryable<SubSector> GetAllIncludingCompanySubsectors()
        {
            try
            {
                return _subSectorRepository.GetAllInclude(new Expression<Func<SubSector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null, y => y.Companies).OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public IQueryable<SubSector> GetAllIncludingForAddCompanyBySectorIdAsync(int? sectorId)
        {
            try
            {
                if (sectorId == null)
                    throw new ArgumentNullException(nameof(sectorId), "sectorId was null");

                var data = _subSectorRepository.GetAllIncludeById(sectorId, "SectorId", new Expression<Func<SubSector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Sector, y => y.Companies, y => y.RecentlyInvests);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public IQueryable<SubSector> GetAllIncludingForAddRecentlyInvestBySectorIdAsync(int? sectorId)
        {
            try
            {
                if (sectorId == null)
                    throw new ArgumentNullException(nameof(sectorId), "sectorId was null");

                var data = _subSectorRepository.GetAllIncludeById(sectorId, "SectorId", new Expression<Func<SubSector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Sector, y => y.Companies, y => y.RecentlyInvests);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public IQueryable<SubSector> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _subSectorRepository.GetAllInclude(new Expression<Func<SubSector, bool>>[]
                {

                }, null, y => y.Sector, y => y.Companies, y => y.RecentlyInvests);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public IQueryable<SubSector> GetAllIncludingForAdminHome()
        {
            try
            {
                return _subSectorRepository.GetAllInclude(new Expression<Func<SubSector, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies, y => y.RecentlyInvests).OrderByDescending(i => i.Companies.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<SubSector>().AsQueryable();
            }
        }

        public async Task<SubSector> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _subSectorRepository.GetIncludeAsync(i => i.Id == id, y => y.Sector, y => y.Companies, y => y.RecentlyInvests);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _subSectorRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _subSectorRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _subSectorRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _subSectorRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(SubSector entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _subSectorRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
