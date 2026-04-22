
namespace Investigation.ServerHub.Dtos
{
    public class CompanyPintechDto
    {
        public int Id { get; set; }
        public string WorkPlan { get; set; }
        public string ServiceProduct { get; set; }
        public string Description { get; set; }
        public string MarketingStrategy { get; set; }
        public string GrowingPotantial { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? CompanyDtoId { get; set; }
        public int? VisibilitySettingDtoId { get; set; }

        public virtual CompanyDto CompanyDto { get; set; }
        public virtual VisibilitySettingDto VisibilitySettingDto { get; set; }

        public virtual ICollection<HitDto> HitsDto { get; set; }
        public int HitCount { get; set; }
    }
}
