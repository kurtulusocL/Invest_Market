using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class PictureHub : Hub
    {
        readonly IPictureService _pictureService;
        public PictureHub(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        public async Task<IEnumerable<PictureDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _pictureService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new PictureDto
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        BlogDtoId = i.BlogId,
                        CompanyDtoId = i.CompanyId,
                        PostDtoId = i.PostId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<PictureDto>();
            }
            catch (Exception)
            {
                return new List<PictureDto>();
            }
        }
    }
}
