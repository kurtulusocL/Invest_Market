using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class AboutHub : Hub
    {
        readonly IAboutService _aboutService;
        public AboutHub(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IEnumerable<AboutDto>> GetAllAsync()
        {
            try
            {
                var data = await _aboutService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new AboutDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Subtitle = i.Subtitle,
                        Detail = i.Detail,
                        Desc = i.Desc,
                        ImageUrl = i.ImageUrl,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<AboutDto>();
            }
            catch (Exception)
            {
                return new List<AboutDto>();
            }
        }
    }
}
