using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class HitHub : Hub
    {
        readonly IHitService _hitService;
        public HitHub(IHitService hitService)
        {
            _hitService = hitService;
        }
        public async Task<IEnumerable<HitDto>> GetAllIncludingAsync()
        {
            try
            {
                var data=await _hitService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new HitDto
                    {
                        Id = i.Id,
                        CurrentValue = i.CurrentValue,
                        AppUserDtoId = i.AppUserId,
                        AdDtoId = i.AdId,
                        AnnouncementDtoId = i.AnnouncementId,
                        BlogDtoId = i.BlogId,
                        CommentDtoId = i.CommentId,
                        CommentAnswerDtoId = i.CommentAnswerId,
                        CompanyDtoId = i.CompanyId,
                        CompanyFinanceDtoId = i.CompanyFinanceId,
                        CompanyPintechDtoId = i.CompanyPintechId,
                        CompanyStageDtoId = i.CompanyStageId,
                        InvestorDtoId = i.InvestorId,
                        PostDtoId = i.PostId,
                        SurveyDtoId = i.SurveyId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<HitDto>();
            }
            catch (Exception)
            {
                return new List<HitDto>();
            }
        }
    }
}
