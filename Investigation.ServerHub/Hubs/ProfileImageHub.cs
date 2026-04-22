using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class ProfileImageHub : Hub
    {
        readonly IProfileImageService _profileImageService;
        public ProfileImageHub(IProfileImageService profileImageService)
        {
            _profileImageService = profileImageService;
        }
        public async Task<IEnumerable<ProfileImageDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _profileImageService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new ProfileImageDto
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
                return new List<ProfileImageDto>();
            }
            catch (Exception)
            {
                return new List<ProfileImageDto>();
            }
        }
    }
}
