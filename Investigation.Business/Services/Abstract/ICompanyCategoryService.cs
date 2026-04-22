using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICompanyCategoryService
    {
        IQueryable<CompanyCategory> GetAllIncludingAsync();
        IQueryable<CompanyCategory> GetAllIncludingByCompanyCategoryQuantityAsync();
        IQueryable<CompanyCategory> GetAllIncludingForAddCompanyAsync();
        IQueryable<CompanyCategory> GetAllIncludingForAdminAsync();
        Task<IEnumerable<CompanyCategory>> GetAllForSignalRAsync();
        Task<CompanyCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(CompanyCategory entity);
        Task<bool> UpdateAsync(CompanyCategory entity);
        Task<bool> DeleteAsync(CompanyCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<CompanyCategory> GetAllForSitemap();
        IQueryable<CompanyCategory> GetAllIncludingForAdminHome();
        IQueryable<CompanyCategory> GetAllIncludingCompanyCategories();
        IQueryable<CompanyCategory> GetAllCompanyCategoriesForCompanySearch();
    }
}
