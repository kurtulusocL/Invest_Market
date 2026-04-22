using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SectorHub : Hub
    {
        readonly ISectorService _sectorService;
        public SectorHub(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }
        public async Task<IEnumerable<SectorDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _sectorService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SectorDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        CompanyCount = i.Companies?.Count ?? 0,
                        RecentlyInvestCount = i.RecentlyInvests?.Count ?? 0,
                        SubSectorCount = i.SubSectors?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SectorDto>();
            }
            catch (Exception)
            {
                return new List<SectorDto>();
            }
        }
    }
}
