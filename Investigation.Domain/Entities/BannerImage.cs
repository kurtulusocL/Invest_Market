using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class BannerImage : BaseEntity
    {
        public string? Title { get; set; }
        public string ControllerName { get; set; }
        public string Image { get; set; }
    }
}
