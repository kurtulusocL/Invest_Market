using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SubSectorHub : Hub
    {
        readonly ISubSectorService _subSectorService;
        public SubSectorHub(ISubSectorService subSectorService)
        {
            _subSectorService = subSectorService;
        }
        public async Task<IEnumerable<SubSectorDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _subSectorService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SubSectorDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        SectorDtoId = i.SectorId,
                        CompanyCount = i.Companies?.Count ?? 0,
                        RecentlyInvestCount = i.RecentlyInvests?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SubSectorDto>();
            }
            catch (Exception)
            {
                return new List<SubSectorDto>();
            }
        }
    }
}
