using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class InvestorHub : Hub
    {
        readonly IInvestorService _investorService;
        public InvestorHub(IInvestorService investorService)
        {
            _investorService = investorService;
        }
        public async Task<IEnumerable<InvestorDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _investorService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new InvestorDto
                    {
                        Id = i.Id,
                        Bio = i.Bio,
                        InvestArea = i.InvestArea,
                        SinceWhen = i.SinceWhen,
                        IsLookingForCompany = i.IsLookingForCompany,
                        EmailAddress = i.EmailAddress,
                        PhoneNumber = i.PhoneNumber,
                        CoverImageUrl = i.CoverImageUrl,
                        AppUserDtoId = i.AppUserId,
                        CountryDtoId = i.CountryId,
                        InvestorCategoryDtoId = i.InvestorCategoryId,
                        AnnouncementCount = i.Announcements?.Count ?? 0,
                        BlogCount = i.Blogs?.Count ?? 0,
                        HitCount = i.Hits?.Count ?? 0,
                        LikeCount = i.Likes?.Count ?? 0,
                        RecentlyInvestCount = i.RecentlyInvests?.Count ?? 0,
                        PostCount = i.Posts?.Count ?? 0,
                        ReportCount = i.Reports?.Count ?? 0,
                        SavedContentCount = i.SavedContents?.Count ?? 0,
                        SurveyCount = i.Surveys?.Count ?? 0,
                        UserSocialMediaCount = i.UserSocialMedias?.Count ?? 0,
                    }).ToList();
                }
                return new List<InvestorDto>();
            }
            catch (Exception)
            {
                return new List<InvestorDto>();
            }
        }
    }
}
