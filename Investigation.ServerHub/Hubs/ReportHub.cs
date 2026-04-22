using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class ReportHub : Hub
    {
        readonly IReportService _reportService;
        public ReportHub(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<IEnumerable<ReportDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _reportService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new ReportDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Subject = i.Subject,
                        IsFixed = i.IsFixed,
                        FixedDate = i.FixedDate,
                        ReportCategoryDtoId = i.ReportCategoryId,
                        AppUserDtoId = i.AppUserId,
                        AnnouncementDtoId = i.AnnouncementId,
                        BlogDtoId = i.BlogId,
                        CommentDtoId = i.CommentId,
                        CommentAnswerDtoId = i.CommentAnswerId,
                        CompanyDtoId = i.CompanyId,
                        InvestorDtoId = i.InvestorId,
                        NewsDtoId = i.NewsId,
                        PostDtoId = i.PostId,
                        SectorNewsDtoId = i.SectorNewsId,
                        SurveyDtoId = i.SurveyId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<ReportDto>();
            }
            catch (Exception)
            {
                return new List<ReportDto>();
            }
        }
    }
}
