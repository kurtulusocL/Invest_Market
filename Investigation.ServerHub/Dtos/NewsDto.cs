
namespace Investigation.ServerHub.Dtos
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Detail { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public int Hit { get; set; } = 0;
        public int Like { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<ReportDto> ReportDtos { get; set; }
        public int ReportCount { get; set; }
    }
}
