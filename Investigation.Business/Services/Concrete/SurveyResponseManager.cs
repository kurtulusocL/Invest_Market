using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class SurveyResponseManager : ISurveyResponseService
    {
        readonly ISurveyResponseRepository _surveyReponseRepository;
        public SurveyResponseManager(ISurveyResponseRepository surveyResponseRepository)
        {
            _surveyReponseRepository = surveyResponseRepository;
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _surveyReponseRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(SurveyResponse entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _surveyReponseRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _surveyReponseRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<SurveyResponse>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _surveyReponseRepository.GetAllIncludeAsync(new Expression<Func<SurveyResponse, bool>>[]
                {
                   
                }, null, y => y.Survey, y => y.AppUser, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SurveyResponse>();
            }
        }

        public IQueryable<SurveyResponse> GetAllIncludingAsync()
        {
            try
            {
                var data = _surveyReponseRepository.GetAllInclude(new Expression<Func<SurveyResponse, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Survey, y => y.AppUser, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyResponse>().AsQueryable();
            }
        }

        public IQueryable<SurveyResponse> GetAllIncludingByCompletedDateAsync()
        {
            try
            {
                var data = _surveyReponseRepository.GetAllInclude(new Expression<Func<SurveyResponse, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsCompleted==true&&i.CompletedAt!=null
                }, null, y => y.Survey, y => y.AppUser, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CompletedAt);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyResponse>().AsQueryable();
            }
        }

        public IQueryable<SurveyResponse> GetAllIncludingByStartedDateAsync()
        {
            try
            {
                var data = _surveyReponseRepository.GetAllInclude(new Expression<Func<SurveyResponse, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Survey, y => y.AppUser, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.StartedAt);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyResponse>().AsQueryable();
            }
        }

        public IQueryable<SurveyResponse> GetAllIncludingBySurveyIdAsync(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var data = _surveyReponseRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<SurveyResponse, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Survey, y => y.AppUser, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyResponse>().AsQueryable();
            }
        }

        public IQueryable<SurveyResponse> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _surveyReponseRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<SurveyResponse, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Survey, y => y.AppUser, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyResponse>().AsQueryable();
            }
        }

        public IQueryable<SurveyResponse> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _surveyReponseRepository.GetAllInclude(new Expression<Func<SurveyResponse, bool>>[]
                {

                }, null, y => y.Survey, y => y.AppUser, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyResponse>().AsQueryable();
            }
        }

        public async Task<SurveyResponse> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _surveyReponseRepository.GetIncludeAsync(i => i.Id == id, y => y.Survey, y => y.AppUser, y => y.SurveyAnswers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _surveyReponseRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _surveyReponseRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _surveyReponseRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _surveyReponseRepository.SetNotDeletedAsync(id);
            return result;
        }

        public int SurveyResponseCounter()
        {
            return _surveyReponseRepository.SurveyResponseCounter();
        }
    }
}
