using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class CompanyCategoryManager : ICompanyCategoryService
    {
        readonly ICompanyCategoryRepository _companyCategoryRepository;
        public CompanyCategoryManager(ICompanyCategoryRepository companyCategoryRepository)
        {
            _companyCategoryRepository = companyCategoryRepository;
        }

        public async Task<bool> CreateAsync(CompanyCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _companyCategoryRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _companyCategoryRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyCategoryRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<CompanyCategory> GetAllCompanyCategoriesForCompanySearch()
        {
            try
            {
                return _companyCategoryRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false && i.Companies.Count() > 0).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyCategory>().AsQueryable();
            }
        }

        public async Task<IEnumerable<CompanyCategory>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _companyCategoryRepository.GetAllIncludeAsync(new Expression<Func<CompanyCategory, bool>>[]
                {
                   
                }, null, y => y.Companies);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyCategory>();
            }
        }

        public IQueryable<CompanyCategory> GetAllForSitemap()
        {
            try
            {
                return _companyCategoryRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyCategory>().AsQueryable();
            }
        }

        public IQueryable<CompanyCategory> GetAllIncludingAsync()
        {
            try
            {
                var data = _companyCategoryRepository.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyCategory>().AsQueryable();
            }
        }

        public IQueryable<CompanyCategory> GetAllIncludingByCompanyCategoryQuantityAsync()
        {
            try
            {
                var data = _companyCategoryRepository.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies);
                return data.OrderByDescending(i => i.Companies.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyCategory>().AsQueryable();
            }
        }

        public IQueryable<CompanyCategory> GetAllIncludingCompanyCategories()
        {
            try
            {
                return _companyCategoryRepository.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Companies.Count()>0
                }, null, y => y.Companies).OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyCategory>().AsQueryable();
            }
        }

        public IQueryable<CompanyCategory> GetAllIncludingForAddCompanyAsync()
        {
            try
            {
                var data = _companyCategoryRepository.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyCategory>().AsQueryable();
            }
        }

        public IQueryable<CompanyCategory> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _companyCategoryRepository.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {

                }, null, y => y.Companies);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyCategory>().AsQueryable();
            }
        }

        public IQueryable<CompanyCategory> GetAllIncludingForAdminHome()
        {
            try
            {
                return _companyCategoryRepository.GetAllInclude(new Expression<Func<CompanyCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Companies).OrderByDescending(i => i.Companies.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyCategory>().AsQueryable();
            }
        }

        public async Task<CompanyCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _companyCategoryRepository.GetIncludeAsync(i => i.Id == id, y => y.Companies);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyCategoryRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyCategoryRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyCategoryRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyCategoryRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(CompanyCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _companyCategoryRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
