using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class InvestorCategoryHub : Hub
    {
        readonly IInvestorCategoryService _investorCategoryService;
        public InvestorCategoryHub(IInvestorCategoryService investorCategoryService)
        {
            _investorCategoryService = investorCategoryService;
        }
        public async Task<IEnumerable<InvestorCategoryDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _investorCategoryService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new InvestorCategoryDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        InvestorCount = i.Investors?.Count ?? 0,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    });
                }
                return new List<InvestorCategoryDto>();
            }
            catch (Exception)
            {
                return new List<InvestorCategoryDto>();
            }
        }
    }
}
