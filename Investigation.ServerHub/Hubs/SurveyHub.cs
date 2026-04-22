using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SurveyHub : Hub
    {
        readonly ISurveyService _surveyService;
        public SurveyHub(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }
        public async Task<IEnumerable<SurveyDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _surveyService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SurveyDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        IsOnline = i.IsOnline,
                        StartDate = i.StartDate,
                        ClosedDate = i.ClosedDate,
                        Desc = i.Desc,
                        IsAnonymous = i.IsAnonymous,
                        AllowMultipleResponses = i.AllowMultipleResponses,
                        AppUserDtoId = i.AppUserId,
                        CompanyDtoId = i.CompanyId,
                        InvestorDtoId = i.InvestorId,
                        HitCount = i.Hits?.Count ?? 0,
                        LikeCount = i.Likes?.Count ?? 0,
                        ReportCount = i.Reports?.Count ?? 0,
                        SurveyAnalyicsCount = i.SurveyAnalytics?.Count ?? 0,
                        SurveyQuestionCount = i.SurveyQuestions?.Count ?? 0,
                        SurveyResponseCount = i.SurveyResponses?.Count ?? 0,
                        SavedContentCount = i.SavedContents?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SurveyDto>();
            }
            catch (Exception)
            {
                return new List<SurveyDto>();
            }
        }
    }
}
