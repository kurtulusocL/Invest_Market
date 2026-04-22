using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Slider : BaseEntity
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string ImageUrl { get; set; }
    }
}
