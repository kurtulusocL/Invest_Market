using Investigation.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Services.Abstract
{
    public interface IWhatWeOfferService
    {
        IQueryable<WhatWeOffer> GetAllAsync();
        IQueryable<WhatWeOffer> GetAllForAdminAsync();
        Task<IEnumerable<WhatWeOffer>> GetAllForSignalRAsync();
        Task<WhatWeOffer> GetByIdAsync(int? id);
        Task<bool> CreateAsync(WhatWeOffer entity, IFormFile? image);
        Task<bool> UpdateAsync(WhatWeOffer entity, IFormFile? image);
        Task<bool> DeleteAsync(WhatWeOffer entity, int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<WhatWeOffer> GetAllWhatWeOfferForPublic();
        IQueryable<WhatWeOffer> GetAllWhatWeOfferForPublicHome();
        IQueryable<WhatWeOffer> GetAllForSitemap();
    }
}
