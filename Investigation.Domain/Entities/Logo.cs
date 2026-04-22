using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Logo : BaseEntity
    {
        public string UseFor { get; set; }
        public string ImageUrl { get; set; }
    }
}
