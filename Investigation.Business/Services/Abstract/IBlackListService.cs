using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IBlackListService
    {
        IQueryable<BlackList> GetAllIncludingAsync();
        IQueryable<BlackList> GetAllIncludingByExpirationDateAsync();
        IQueryable<BlackList> GetAllIncludingByAuditIdAsync(int? auditId);
        IQueryable<BlackList> GetAllIncludingForAdminAsync();
        Task<IEnumerable<BlackList>> GetAllForSignalRAsync();
        Task<BlackList> GetByIdAsync(int? id);
        Task<bool> CreateAsync(string remoteIpAddress, string ipAddressWithVPN, string? deviceFingerprint, string localIpAddress, DateTime expirationDate, int? auditId);
        Task<bool> UpdateAsync(string remoteIpAddress, string ipAddressWithVPN, string? deviceFingerprint, string localIpAddress, DateTime expirationDate, int? auditId, int id);
        Task<bool> DeleteAsync(BlackList entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
