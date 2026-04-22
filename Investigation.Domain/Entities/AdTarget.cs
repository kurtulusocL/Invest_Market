using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class AdTarget : BaseEntity
    {
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? TargetCountries { get; set; }
        public string? TargetCategoryType { get; set; }
        public string? TargetCategoryIdsJson { get; set; }

        [NotMapped]
        public List<int>? TargetCategoryIds
        {
            get
            {
                if (string.IsNullOrWhiteSpace(TargetCategoryIdsJson))
                    return null;

                try
                {
                    return JsonSerializer.Deserialize<List<int>>(TargetCategoryIdsJson);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                TargetCategoryIdsJson = value != null
                    ? JsonSerializer.Serialize(value)
                    : null;
            }
        }
        public int MinInteractionCount { get; set; }
        public int? MinTotalLikeCount { get; set; }
        public int? MinTotalSaveCount { get; set; }
        public int? MinTotalViewCount { get; set; }
        public bool IncludeBlogInteractions { get; set; }
        public bool IncludeInvestorInteractions { get; set; }
        public bool IncludeCompanyInteractions { get; set; }
        public bool IncludePostInteractions { get; set; }

        public int AdId { get; set; }
        public virtual Ad Ad { get; set; }
    }
}
