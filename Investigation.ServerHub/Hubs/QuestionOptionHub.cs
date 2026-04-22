using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class QuestionOptionHub : Hub
    {
        readonly IQuestionOptionService _questionOptionService;
        public QuestionOptionHub(IQuestionOptionService questionOptionService)
        {
            _questionOptionService = questionOptionService;
        }
        public async Task<IEnumerable<QuestionOptionDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _questionOptionService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new QuestionOptionDto()
                    {
                        Id = i.Id,
                        OptionText = i.OptionText,
                        OrderIndex = i.OrderIndex,
                        SurveyAnswerCount = i.SurveyAnswers?.Count ?? 0,
                        SurveyQuestionDtoId = i.SurveyQuestionId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<QuestionOptionDto>();
            }
            catch (Exception)
            {
                return new List<QuestionOptionDto>();
            }
        }
    }
}
