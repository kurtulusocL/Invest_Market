using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Picture : BaseEntity
    {
        public string ImageUrl { get; set; }

        public int? BlogId { get; set; }
        public int? CompanyId { get; set; }
        public int? PostId { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Company Company { get; set; }
        public virtual Post Post { get; set; }
    }
}
