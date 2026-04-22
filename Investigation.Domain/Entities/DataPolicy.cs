using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class DataPolicy : BaseEntity
    {
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Desc { get; set; }
    }
}
