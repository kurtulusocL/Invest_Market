using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IVisibilitySettingService
    {
        IQueryable<VisibilitySetting> GetAllIncludingAsync();
        IQueryable<VisibilitySetting> GetAllIncludingByCompanyFinanceIdAsync(int? companyFinanceId);
        IQueryable<VisibilitySetting> GetAllIncludingByCompanyPintechIdAsync(int? companyPintechId);
        IQueryable<VisibilitySetting> GetAllIncludingByCompanyStageIdAsync(int? companyStageId);
        IQueryable<VisibilitySetting> GetAllIncludingByLastUpdateDateAsync();
        IQueryable<VisibilitySetting> GetAllIncludingForAdminAsync();
        Task<IEnumerable<VisibilitySetting>> GetAllForSignalRAsync();
        Task<VisibilitySetting> GetByIdAsync(int? id);
        Task<bool> UpdateCompanyFinanceVisibilityAsync(bool isVisibleForCompanies, bool isVisibleForInvestors, bool isVisibleForAll, bool isVisibleForNone, int? companyFinanceId);
        Task<bool> UpdateCompanyPintechVisibilityAsync(bool isVisibleForCompanies, bool isVisibleForInvestors, bool isVisibleForAll, bool isVisibleForNone, int? companyPintechId);
        Task<bool> UpdateCompanyStageVisibilityAsync(bool isVisibleForCompanies, bool isVisibleForInvestors, bool isVisibleForAll, bool isVisibleForNone, int? companyStageId);
        Task<bool> DeleteAsync(VisibilitySetting entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
