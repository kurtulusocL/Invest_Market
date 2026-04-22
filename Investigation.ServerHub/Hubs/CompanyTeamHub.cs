using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CompanyTeamHub : Hub
    {
        readonly ICompanyTeamService _companyTeamService;
        public CompanyTeamHub(ICompanyTeamService companyTeamService)
        {
            _companyTeamService = companyTeamService;
        }
        public async Task<IEnumerable<CompanyTeamDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _companyTeamService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CompanyTeamDto
                    {
                        Id = i.Id,
                        CompanyDtoId = i.CompanyId,
                        NameSurname = i.NameSurname,
                        Email = i.Email,
                        Title = i.Title,
                        TotalExperienceDuration = i.TotalExperienceDuration,
                        PhotoUrl = i.PhotoUrl,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CompanyTeamDto>();
            }
            catch (Exception)
            {
                return new List<CompanyTeamDto>();
            }
        }
    }
}
