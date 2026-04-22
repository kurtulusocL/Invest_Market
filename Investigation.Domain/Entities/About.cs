using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class About : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? Detail { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
    }
}
