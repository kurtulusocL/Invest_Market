using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract.ServiceAbstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class AppUserHub : Hub
    {
        readonly IUserService _userService;
        readonly IUserAbstractServices _userAbstractServices;
        public AppUserHub(IUserService userService, IUserAbstractServices userAbstractServices)
        {
            _userService = userService;
            _userAbstractServices = userAbstractServices;
        }
        public async Task<IEnumerable<AppUserDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _userService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new AppUserDto
                    {
                        Id = i.Id,
                        NameSurname = i.NameSurname,
                        UserName = i.UserName,
                        Email = i.Email,
                        PhoneNumber = i.PhoneNumber,
                        Birthdate = i.Birthdate,
                        Country = i.Country,
                        Title = i.Title,
                        IsAdmin = i.IsAdmin,
                        IsInvestor = i.IsInvestor,
                        IsCompany = i.IsCompany,
                        PhoneNumberConfirmed = i.PhoneNumberConfirmed,
                        EmailConfirmed = i.EmailConfirmed,
                        ConfirmCode = i.ConfirmCode,
                        IsAcceptedPolicies = i.IsAcceptedPolicies,
                        IsLoginConfirmCodeActive = i.IsLoginConfirmCodeActive,
                        IsRegisterConfirmCodeActive = i.IsRegisterConfirmCodeActive,
                        BlogCount = i.Blogs?.Count ?? 0,
                        CancelMembershipCount = i.CancelMemberships?.Count ?? 0,
                        CommentCount = i.Comments?.Count ?? 0,
                        CommentAnswerCount = i.CommentAnswers?.Count ?? 0,
                        CompanyCount = i.Companies?.Count ?? 0,
                        HitCount = i.Hits?.Count ?? 0,
                        InvestorCount = i.Investors?.Count ?? 0,
                        LikeCount = i.Likes?.Count ?? 0,
                        PostCount = i.Posts?.Count ?? 0,
                        ProfileImageCount = i.ProfileImages?.Count ?? 0,
                        ReportCount = i.Reports?.Count ?? 0,
                        SavedContentCount = i.SavedContents?.Count ?? 0,
                        SurveyCount = i.Surveys?.Count ?? 0,
                        SurveyAnswerCount = i.SurveyAnswers?.Count ?? 0,
                        SurveyReponseCount = i.SurveyResponses?.Count ?? 0,
                        UserProfileImageCount = i.UserProfileImages?.Count ?? 0,
                        UserSessionCount = i.UserSessions?.Count ?? 0,
                        SentMessageCount = i.SentMessages?.Count ?? 0,
                        RecievedMessageCount = i.ReceivedMessages?.Count ?? 0,
                        BlockedMessageCount = i.MessageUserBlockedUsers?.Count ?? 0,
                        BlockedUserCount = i.MessageUserBlockedByUsers?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted,
                        NormalizedEmail = i.NormalizedEmail,
                        NormalizedUserName = i.NormalizedUserName,
                        PasswordHash = i.PasswordHash,
                        SecurityStamp = i.SecurityStamp,
                        ConcurrencyStamp = i.ConcurrencyStamp,
                        TwoFactorEnabled = i.TwoFactorEnabled,
                        AccessFailedCount = i.AccessFailedCount,
                        LockoutEnd = i.LockoutEnd.HasValue ? i.LockoutEnd.Value.DateTime : (DateTime?)null,
                        LockoutEnabled = i.LockoutEnabled
                    }).ToList();
                }
                return new List<AppUserDto>();
            }
            catch (Exception)
            {
                return new List<AppUserDto>();
            }
        }

        public async Task<List<IdentityUserRole<string>>> GetAllUserRoles()
        {
            try
            {
                return await _userAbstractServices.GetAllUserRoles();
            }
            catch (Exception)
            {
                return new List<IdentityUserRole<string>>();
            }
        }
    }
}
