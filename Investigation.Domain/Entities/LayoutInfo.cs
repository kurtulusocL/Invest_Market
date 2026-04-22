using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class LayoutInfo:BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Keyword { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
    }
}
