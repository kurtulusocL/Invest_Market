using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IInvestorCategoryService
    {
        IQueryable<InvestorCategory> GetAllIncludingAsync();
        IQueryable<InvestorCategory> GetAllIncludingForAddInvestorAsync();
        IQueryable<InvestorCategory> GetAllIncludingByInvestorQuantityAsync();
        IQueryable<InvestorCategory> GetAllIncludingForAdminAsync();
        Task<IEnumerable<InvestorCategory>> GetAllForSignalRAsync();
        Task<InvestorCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(InvestorCategory entity);
        Task<bool> UpdateAsync(InvestorCategory entity);
        Task<bool> DeleteAsync(InvestorCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<InvestorCategory> GetAllForSitemap();
        IQueryable<InvestorCategory> GetAllIncludingForAdminHome();
        IQueryable<InvestorCategory> GetAllIncludingInvestorCategories();
        IQueryable<InvestorCategory> GetAllInvestorCategoriesForSearch();
    }
}
