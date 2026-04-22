using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IReportCategoryService
    {
        IQueryable<ReportCategory> GetAllIncludingAsync();
        IQueryable<ReportCategory> GetAllIncludingByReportQuantityAsync();
        IQueryable<ReportCategory> GetAllIncludingForAddReportAsync();
        IQueryable<ReportCategory> GetAllIncludingForAdminAsync();
        Task<IEnumerable<ReportCategory>> GetAllForSignalRAsync();
        Task<ReportCategory> GetByIdAsync(int? id);
        Task<bool> CreateAsync(ReportCategory entity);
        Task<bool> UpdateAsync(ReportCategory entity);
        Task<bool> DeleteAsync(ReportCategory entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<ReportCategory> GetAllIncludingForAdminHome();
    }
}
