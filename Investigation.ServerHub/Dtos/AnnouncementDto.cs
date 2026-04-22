
namespace Investigation.ServerHub.Dtos
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int AnnouncementCategoryDtoId { get; set; }
        public int? InvestorDtoId { get; set; }
        public int? CompanyDtoId { get; set; }

        public virtual AnnouncementCategoryDto AnnouncementCategoryDto { get; set; }
        public virtual InvestorDto InvestorDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }

        public virtual ICollection<HitDto> HitsDto { get; set; }       
        public virtual ICollection<ReportDto> ReportsDto { get; set; }

        public int HitCount { get; set; }
        public int ReportCount { get; set; }
    }
}
