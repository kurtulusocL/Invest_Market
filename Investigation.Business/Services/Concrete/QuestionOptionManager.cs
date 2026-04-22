using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class QuestionOptionManager : IQuestionOptionService
    {
        readonly IQuestionOptionRepository _questionOptionRepository;
        public QuestionOptionManager(IQuestionOptionRepository questionOptionRepository)
        {
            _questionOptionRepository = questionOptionRepository;
        }

        public async Task<bool> CreateAsync(string optionText, int orderIndex, int surveyQuestionId)
        {
            try
            {
                var model = new QuestionOption
                {
                    OptionText = optionText,
                    OrderIndex = orderIndex,
                    SurveyQuestionId = surveyQuestionId
                };
                if (model != null)
                {
                    var result = await _questionOptionRepository.AddAsync(model);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(QuestionOption entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _questionOptionRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _questionOptionRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<QuestionOption>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _questionOptionRepository.GetAllIncludeAsync(new Expression<Func<QuestionOption, bool>>[]
                {
                    
                }, null, y => y.SurveyAnswers, y => y.SurveyQuestion, y => y.SurveyQuestion.Survey);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<QuestionOption>();
            }
        }

        public IQueryable<QuestionOption> GetAllIncludingAsync()
        {
            try
            {
                var data = _questionOptionRepository.GetAllInclude(new Expression<Func<QuestionOption, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.SurveyAnswers, y => y.SurveyQuestion, y => y.SurveyQuestion.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<QuestionOption>().AsQueryable();
            }
        }

        public IQueryable<QuestionOption> GetAllIncludingBySurveyQuestionIdAsync(int surveyQuestionId)
        {
            try
            {
                var data = _questionOptionRepository.GetAllIncludeById(surveyQuestionId, "SurveyQuestionId", new Expression<Func<QuestionOption, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.SurveyAnswers, y => y.SurveyQuestion, y => y.SurveyQuestion.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<QuestionOption>().AsQueryable();
            }
        }

        public IQueryable<QuestionOption> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _questionOptionRepository.GetAllInclude(new Expression<Func<QuestionOption, bool>>[]
                {

                }, null, y => y.SurveyAnswers, y => y.SurveyQuestion, y => y.SurveyQuestion.Survey);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<QuestionOption>().AsQueryable();
            }
        }

        public IQueryable<QuestionOption> GetAllQuestionOptionsForSurveyVoteBySurveyQuestionId(int surveyQuestionId)
        {
            try
            {
                return _questionOptionRepository.GetAllIncludeById(surveyQuestionId, "SurveyQuestionId", new Expression<Func<QuestionOption, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.SurveyQuestionId==surveyQuestionId
                }, y => y.SurveyAnswers, y => y.SurveyQuestion).OrderBy(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<QuestionOption>().AsQueryable();
            }
        }

        public async Task<QuestionOption> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _questionOptionRepository.GetIncludeAsync(i => i.Id == id, y => y.SurveyAnswers, y => y.SurveyQuestion, y => y.SurveyQuestion.Survey);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _questionOptionRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _questionOptionRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _questionOptionRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _questionOptionRepository.SetNotDeletedAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(string optionText, int orderIndex, int surveyQuestionId, int id)
        {
            try
            {
                var model = await _questionOptionRepository.GetIncludeAsync(i => i.Id == id);
                if (model == null)
                {
                    return false;
                }
                model.OptionText = optionText;
                model.OrderIndex = orderIndex;
                model.SurveyQuestionId = surveyQuestionId;
                model.UpdatedDate = DateTime.UtcNow;

                if (model != null)
                {
                    var result = await _questionOptionRepository.UpdateAsync(model);
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
