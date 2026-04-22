using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class InvestorCategoryManager : IInvestorCategoryService
    {
        readonly IInvestorCategoryRepository _investorCategoryRepository;
        public InvestorCategoryManager(IInvestorCategoryRepository investorCategoryRepository)
        {
            _investorCategoryRepository = investorCategoryRepository;
        }

        public async Task<bool> CreateAsync(InvestorCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _investorCategoryRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(InvestorCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _investorCategoryRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _investorCategoryRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<InvestorCategory>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _investorCategoryRepository.GetAllIncludeAsync(new Expression<Func<InvestorCategory, bool>>[]
                {
                    
                }, null, y => y.Investors);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<InvestorCategory>();
            }
        }

        public IQueryable<InvestorCategory> GetAllForSitemap()
        {
            try
            {
                return _investorCategoryRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<InvestorCategory>().AsQueryable();
            }
        }

        public IQueryable<InvestorCategory> GetAllIncludingAsync()
        {
            try
            {
                var data = _investorCategoryRepository.GetAllInclude(new Expression<Func<InvestorCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Investors);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<InvestorCategory>().AsQueryable();
            }
        }

        public IQueryable<InvestorCategory> GetAllIncludingByInvestorQuantityAsync()
        {
            try
            {
                var data = _investorCategoryRepository.GetAllInclude(new Expression<Func<InvestorCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Investors);
                return data.OrderByDescending(i => i.Investors.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<InvestorCategory>().AsQueryable();
            }
        }

        public IQueryable<InvestorCategory> GetAllIncludingForAddInvestorAsync()
        {
            try
            {
                var data = _investorCategoryRepository.GetAllInclude(new Expression<Func<InvestorCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Investors);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<InvestorCategory>().AsQueryable();
            }
        }

        public IQueryable<InvestorCategory> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _investorCategoryRepository.GetAllInclude(new Expression<Func<InvestorCategory, bool>>[]
                {

                }, null, y => y.Investors);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<InvestorCategory>().AsQueryable();
            }
        }

        public IQueryable<InvestorCategory> GetAllIncludingForAdminHome()
        {
            try
            {
                return _investorCategoryRepository.GetAllInclude(new Expression<Func<InvestorCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Investors).OrderByDescending(i => i.Investors.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<InvestorCategory>().AsQueryable();
            }
        }

        public IQueryable<InvestorCategory> GetAllIncludingInvestorCategories()
        {
            try
            {
                return _investorCategoryRepository.GetAllInclude(new Expression<Func<InvestorCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Investors.Count()>0
                }, null, y => y.Investors).OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<InvestorCategory>().AsQueryable();
            }
        }

        public IQueryable<InvestorCategory> GetAllInvestorCategoriesForSearch()
        {
            try
            {
                return _investorCategoryRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.Investors.Count() > 0).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<InvestorCategory>().AsQueryable();
            }
        }

        public async Task<InvestorCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _investorCategoryRepository.GetIncludeAsync(i => i.Id == id, y => y.Investors);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _investorCategoryRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _investorCategoryRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _investorCategoryRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _investorCategoryRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(InvestorCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _investorCategoryRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
