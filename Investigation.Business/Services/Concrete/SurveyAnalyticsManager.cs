using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class SurveyAnalyticsManager : ISurveyAnalyticsService
    {
        readonly ISurveyAnalyticsRepository _surveyAnalyticsRepository;
        public SurveyAnalyticsManager(ISurveyAnalyticsRepository surveyAnalyticsRepository)
        {
            _surveyAnalyticsRepository = surveyAnalyticsRepository;
        }

        public async Task<bool> DeleteAsync(SurveyAnalytics entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _surveyAnalyticsRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _surveyAnalyticsRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<SurveyAnalytics>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _surveyAnalyticsRepository.GetAllIncludeAsync(new Expression<Func<SurveyAnalytics, bool>>[]
                {
                   
                }, null, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SurveyAnalytics>();
            }
        }

        public IQueryable<SurveyAnalytics> GetAllIncludingAsync()
        {
            try
            {
                var data = _surveyAnalyticsRepository.GetAllInclude(new Expression<Func<SurveyAnalytics, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnalytics>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnalytics> GetAllIncludingByCompletionRateAsync()
        {
            try
            {
                var data = _surveyAnalyticsRepository.GetAllInclude(new Expression<Func<SurveyAnalytics, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Survey);
                return data.OrderByDescending(i => i.CompletionRate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnalytics>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnalytics> GetAllIncludingBySurveyIdAsync(int surveyId)
        {
            try
            {
                var data = _surveyAnalyticsRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<SurveyAnalytics, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnalytics>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnalytics> GetAllIncludingByTotalResponseAsync()
        {
            try
            {
                var data = _surveyAnalyticsRepository.GetAllInclude(new Expression<Func<SurveyAnalytics, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Survey);
                return data.OrderByDescending(i => i.TotalResponses);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnalytics>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnalytics> GetAllIncludingClosedSurveyDataBySurveyIdAsync(int surveyId)
        {
            try
            {
                var data = _surveyAnalyticsRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<SurveyAnalytics, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.Survey.IsOnline==false
                }, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnalytics>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnalytics> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _surveyAnalyticsRepository.GetAllInclude(new Expression<Func<SurveyAnalytics, bool>>[]
                {

                }, null, y => y.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnalytics>().AsQueryable();
            }
        }

        public async Task<SurveyAnalytics> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _surveyAnalyticsRepository.GetIncludeAsync(i => i.Id == id, y => y.Survey);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }
        public async Task<SurveyAnalytics> GetIncludingClosedSurveyDataBySurveyIdAsync(int surveyId)
        {
            try
            {
                var data = await _surveyAnalyticsRepository.GetIncludingClosedSurveyDataBySurveyIdAsync(surveyId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error exception while uploading survey analyze data.", ex);
            }
        }

        public SurveyAnalytics GetSurveyInformationForSurveyAnalyticBySurveyId(int surveyId)
        {
            try
            {
                var data = _surveyAnalyticsRepository.GetInclude(i => i.SurveyId == surveyId, y => y.Survey, y => y.Survey.Hits, y => y.Survey.Likes, y => y.Survey.SavedContents);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error exception while uploading survey analyze data.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _surveyAnalyticsRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _surveyAnalyticsRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _surveyAnalyticsRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _surveyAnalyticsRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task UpdateSurveyAnalyticsAsync(int surveyId)
        {
            await _surveyAnalyticsRepository.UpdateSurveyAnalyticsAsync(surveyId);
        }
    }
}
