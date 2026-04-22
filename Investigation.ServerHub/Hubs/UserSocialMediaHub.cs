using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class UserSocialMediaHub : Hub
    {
        readonly IUserSocialMediaService _userSocialMediaService;
        public UserSocialMediaHub(IUserSocialMediaService userSocialMediaService)
        {
            _userSocialMediaService = userSocialMediaService;
        }
        public async Task<IEnumerable<UserSocialMediaDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _userSocialMediaService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new UserSocialMediaDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Url = i.Url,
                        CompanyDtoId = i.CompanyId,
                        InvestorDtoId = i.InvestorId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<UserSocialMediaDto>();
            }
            catch (Exception)
            {
                return new List<UserSocialMediaDto>();
            }
        }
    }
}
