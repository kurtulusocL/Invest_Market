using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class VisibilitySettingManager : IVisibilitySettingService
    {
        readonly IVisibilitySettingRepository _visibilitySettingRepository;
        readonly ICompanyFinanceRepository _companyFinanceRepository;
        readonly ICompanyPintechRepository _companyPintechRepository;
        readonly ICompanyStageRepository _companyStageRepository;
        public VisibilitySettingManager(IVisibilitySettingRepository visibilitySettingRepository, ICompanyFinanceRepository companyFinanceRepository, ICompanyPintechRepository companyPintechRepository, ICompanyStageRepository companyStageRepository)
        {
            _visibilitySettingRepository = visibilitySettingRepository;
            _companyFinanceRepository = companyFinanceRepository;
            _companyPintechRepository = companyPintechRepository;
            _companyStageRepository = companyStageRepository;
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _visibilitySettingRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(VisibilitySetting entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _visibilitySettingRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _visibilitySettingRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<VisibilitySetting> GetAllIncludingAsync()
        {
            try
            {
                var data =  _visibilitySettingRepository.GetAllInclude(new Expression<Func<VisibilitySetting, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<VisibilitySetting>().AsQueryable();
            }
        }

        public IQueryable<VisibilitySetting> GetAllIncludingByCompanyFinanceIdAsync(int? companyFinanceId)
        {
            try
            {
                if (companyFinanceId == null)
                    throw new ArgumentNullException(nameof(companyFinanceId), "companyFinanceId was null");

                var data =  _visibilitySettingRepository.GetAllIncludeById(companyFinanceId, "CompanyFinanceId", new Expression<Func<VisibilitySetting, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<VisibilitySetting>().AsQueryable();
            }
        }

        public IQueryable<VisibilitySetting> GetAllIncludingByCompanyPintechIdAsync(int? companyPintechId)
        {
            try
            {
                if (companyPintechId == null)
                    throw new ArgumentNullException(nameof(companyPintechId), "companyPintechId was null");

                var data =  _visibilitySettingRepository.GetAllIncludeById(companyPintechId, "CompanyPintechId", new Expression<Func<VisibilitySetting, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<VisibilitySetting>().AsQueryable();
            }
        }

        public IQueryable<VisibilitySetting> GetAllIncludingByCompanyStageIdAsync(int? companyStageId)
        {
            try
            {
                if (companyStageId == null)
                    throw new ArgumentNullException(nameof(companyStageId), "companyStageId was null");

                var data =  _visibilitySettingRepository.GetAllIncludeById(companyStageId, "CompanyStageId", new Expression<Func<VisibilitySetting, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<VisibilitySetting>().AsQueryable();
            }
        }

        public async Task<VisibilitySetting> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _visibilitySettingRepository.GetIncludeAsync(i => i.Id == id, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public IQueryable<VisibilitySetting> GetAllIncludingByLastUpdateDateAsync()
        {
            try
            {
                var data =  _visibilitySettingRepository.GetAllInclude(new Expression<Func<VisibilitySetting, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.UpdatedDate!=null
                }, null, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage);
                return data.OrderByDescending(i => i.UpdatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<VisibilitySetting>().AsQueryable();
            }
        }

        public IQueryable<VisibilitySetting> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data =  _visibilitySettingRepository.GetAllInclude(new Expression<Func<VisibilitySetting, bool>>[]
                {

                }, null, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<VisibilitySetting>().AsQueryable();
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _visibilitySettingRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _visibilitySettingRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _visibilitySettingRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _visibilitySettingRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateCompanyFinanceVisibilityAsync(bool isVisibleForCompanies, bool isVisibleForInvestors, bool isVisibleForAll, bool isVisibleForNone, int? companyFinanceId)
        {
            try
            {
                if (companyFinanceId == null)
                    throw new ArgumentNullException(nameof(companyFinanceId), "companyFinanceId was null");

                var financeExists = await _companyFinanceRepository.GetIncludeAsync(f => f.Id == companyFinanceId, y => y.VisibilitySetting);
                if (financeExists == null)
                    throw new Exception($"CompanyFinance with ID {companyFinanceId} not found");

                if (financeExists.VisibilitySettingId != null)
                {
                    return await UpdateCompanyFinanceVisibilityAsync(isVisibleForCompanies, isVisibleForInvestors, isVisibleForAll, isVisibleForNone, companyFinanceId);
                }
                var entity = new VisibilitySetting
                {
                    CompanyFinanceId = companyFinanceId,
                    IsVisibleForCompanies = isVisibleForCompanies,
                    IsVisibleForAll = isVisibleForAll,
                    IsVisibleForInvestors = isVisibleForInvestors,
                    IsVisibleForNone = isVisibleForNone
                };

                var result = await _visibilitySettingRepository.AddAsync(entity);                
                if (result && entity.Id > 0)
                {
                    financeExists.VisibilitySettingId = entity.Id;
                    await _companyFinanceRepository.UpdateAsync(financeExists);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while creating the entity.", ex);
            }
        }

        public async Task<bool> UpdateCompanyPintechVisibilityAsync(bool isVisibleForCompanies, bool isVisibleForInvestors, bool isVisibleForAll, bool isVisibleForNone, int? companyPintechId)
        {
            try
            {
                if (companyPintechId == null)
                    throw new ArgumentNullException(nameof(companyPintechId), "companyPintechId was null");

                var pintechExists = await _companyPintechRepository.GetIncludeAsync(f => f.Id == companyPintechId, y => y.VisibilitySetting);
                if (pintechExists == null)
                    throw new Exception($"CompanyPintech with ID {companyPintechId} not found");
                if (pintechExists.VisibilitySettingId != null)
                {
                    return await UpdateCompanyPintechVisibilityAsync(isVisibleForCompanies, isVisibleForInvestors, isVisibleForAll, isVisibleForNone, companyPintechId);
                }

                var entity = new VisibilitySetting
                {
                    CompanyPintechId = companyPintechId,
                    IsVisibleForCompanies = isVisibleForCompanies,
                    IsVisibleForAll = isVisibleForAll,
                    IsVisibleForInvestors = isVisibleForInvestors,
                    IsVisibleForNone = isVisibleForNone
                };

                var result = await _visibilitySettingRepository.AddAsync(entity);
                if (result && entity.Id > 0)
                {
                    pintechExists.VisibilitySettingId = entity.Id;
                    await _companyPintechRepository.UpdateAsync(pintechExists);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while creating the entity.", ex);
            }
        }

        public async Task<bool> UpdateCompanyStageVisibilityAsync(bool isVisibleForCompanies, bool isVisibleForInvestors, bool isVisibleForAll, bool isVisibleForNone, int? companyStageId)
        {
            try
            {
                if (companyStageId == null)
                    throw new ArgumentNullException(nameof(companyStageId), "companyStageId was null");

                var stageExists = await _companyStageRepository.GetIncludeAsync(f => f.Id == companyStageId, y => y.VisibilitySetting);
                if (stageExists == null)
                    throw new Exception($"CompanyStage with ID {companyStageId} not found");                
                
                if (stageExists.VisibilitySettingId != null)
                {
                    return await UpdateCompanyStageVisibilityAsync(isVisibleForCompanies, isVisibleForInvestors, isVisibleForAll, isVisibleForNone, companyStageId);
                }

                var entity = new VisibilitySetting
                {
                    CompanyStageId = companyStageId,
                    IsVisibleForCompanies = isVisibleForCompanies,
                    IsVisibleForAll = isVisibleForAll,
                    IsVisibleForInvestors = isVisibleForInvestors,
                    IsVisibleForNone = isVisibleForNone
                };
                var result = await _visibilitySettingRepository.AddAsync(entity);
                if (result && entity.Id > 0)
                {
                    stageExists.VisibilitySettingId = entity.Id;
                    await _companyStageRepository.UpdateAsync(stageExists);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while creating the entity.", ex);
            }
        }

        public async Task<IEnumerable<VisibilitySetting>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _visibilitySettingRepository.GetAllIncludeAsync(new Expression<Func<VisibilitySetting, bool>>[]
                {

                }, null, y => y.CompanyFinance, y => y.CompanyPintech, y => y.CompanyStage);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<VisibilitySetting>();
            }
        }
    }
}
