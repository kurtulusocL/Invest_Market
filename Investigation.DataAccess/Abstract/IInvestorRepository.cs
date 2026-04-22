using Investigation.Domain.Entities;
using Investigation.Shared.DataAccess;

namespace Investigation.DataAccess.Abstract
{
    public interface IInvestorRepository : IEntityRepository<Investor>
    {
        Task<Investor?> GetBySlugAsync(string slug);
        Task<IEnumerable<Investor>> GetAllIncludingMostPopularInvestorsAsync();
        Task<IEnumerable<Investor>> GetAllIncludingUnPopularInvestorsAsync();
        IEnumerable<Investor> GetAllIncludingMostPopularInvestors();
        int InvestorCounter();
        Task<bool> SetInvestorLookingForCompanyAsync(int id);
        Task<bool> SetInvestorNotLookingForCompanyAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
