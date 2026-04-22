using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISectorService
    {
        IQueryable<Sector> GetAllIncludingAsync();
        IQueryable<Sector> GetAllIncludingForAddSubsectorAsync();
        IQueryable<Sector> GetAllIncludingForAddCompanyAsync();
        IQueryable<Sector> GetAllIncludingForAddRecentlyInvestAsync();
        IQueryable<Sector> GetAllIncludingByCompanyQuantityAsync();
        IQueryable<Sector> GetAllIncludingByRecentlyInvestQuantityAsync();
        IQueryable<Sector> GetAllIncludingForAdminAsync();
        Task<IEnumerable<Sector>> GetAllForSignalRAsync();
        Task<Sector> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Sector entity);
        Task<bool> UpdateAsync(Sector entity);
        Task<bool> DeleteAsync(Sector entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Sector> GetAllForSitemap();
        IQueryable<Sector> GetAllIncludingForAdminHome();
        IQueryable<Sector> GetAllIncludingCompanySectors();
        IQueryable<Sector> GetAllSectorsForCompanySearch();
    }
}
