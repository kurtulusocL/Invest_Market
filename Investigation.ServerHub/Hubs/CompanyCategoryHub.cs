using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CompanyCategoryHub : Hub
    {
        readonly ICompanyCategoryService _companyCategoryService;
        public CompanyCategoryHub(ICompanyCategoryService companyCategoryService)
        {
            _companyCategoryService = companyCategoryService;
        }
        public async Task<IEnumerable<CompanyCategoryDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _companyCategoryService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new CompanyCategoryDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        CompanyCount = i.Companies?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<CompanyCategoryDto>();
            }
            catch (Exception)
            {
                return new List<CompanyCategoryDto>();
            }
        }
    }
}
