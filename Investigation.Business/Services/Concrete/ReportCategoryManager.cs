using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class ReportCategoryManager : IReportCategoryService
    {
        readonly IReportCategoryRepository _reportCategoryRepository;
        public ReportCategoryManager(IReportCategoryRepository reportCategoryRepository)
        {
            _reportCategoryRepository = reportCategoryRepository;
        }

        public async Task<bool> CreateAsync(ReportCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var result = await _reportCategoryRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(ReportCategory entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _reportCategoryRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _reportCategoryRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<ReportCategory>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _reportCategoryRepository.GetAllIncludeAsync(new Expression<Func<ReportCategory, bool>>[]
                {
                   
                }, null, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<ReportCategory>();
            }
        }

        public IQueryable<ReportCategory> GetAllIncludingAsync()
        {
            try
            {
                var data = _reportCategoryRepository.GetAllInclude(new Expression<Func<ReportCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ReportCategory>().AsQueryable();
            }
        }

        public IQueryable<ReportCategory> GetAllIncludingByReportQuantityAsync()
        {
            try
            {
                var data = _reportCategoryRepository.GetAllInclude(new Expression<Func<ReportCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports);
                return data.OrderByDescending(i => i.Reports.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<ReportCategory>().AsQueryable();
            }
        }

        public IQueryable<ReportCategory> GetAllIncludingForAddReportAsync()
        {
            try
            {
                var data = _reportCategoryRepository.GetAllInclude(new Expression<Func<ReportCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports);
                return data.OrderBy(i => i.Name);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ReportCategory>().AsQueryable();
            }
        }

        public IQueryable<ReportCategory> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _reportCategoryRepository.GetAllInclude(new Expression<Func<ReportCategory, bool>>[]
                {

                }, null, y => y.Reports);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<ReportCategory>().AsQueryable();
            }
        }

        public IQueryable<ReportCategory> GetAllIncludingForAdminHome()
        {
            try
            {
                return _reportCategoryRepository.GetAllInclude(new Expression<Func<ReportCategory, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Reports).OrderByDescending(i => i.Reports.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<ReportCategory>().AsQueryable();
            }
        }

        public async Task<ReportCategory> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _reportCategoryRepository.GetIncludeAsync(i => i.Id == id, y => y.Reports);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _reportCategoryRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _reportCategoryRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _reportCategoryRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _reportCategoryRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(ReportCategory entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                entity.UpdatedDate = DateTime.UtcNow;
                var result = await _reportCategoryRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
