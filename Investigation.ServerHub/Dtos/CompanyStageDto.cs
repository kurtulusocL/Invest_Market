
namespace Investigation.ServerHub.Dtos
{
    public class CompanyStageDto
    {
        public int Id { get; set; }
        public string StageName { get; set; }
        public decimal StageValue { get; set; }
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
