using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISubSectorService
    {
        IQueryable<SubSector> GetAllIncludingAsync();
        IQueryable<SubSector> GetAllIncludingBySectorIdAsync(int? sectorId);
        IQueryable<SubSector> GetAllIncludingForAddCompanyBySectorIdAsync(int? sectorId);
        IQueryable<SubSector> GetAllIncludingForAddRecentlyInvestBySectorIdAsync(int? sectorId);
        IQueryable<SubSector> GetAllIncludingByCompanyQuantityAsync();
        IQueryable<SubSector> GetAllIncludingByRecentlyInvestQuantityAsync();
        IQueryable<SubSector> GetAllIncludingForAdminAsync();
        Task<IEnumerable<SubSector>> GetAllForSignalRAsync();
        Task<SubSector> GetByIdAsync(int? id);
        Task<bool> CreateAsync(SubSector entity);
        Task<bool> UpdateAsync(SubSector entity);
        Task<bool> DeleteAsync(SubSector entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<SubSector> GetAllForSitemap();
        IQueryable<SubSector> GetAllIncludingForAdminHome();
        IQueryable<SubSector> GetAllIncludingCompanySubsectors();
    }
}
