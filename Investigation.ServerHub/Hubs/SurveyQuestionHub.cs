using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SurveyQuestionHub : Hub
    {
        readonly ISurveyQuestionService _surveyQuestionService;
        public SurveyQuestionHub(ISurveyQuestionService surveyQuestionService)
        {
            _surveyQuestionService = surveyQuestionService;
        }
        public async Task<IEnumerable<SurveyQuestionDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _surveyQuestionService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SurveyQuestionDto
                    {
                        Id = i.Id,
                        QuestionText = i.QuestionText,
                        IsRequired = i.IsRequired,
                        OrderIndex = i.OrderIndex,
                        SurveyDtoId = i.SurveyId,
                        QuestionOptionsCount = i.QuestionOptions?.Count ?? 0,
                        SurveyAnswerCount = i.SurveyAnswers?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SurveyQuestionDto>();
            }
            catch (Exception)
            {
                return new List<SurveyQuestionDto>();
            }
        }
    }
}
