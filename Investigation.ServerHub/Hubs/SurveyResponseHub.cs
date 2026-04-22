using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SurveyResponseHub : Hub
    {
        readonly ISurveyResponseService _surveyResponseService;
        public SurveyResponseHub(ISurveyResponseService surveyResponseService)
        {
            _surveyResponseService = surveyResponseService;
        }
        public async Task<IEnumerable<SurveyResponseDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _surveyResponseService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SurveyResponseDto
                    {
                        Id = i.Id,
                        StartedAt = i.StartedAt,
                        IsCompleted = i.IsCompleted,
                        CompletedAt = i.CompletedAt,
                        AppUserDtoId = i.AppUserId,
                        SurveyDtoId = i.SurveyId,
                        SurveyAnswerCount = i.SurveyAnswers?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SurveyResponseDto>();
            }
            catch (Exception)
            {
                return new List<SurveyResponseDto>();
            }
        }
    }
}
