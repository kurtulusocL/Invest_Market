using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICompanyContactService
    {
        IQueryable<CompanyContact> GetAllIncludingAsync();
        IQueryable<CompanyContact> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<CompanyContact> GetAllIncludingForAdminAsync();
        IQueryable<CompanyContact> GetAllIncludingCompanyContactByCompanyIdAsync(int? companyId);
        Task<IEnumerable<CompanyContact>> GetAllForSignalRAsync();
        Task<CompanyContact> GetByIdAsync(int? id);
        Task<CompanyContact> GetCompanyContactByCompanyIdAsync(int? companyId);
        Task<bool> CreateAsync(string website, string? phoneNumber, string email, string location, int? companyId);
        Task<bool> UpdateAsync(string website, string? phoneNumber, string email, string location, int? companyId, int id);
        Task<bool> DeleteAsync(CompanyContact entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        CompanyContact GetCompanyContactByCompanyId(int? companyId);
    }
}
