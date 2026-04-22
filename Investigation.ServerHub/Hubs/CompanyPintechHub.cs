using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CompanyPintechHub : Hub
    {
        readonly ICompanyPintechService _companyPintechService;
        public CompanyPintechHub(ICompanyPintechService companyPintechService)
        {
            _companyPintechService = companyPintechService;
        }
        public async Task<IEnumerable<CompanyPintechDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _companyPintechService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CompanyPintechDto
                    {
                        Id = i.Id,
                        CompanyDtoId = i.CompanyId,
                        WorkPlan = i.WorkPlan,
                        ServiceProduct = i.ServiceProduct,
                        Description = i.Description,
                        MarketingStrategy = i.MarketingStrategy,
                        GrowingPotantial = i.GrowingPotantial,
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
                return new List<CompanyPintechDto>();
            }
            catch (Exception)
            {
                return new List<CompanyPintechDto>();
            }
        }
    }
}
