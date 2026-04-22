using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SectorNews:BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Desc { get; set; }
        public string? Detail { get; set; }
        public string? RedirectUrl { get; set; }
        public string? Source { get; set; }
        public string ImageUrl { get; set; }
        public int Like { get; set; } = 0;
        public int Hit { get; set; } = 0;

        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SavedContent> SavedContents { get; set; }
    }
}
