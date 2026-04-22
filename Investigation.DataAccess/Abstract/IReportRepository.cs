using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface IReportRepository : IEntityRepository<Report>
    {
        int ReportCounter();
        Task<bool> SetFixedReportAsync(int id);
        Task<bool> SetNotFixedReportAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
