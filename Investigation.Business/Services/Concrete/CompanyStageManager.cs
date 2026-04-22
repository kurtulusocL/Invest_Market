using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class CompanyStageManager : ICompanyStageService
    {
        readonly ICompanyStageRepository _companyStageRepository;
        public CompanyStageManager(ICompanyStageRepository companyStageRepository)
        {
            _companyStageRepository = companyStageRepository;
        }

        public async Task<bool> CreateAsync(string stageName, decimal stageValue, int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var entity = new CompanyStage
                {
                    StageName = stageName,
                    StageValue = stageValue,
                    CompanyId = companyId
                };
                var result = await _companyStageRepository.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> UpdateAsync(string stageName, decimal stageValue, int? companyId, int id)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var entity = new CompanyStage
                {
                    StageName = stageName,
                    StageValue = stageValue,
                    CompanyId = companyId,
                    Id = id,
                    UpdatedDate = DateTime.UtcNow
                };
                var result = await _companyStageRepository.UpdateAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(CompanyStage entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _companyStageRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _companyStageRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<CompanyStage> GetAllIncludingAsync()
        {
            try
            {
                var data = _companyStageRepository.GetAllInclude(new Expression<Func<CompanyStage, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyStage>().AsQueryable();
            }
        }

        public IQueryable<CompanyStage> GetAllIncludingByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _companyStageRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyStage, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyStage>().AsQueryable();
            }
        }

        public IQueryable<CompanyStage> GetAllIncludingByVisibilitySettingIdAsync(int? visibilitySettingId)
        {
            try
            {
                if (visibilitySettingId == null)
                    throw new ArgumentNullException(nameof(visibilitySettingId), "visibilitySettingId was null");

                var data = _companyStageRepository.GetAllIncludeById(visibilitySettingId, "VisibilitySettingId", new Expression<Func<CompanyStage, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyStage>().AsQueryable();
            }
        }

        public IQueryable<CompanyStage> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _companyStageRepository.GetAllInclude(new Expression<Func<CompanyStage, bool>>[]
                {

                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyStage>().AsQueryable();
            }
        }

        public async Task<CompanyStage> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _companyStageRepository.GetIncludeAsync(i => i.Id == id, y => y.Company, y => y.Hits);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<CompanyStage> GetCompanyStageByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return await _companyStageRepository.GetIncludeAsync(i => i.CompanyId == companyId, y => y.Company, y => y.Hits);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _companyStageRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _companyStageRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _companyStageRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _companyStageRepository.SetNotDeletedAsync(id);
            return result;
        }

        public CompanyStage GetCompanyStageForCompanyDetailByCompanyId(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                return _companyStageRepository.GetInclude(i => i.CompanyId == companyId, y => y.VisibilitySetting, y => y.Hits);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public IQueryable<CompanyStage> GetAllIncludingCompanyStageForCompanyByCompanyIdAsync(int? companyId)
        {
            try
            {
                if (companyId == null)
                    throw new ArgumentNullException(nameof(companyId), "companyId was null");

                var data = _companyStageRepository.GetAllIncludeById(companyId, "CompanyId", new Expression<Func<CompanyStage, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Company.IsActive==true&&i.IsDeleted==false
                }, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<CompanyStage>().AsQueryable();
            }
        }

        public async Task<IEnumerable<CompanyStage>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _companyStageRepository.GetAllIncludeAsync(new Expression<Func<CompanyStage, bool>>[]
                {

                }, null, y => y.Company, y => y.Hits);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<CompanyStage>();
            }
        }
    }
}
