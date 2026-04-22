using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class HowItWorksHub : Hub
    {
        readonly IHowItWorksService _howItWorksService;
        public HowItWorksHub(IHowItWorksService howItWorksService)
        {
            _howItWorksService = howItWorksService;
        }
        public async Task<IEnumerable<HowItWorksDto>> GetAllAsync()
        {
            try
            {
                var data = await _howItWorksService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new HowItWorksDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Subtitle = i.Subtitle,
                        Desc = i.Desc,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<HowItWorksDto>();
            }
            catch (Exception)
            {
                return new List<HowItWorksDto>();
            }
        }
    }
}
