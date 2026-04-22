using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class UserProfileImageHub : Hub
    {
        readonly IUserProfileImageService _userProfileImageService;
        public UserProfileImageHub(IUserProfileImageService userProfileImageService)
        {
            _userProfileImageService = userProfileImageService;
        }
        public async Task<IEnumerable<UserProfileImageDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _userProfileImageService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new UserProfileImageDto
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        AppUserDtoId = i.AppUserId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<UserProfileImageDto>();
            }
            catch (Exception)
            {
                return new List<UserProfileImageDto>();
            }
        }
    }
}
