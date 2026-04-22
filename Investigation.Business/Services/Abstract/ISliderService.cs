using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface ISliderService
    {
        IQueryable<Slider> GetAllAsync();
        IQueryable<Slider> GetAllForAdminAsync();
        Task<IEnumerable<Slider>> GetAllForSignalRAsync();
        Task<Slider> GetByIdAsync(int? id);
        Task<bool> CreateAsync(Slider entity, IFormFile image);
        Task<bool> UpdateAsync(Slider entity, IFormFile image);
        Task<bool> DeleteAsync(Slider entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Slider> GetAllSliderRandom();
        IQueryable<Slider> GetAllForSitemap();        
    }
}
