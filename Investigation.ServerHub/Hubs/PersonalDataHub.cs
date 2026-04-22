using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class PersonalDataHub : Hub
    {
        readonly IPersonalDataService _personalDataService;
        public PersonalDataHub(IPersonalDataService personalDataService)
        {
            _personalDataService = personalDataService;
        }
        public async Task<IEnumerable<PersonalDataDto>> GetAllAsync()
        {
            try
            {
                var data = await _personalDataService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new PersonalDataDto
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
                return new List<PersonalDataDto>();
            }
            catch (Exception)
            {
                return new List<PersonalDataDto>();
            }
        }
    }
}
