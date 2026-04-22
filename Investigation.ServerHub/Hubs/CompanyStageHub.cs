using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CompanyStageHub : Hub
    {
        readonly ICompanyStageService _companyStageService;
        public CompanyStageHub(ICompanyStageService companyStageService)
        {
            _companyStageService = companyStageService;
        }
        public async Task<IEnumerable<CompanyStageDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _companyStageService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CompanyStageDto
                    {
                        Id = i.Id,
                        CompanyDtoId = i.CompanyId,
                        StageName = i.StageName,
                        StageValue = i.StageValue,
                        HitCount = i.Hits?.Count ?? 0,
                        VisibilitySettingDtoId = i.VisibilitySettingId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CompanyStageDto>();
            }
            catch (Exception)
            {
                return new List<CompanyStageDto>();
            }
        }
    }
}
