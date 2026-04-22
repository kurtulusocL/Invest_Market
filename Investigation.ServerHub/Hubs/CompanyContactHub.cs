using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CompanyContactHub : Hub
    {
        readonly ICompanyContactService _companyContactService;
        public CompanyContactHub(ICompanyContactService companyContactService)
        {
            _companyContactService = companyContactService;
        }
        public async Task<IEnumerable<CompanyContactDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _companyContactService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CompanyContactDto
                    {
                        Id = i.Id,
                        Website = i.Website,
                        PhoneNumber = i.PhoneNumber,
                        Email = i.Email,
                        Location = i.Location,
                        CompanyDtoId = i.CompanyId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CompanyContactDto>();
            }
            catch (Exception)
            {
                return new List<CompanyContactDto>();
            }
        }
    }
}
