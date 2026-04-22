using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICompanyPintechService
    {
        IQueryable<CompanyPintech> GetAllIncludingAsync();
        IQueryable<CompanyPintech> GetAllIncludingByVisibilitySettingIdAsync(int? visibilitySettingId);
        IQueryable<CompanyPintech> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<CompanyPintech> GetAllIncludingForAdminAsync();
        IQueryable<CompanyPintech> GetAllIncludingCompanyPintechForCompanyByCompanyIdAsync(int? companyId);
        Task<IEnumerable<CompanyPintech>> GetAllForSignalRAsync();
        Task<CompanyPintech> GetByIdAsync(int? id);
        Task<CompanyPintech> GetCompanyPintechByCompanyIdAsync(int? companyId);
        Task<bool> CreateAsync(string workPlan, string serviceProduct, string description, string marketingStrategy, string growingPotantial, int? companyId);
        Task<bool> UpdateAsync(string workPlan, string serviceProduct, string description, string marketingStrategy, string growingPotantial, int? companyId, int id);
        Task<bool> DeleteAsync(CompanyPintech entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        CompanyPintech GetCompanyPintechForCompanyDetailByCompanyId(int? companyId);
    }
}
