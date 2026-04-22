using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class UserSessionHub : Hub
    {
        readonly IUserSessionService _userSessionService;
        public UserSessionHub(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }
        public async Task<IEnumerable<UserSessionDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _userSessionService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new UserSessionDto
                    {
                        Id = i.Id,
                        Username = i.Username,
                        LoginDate = i.LoginDate,
                        LogoutDate = i.LogoutDate,
                        IsOnline = i.IsOnline,
                        OnlineDurationSeconds = i.OnlineDurationSeconds,
                        AppUserDtoId = i.AppUserId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<UserSessionDto>();
            }
            catch (Exception)
            {
                return new List<UserSessionDto>();
            }
        }
    }
}
