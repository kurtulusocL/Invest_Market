using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SliderHub : Hub
    {
        readonly ISliderService _sliderService;
        public SliderHub(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        public async Task<IEnumerable<SliderDto>> GetAllAsync()
        {
            try
            {
                var data = await _sliderService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SliderDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Text = i.Text,
                        ImageUrl = i.ImageUrl,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SliderDto>();
            }
            catch (Exception)
            {
                return new List<SliderDto>();
            }
        }
    }
}
