using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class BannerImageHub : Hub
    {
        readonly IBannerImageService _bannerImageService;
        public BannerImageHub(IBannerImageService bannerImageService)
        {
            _bannerImageService = bannerImageService;
        }
        public async Task<IEnumerable<BannerImageDto>> GetAllAsync()
        {
            try
            {
                var data = await _bannerImageService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new BannerImageDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        ControllerName = i.ControllerName,
                        Image = i.Image,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<BannerImageDto>();
            }
            catch (Exception)
            {
                return new List<BannerImageDto>();
            }
        }
    }
}
