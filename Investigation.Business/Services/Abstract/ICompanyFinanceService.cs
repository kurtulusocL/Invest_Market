using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICompanyFinanceService
    {
        IQueryable<CompanyFinance> GetAllIncludingAsync();
        IQueryable<CompanyFinance> GetAllIncludingByTotalIncomeAsync();
        IQueryable<CompanyFinance> GetAllIncludingByVisilibilitySettingIdAsync(int? visibilitySettingId);
        IQueryable<CompanyFinance> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<CompanyFinance> GetAllIncludingForAdminAsync();
        IQueryable<CompanyFinance> GetAllIncludingCompanyFinanceForCompanyByCompanyIdAsync(int? companyId);
        Task<IEnumerable<CompanyFinance>> GetAllForSignalRAsync();
        Task<CompanyFinance> GetByIdAsync(int? id);
        Task<CompanyFinance> GetCopanyFinanceByCompanyIdAsync(int? companyId);        
        Task<bool> CreateAsync(decimal? marketvalue, decimal? arrIncome, decimal totalIncome, int? companyId);
        Task<bool> UpdateAsync(decimal? marketvalue, decimal? arrIncome, decimal totalIncome, int? companyId, int id);
        Task<bool> DeleteAsync(CompanyFinance entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        CompanyFinance GetCompanyFinanceForCompanyDetailByCompanyId(int? companyId);
    }
}
