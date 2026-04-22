using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface ILogoService
    {
        IQueryable<Logo> GetAllAsync();
        IQueryable<Logo> GetAllForAdminAsync();
        Task<IEnumerable<Logo>> GetAllForSignalRAsync();
        Task<Logo> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Logo entity, IFormFile image);
        Task<bool> UpdateAsync(Logo entity, IFormFile image);
        Task<bool> DeleteAsync(Logo entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Logo> GetAllForAdmin();
        IQueryable<Logo> GetAllLogo();
        IQueryable<Logo> GetAllIconLogo();
    }
}
