using Investigation.Business.Services.Abstract;
using Investigation.ServerHub.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Investigation.ServerHub.Hubs
{
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class SavedContentHub : Hub
    {
        readonly ISavedContentService _savedContentService;
        public SavedContentHub(ISavedContentService savedContentService)
        {
            _savedContentService = savedContentService;
        }
        public async Task<IEnumerable<SavedContentDto>> GetAllIncludingAsync()
        {
            try
            {
                var data = await _savedContentService.GetAllForSignalRAsync();
                if (data != null)
                {
                    return data.Select(i => new SavedContentDto
                    {
                        Id = i.Id,
                        IsSaved = i.IsSaved,
                        SaveDate = i.SaveDate,
                        DisSaveDate = i.DisSaveDate,
                        AppUserDtoId = i.AppUserId,
                        BlogDtoId = i.BlogId,
                        SectorNewsDtoId = i.SectorNewsId,
                        CompanyDtoId = i.CompanyId,
                        InvestorDtoId = i.InvestorId,
                        PostDtoId = i.PostId,
                        SurveyDtoId = i.SurveyId,
                        CreatedDate = i.CreatedDate,
                        UpdatedDate = i.UpdatedDate,
                        SuspendedDate = i.SuspendedDate,
                        DeletedDate = i.DeletedDate,
                        IsActive = i.IsActive,
                        IsDeleted = i.IsDeleted
                    }).ToList();
                }
                return new List<SavedContentDto>();
            }
            catch (Exception)
            {
                return new List<SavedContentDto>();
            }
        }
    }
}
