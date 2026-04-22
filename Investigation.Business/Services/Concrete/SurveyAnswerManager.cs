using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class SurveyAnswerManager : ISurveyAnswerService
    {
        readonly ISurveyAnswerRepository _surveyAnswerRepository;
        public SurveyAnswerManager(ISurveyAnswerRepository surveyAnswerRepository)
        {
            _surveyAnswerRepository = surveyAnswerRepository;
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _surveyAnswerRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(SurveyAnswer entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _surveyAnswerRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _surveyAnswerRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<SurveyAnswer> GetAllIncludingAsync()
        {
            try
            {
                var data = _surveyAnswerRepository.GetAllInclude(new Expression<Func<SurveyAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.AppUser, y => y.SurveyResponse, y => y.QuestionOption, y => y.QuestionOption);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnswer>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnswer> GetAllIncludingBySurveyQuestionOptionIdAsync(int? questionOptionId)
        {
            try
            {
                if (questionOptionId == null)
                    throw new ArgumentNullException(nameof(questionOptionId), "questionOptionId was null");

                var data = _surveyAnswerRepository.GetAllIncludeById(questionOptionId, "QuestionOptionId", new Expression<Func<SurveyAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.SurveyResponse, y => y.QuestionOption, y => y.QuestionOption);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnswer>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnswer> GetAllIncludingBySurveyQuestionIdAsync(int? surveyQuestionId)
        {
            try
            {
                if (surveyQuestionId == null)
                    throw new ArgumentNullException(nameof(surveyQuestionId), "surveyQuestionId was null");

                var data = _surveyAnswerRepository.GetAllIncludeById(surveyQuestionId, "SurveyQuestionId", new Expression<Func<SurveyAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.SurveyResponse, y => y.QuestionOption, y => y.QuestionOption);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnswer>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnswer> GetAllIncludingBySurveyResponseIdAsync(int? surveyResponseId)
        {
            try
            {
                if (surveyResponseId == null)
                    throw new ArgumentNullException(nameof(surveyResponseId), "surveyResponseId was null");

                var data = _surveyAnswerRepository.GetAllIncludeById(surveyResponseId, "SurveyResponseId", new Expression<Func<SurveyAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.SurveyResponse, y => y.QuestionOption, y => y.QuestionOption);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnswer>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnswer> GetAllIncludingByUserIdAsync(string appUserId)
        {
            try
            {
                if (appUserId == null)
                    throw new ArgumentNullException(nameof(appUserId), "appUserId was null");

                var data = _surveyAnswerRepository.GetAllIncludeById(appUserId, "AppUserId", new Expression<Func<SurveyAnswer, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.AppUser, y => y.SurveyResponse, y => y.QuestionOption, y => y.QuestionOption);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnswer>().AsQueryable();
            }
        }

        public IQueryable<SurveyAnswer> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _surveyAnswerRepository.GetAllInclude(new Expression<Func<SurveyAnswer, bool>>[]
                {

                }, null, y => y.AppUser, y => y.SurveyResponse, y => y.QuestionOption, y => y.QuestionOption);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyAnswer>().AsQueryable();
            }
        }

        public async Task<SurveyAnswer> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _surveyAnswerRepository.GetIncludeAsync(i => i.Id == id, y => y.AppUser, y => y.SurveyResponse, y => y.QuestionOption, y => y.QuestionOption);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _surveyAnswerRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _surveyAnswerRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _surveyAnswerRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _surveyAnswerRepository.SetNotDeletedAsync(id);
            return result;
        }

        public int SurveyAnswerCounter()
        {
            return _surveyAnswerRepository.SurveyAnswerCounter();
        }

        public async Task<IEnumerable<SurveyAnswer>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _surveyAnswerRepository.GetAllIncludeAsync(new Expression<Func<SurveyAnswer, bool>>[]
                {
                   
                }, null, y => y.AppUser, y => y.SurveyResponse, y => y.QuestionOption, y => y.QuestionOption);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SurveyAnswer>();
            }
        }
    }
}
