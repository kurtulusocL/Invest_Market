using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class SurveyQuestionManager : ISurveyQuestionService
    {
        readonly ISurveyQuestionRepository _surveyQuestionRepository;
        public SurveyQuestionManager(ISurveyQuestionRepository surveyQuestionRepository)
        {
            _surveyQuestionRepository = surveyQuestionRepository;
        }

        public async Task<bool> CreateAsync(string questionText, int orderIndex, bool isRequired, int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var entity = new SurveyQuestion
                {
                    QuestionText = questionText,
                    OrderIndex = orderIndex,
                    IsRequired = isRequired,
                    SurveyId = surveyId
                };
                if (entity != null)
                {
                    var result = await _surveyQuestionRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _surveyQuestionRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(SurveyQuestion entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _surveyQuestionRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _surveyQuestionRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<SurveyQuestion>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _surveyQuestionRepository.GetAllIncludeAsync(new Expression<Func<SurveyQuestion, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Survey, y => y.QuestionOptions, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SurveyQuestion>();
            }
        }

        public IQueryable<SurveyQuestion> GetAllIncludingAsync()
        {
            try
            {
                var data = _surveyQuestionRepository.GetAllInclude(new Expression<Func<SurveyQuestion, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Survey, y => y.QuestionOptions, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyQuestion>().AsQueryable();
            }
        }

        public IQueryable<SurveyQuestion> GetAllIncludingByAnswerQuantityAsync()
        {
            try
            {
                var data = _surveyQuestionRepository.GetAllInclude(new Expression<Func<SurveyQuestion, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Survey, y => y.QuestionOptions, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.SurveyAnswers.Count());
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyQuestion>().AsQueryable();
            }
        }

        public IQueryable<SurveyQuestion> GetAllIncludingBySurveyIdAsync(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var data = _surveyQuestionRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<SurveyQuestion, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Survey, y => y.QuestionOptions, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyQuestion>().AsQueryable();
            }
        }

        public IQueryable<SurveyQuestion> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _surveyQuestionRepository.GetAllInclude(new Expression<Func<SurveyQuestion, bool>>[]
                {

                }, null, y => y.Survey, y => y.QuestionOptions, y => y.SurveyAnswers);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyQuestion>().AsQueryable();
            }
        }

        public IQueryable<SurveyQuestion> GetAllIncludingQuestionForVoteBySurveyIdAsync(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var data = _surveyQuestionRepository.GetAllIncludeById(surveyId, "SurveyId", new Expression<Func<SurveyQuestion, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SurveyId==surveyId
                }, y => y.Survey, y => y.QuestionOptions, y => y.SurveyAnswers);
                return data.OrderBy(i => i.OrderIndex);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SurveyQuestion>().AsQueryable();
            }
        }

        public async Task<SurveyQuestion> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _surveyQuestionRepository.GetIncludeAsync(i => i.Id == id, y => y.Survey, y => y.QuestionOptions, y => y.SurveyAnswers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public SurveyQuestion GetIncludingQuestionForVoteBySurveyId(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                return _surveyQuestionRepository.GetInclude(i => i.SurveyId == surveyId, y => y.Survey);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<SurveyQuestion> GetIncludingQuestionForVoteBySurveyIdAsync(int? surveyId)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                return await _surveyQuestionRepository.GetIncludeAsync(i => i.SurveyId == surveyId, y => y.Survey, y => y.QuestionOptions, y => y.SurveyAnswers);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _surveyQuestionRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _surveyQuestionRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _surveyQuestionRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _surveyQuestionRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string questionText, int orderIndex, bool isRequired, int? surveyId, int id)
        {
            try
            {
                if (surveyId == null)
                    throw new ArgumentNullException(nameof(surveyId), "surveyId was null");

                var entity = await _surveyQuestionRepository.GetIncludeAsync(i => i.Id == id);
                if (entity == null)
                {
                    return false;
                }

                entity.QuestionText = questionText;
                entity.OrderIndex = orderIndex;
                entity.IsRequired = isRequired;
                entity.SurveyId = surveyId;
                entity.UpdatedDate = DateTime.UtcNow;
                if (entity != null)
                {
                    var result = await _surveyQuestionRepository.UpdateAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the entity.", ex);
            }
        }
    }
}
