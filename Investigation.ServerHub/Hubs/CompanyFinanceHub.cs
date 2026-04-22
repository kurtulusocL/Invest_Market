using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CompanyFinanceHub : Hub
    {
        readonly ICompanyFinanceService _companyFinanceService;
        public CompanyFinanceHub(ICompanyFinanceService companyFinanceService)
        {
            _companyFinanceService = companyFinanceService;
        }
        public async Task<IEnumerable<CompanyFinanceDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _companyFinanceService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CompanyFinanceDto
                    {
                        Id = i.Id,
                        MarketValue = i.MarketValue,
                        ARRIncome = i.ARRIncome,
                        TotalIncome = i.TotalIncome,
                        HitCount = i.Hits?.Count ?? 0,
                        CompanyDtoId = i.CompanyId,
                        VisibilitySettingDtoId = i.VisibilitySettingId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CompanyFinanceDto>();
            }
            catch (Exception)
            {
                return new List<CompanyFinanceDto>();
            }
        }
    }
}
