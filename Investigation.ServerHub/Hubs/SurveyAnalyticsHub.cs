using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SurveyAnalyticsHub : Hub
    {
        readonly ISurveyAnalyticsService _surveyAnalyticsService;
        public SurveyAnalyticsHub(ISurveyAnalyticsService surveyAnalyticsService)
        {
            _surveyAnalyticsService = surveyAnalyticsService;
        }
        public async Task<IEnumerable<SurveyAnalyticsDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _surveyAnalyticsService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SurveyAnalyticsDto
                    {
                        Id = i.Id,
                        AnalyticsDataJson = i.AnalyticsDataJson,
                        TotalResponses = i.TotalResponses,
                        CompletionRate = i.CompletionRate,
                        AverageCompletionTimeSeconds = i.AverageCompletionTimeSeconds,
                        SurveyDtoId = i.SurveyId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SurveyAnalyticsDto>();
            }
            catch (Exception)
            {
                return new List<SurveyAnalyticsDto>();
            }
        }
    }
}
