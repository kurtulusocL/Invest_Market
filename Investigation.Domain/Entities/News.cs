using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Detail { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public int Hit { get; set; } = 0;
        public int Like { get; set; } = 0;

        public virtual ICollection<Report> Reports { get; set; }
    }
}
