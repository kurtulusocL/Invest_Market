using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface ISectorNewsRepository : IEntityRepository<SectorNews>
    {
        Task<SectorNews?> GetBySlugAsync(string slug);
        SectorNews HitRead(int id);
        Task<bool> LikeAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
