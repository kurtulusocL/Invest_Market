
namespace Investigation.ServerHub.Dtos
{
    public class SectorNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Desc { get; set; }
        public string? Detail { get; set; }
        public string? RedirectUrl { get; set; }
        public string? Source { get; set; }
        public string ImageUrl { get; set; }
        public int Like { get; set; } = 0;
        public int Hit { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<ReportDto> ReportsDto { get; set; }        
        public virtual ICollection<SavedContentDto> SavedContentsDto { get; set; }

        public int ReportCount { get; set; }
        public int SavedContentCount { get; set; }
    }
}
