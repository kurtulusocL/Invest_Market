using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CompanyHub : Hub
    {
        readonly ICompanyService _companyService;
        public CompanyHub(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public async Task<IEnumerable<CompanyDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _companyService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CompanyDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Slogan = i.Slogan,
                        ShortBio = i.ShortBio,
                        Desc = i.Desc,
                        FoundationDate = i.FoundationDate,
                        IsLookingForInvest = i.IsLookingForInvest,
                        LinkedIn = i.LinkedIn,
                        GitHub = i.GitHub,
                        LogoUrl = i.LogoUrl,
                        AppUserDtoId = i.AppUserId,
                        CompanyCategoryDtoId = i.CompanyCategoryId,
                        CountryDtoId = i.CountryId,
                        SectorDtoId = i.SectorId,
                        SubSectorDtoId = i.SubSectorId,
                        AnnouncementCount = i.Announcements?.Count ?? 0,
                        BlogCount = i.Blogs?.Count ?? 0,
                        CompanyContactCount = i.CompanyContacts?.Count ?? 0,
                        CompanyFinanceCount = i.CompanyFinances?.Count ?? 0,
                        CompanyPintechCount = i.CompanyPinteches?.Count ?? 0,
                        CompanyStageCount = i.CompanyStages?.Count ?? 0,
                        CompanyTeamCount = i.CompanyTeams?.Count ?? 0,
                        CommentCount = i.Comments?.Count ?? 0,
                        HitCount = i.Hits?.Count ?? 0,
                        LikeCount = i.Likes?.Count ?? 0,
                        PictureCount = i.Pictures?.Count ?? 0,
                        PostCount = i.Posts?.Count ?? 0,
                        ReportCount = i.Reports?.Count ?? 0,
                        SavedContentCount = i.SavedContents?.Count ?? 0,
                        SurveyCount = i.Surveys?.Count ?? 0,
                        UserSocialMediaCount = i.UserSocialMedias?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CompanyDto>();
            }
            catch (Exception)
            {
                return new List<CompanyDto>();
            }
        }
    }
}
