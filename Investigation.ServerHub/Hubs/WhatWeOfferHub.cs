using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class WhatWeOfferHub : Hub
    {
        readonly IWhatWeOfferService _whatWeOfferService;
        public WhatWeOfferHub(IWhatWeOfferService whatWeOfferService)
        {
            _whatWeOfferService = whatWeOfferService;
        }
        public async Task<IEnumerable<WhatWeOfferDto>> GetAllAsync()
        {
            try
            {
                var data = await _whatWeOfferService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new WhatWeOfferDto
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
                return new List<WhatWeOfferDto>();
            }
            catch (Exception)
            {
                return new List<WhatWeOfferDto>();
            }
        }
    }
}
