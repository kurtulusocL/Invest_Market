using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class BlogCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
