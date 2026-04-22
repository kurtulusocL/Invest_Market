using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class RecentlyInvestHub : Hub
    {
        readonly IRecentlyInvestService _recentlyInvestService;
        public RecentlyInvestHub(IRecentlyInvestService recentlyInvestService)
        {
            _recentlyInvestService = recentlyInvestService;
        }
        public async Task<IEnumerable<RecentlyInvestDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _recentlyInvestService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new RecentlyInvestDto
                    {
                        Id = i.Id,
                        Title = i.Title,
                        Desc = i.Desc,
                        InvestDate = i.InvestDate,
                        IsExit = i.IsExit,
                        ExitDate = i.ExitDate,
                        WebUrl = i.WebUrl,
                        ImageUrl = i.ImageUrl,
                        InvestorDtoId = i.InvestorId,
                        SectorDtoId = i.SectorId,
                        SubSectorDtoId = i.SubSectorId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<RecentlyInvestDto>();
            }
            catch (Exception)
            {
                return new List<RecentlyInvestDto>();
            }
        }
    }
}
