using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class ContactHub : Hub
    {
        readonly IContactService _contactService;
        public ContactHub(IContactService contactService)
        {
            _contactService = contactService;
        }
        public async Task<IEnumerable<ContactDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _contactService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new ContactDto
                    {
                        Id = i.Id,
                        BusinessEmail = i.BusinessEmail,
                        OtherEmail = i.OtherEmail,
                        PhoneNumber = i.PhoneNumber,
                        Location = i.Location,
                        LocationMap = i.LocationMap,
                        Mernis = i.Mernis,
                        KEPAddress = i.KEPAddress,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<ContactDto>();
            }
            catch (Exception)
            {
                return new List<ContactDto>();
            }
        }
    }
}
