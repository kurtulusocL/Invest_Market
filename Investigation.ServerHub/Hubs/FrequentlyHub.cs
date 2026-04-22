using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class FrequentlyHub : Hub
    {
        readonly IFrequentlyService _frequentlyService;
        public FrequentlyHub(IFrequentlyService frequentlyService)
        {
            _frequentlyService = frequentlyService;
        }
        public async Task<IEnumerable<FrequentlyDto>> GetAllAsync()
        {
            try
            {
                var data = await _frequentlyService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new FrequentlyDto
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
                return new List<FrequentlyDto>();
            }
            catch (Exception)
            {
                return new List<FrequentlyDto>();
            }
        }
    }
}
