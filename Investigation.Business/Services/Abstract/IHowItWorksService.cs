using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IHowItWorksService
    {
        IQueryable<HowItWorks> GetAllAsync();
        IQueryable<HowItWorks> GetAllForAdminAsync();
        Task<IEnumerable<HowItWorks>> GetAllForSignalRAsync();
        Task<HowItWorks> GetByIdAsync(int? id);
        Task<bool> CreateAsync(HowItWorks entity);
        Task<bool> UpdateAsync(HowItWorks entity);
        Task<bool> DeleteAsync(HowItWorks entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<HowItWorks> GetAllHowItWorksForPublic();
        IQueryable<HowItWorks> GetAllForSitemap();
    }
}
