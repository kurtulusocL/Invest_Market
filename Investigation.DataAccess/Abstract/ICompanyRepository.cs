using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface ICompanyRepository : IEntityRepository<Company>
    {
        Task<Company?> GetBySlugAsync(string slug);
        Task<IEnumerable<Company>> GetAllIncludingMostPopularCompaniesAsync();
        Task<IEnumerable<Company>> GetAllIncludingUnPopularCompaniesAsync();
        IEnumerable<Company> GetAllIncludingMostPopularCompanies();
        int CompanyCounter();
        Task<bool> SetLookingForInvestAsync(int id);
        Task<bool> SetNotLookingForInvestAsync(int id);
        Task<bool> SetFollowableAsync(int id);
        Task<bool> SetNotFollowableAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
