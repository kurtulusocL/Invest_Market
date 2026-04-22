
namespace Investigation.ServerHub.Dtos
{
    public class AdTargetDto
    {
        public int Id { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? TargetCountries { get; set; }
        public string? TargetCategoryType { get; set; }
        public string? TargetCategoryIdsJson { get; set; }
        public List<int>? TargetCategoryIds { get; set; }
        public int MinInteractionCount { get; set; }
        public int? MinTotalLikeCount { get; set; }
        public int? MinTotalSaveCount { get; set; }
        public int? MinTotalViewCount { get; set; }
        public bool IncludeBlogInteractions { get; set; }
        public bool IncludeInvestorInteractions { get; set; }
        public bool IncludeCompanyInteractions { get; set; }
        public bool IncludePostInteractions { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int AdDtoId { get; set; }
        public virtual AdDto AdDto { get; set; }
    }
}
