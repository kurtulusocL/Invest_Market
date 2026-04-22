using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ICompanyStageService
    {
        IQueryable<CompanyStage> GetAllIncludingAsync();
        IQueryable<CompanyStage> GetAllIncludingByVisibilitySettingIdAsync(int? visibilitySettingId);
        IQueryable<CompanyStage> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<CompanyStage> GetAllIncludingForAdminAsync();
        IQueryable<CompanyStage> GetAllIncludingCompanyStageForCompanyByCompanyIdAsync(int? companyId);
        Task<IEnumerable<CompanyStage>> GetAllForSignalRAsync();
        Task<CompanyStage> GetByIdAsync(int? id);
        Task<CompanyStage> GetCompanyStageByCompanyIdAsync(int? companyId);
        Task<bool> CreateAsync(string stageName, decimal stageValue, int? companyId);
        Task<bool> UpdateAsync(string stageName, decimal stageValue, int? companyId, int id);
        Task<bool> DeleteAsync(CompanyStage entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        CompanyStage GetCompanyStageForCompanyDetailByCompanyId(int? companyId);
    }
}
