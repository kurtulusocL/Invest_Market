using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class SocialMedia : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string IconUrl { get; set; }
    }
}
