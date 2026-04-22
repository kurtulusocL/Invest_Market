using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SocialMediaHub : Hub
    {
        readonly ISocialMediaService _socialMediaService;
        public SocialMediaHub(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }
        public async Task<IEnumerable<SocialMediaDto>> GetAllAsync()
        {
            try
            {
                var data = await _socialMediaService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SocialMediaDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Url = i.Url,
                        IconUrl = i.IconUrl,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SocialMediaDto>();
            }
            catch (Exception)
            {
                return new List<SocialMediaDto>();
            }
        }
    }
}
