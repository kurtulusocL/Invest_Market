using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SurveyAnswerHub : Hub
    {
        readonly ISurveyAnswerService _surveyAnswerService;
        public SurveyAnswerHub(ISurveyAnswerService surveyAnswerService)
        {
            _surveyAnswerService = surveyAnswerService;
        }
        public async Task<IEnumerable<SurveyAnswerDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _surveyAnswerService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SurveyAnswerDto
                    {
                        Id = i.Id,
                        AppUserDtoId = i.AppUserId,
                        SurveyResponseDtoId = i.SurveyResponseId,
                        SurveyQuestionDtoId = i.SurveyQuestionId,
                        QuestionOptionDtoId = i.QuestionOptionId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SurveyAnswerDto>();
            }
            catch (Exception)
            {
                return new List<SurveyAnswerDto>();
            }
        }
    }
}
