using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CountryHub : Hub
    {
        readonly ICountryService _countryService;
        public CountryHub(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public async Task<IEnumerable<CountryDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _countryService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CountryDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        CompanyCount = i.Companies?.Count ?? 0,
                        InvestorCount = i.Investors?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CountryDto>();
            }
            catch (Exception)
            {
                return new List<CountryDto>();
            }
        }
    }
}
