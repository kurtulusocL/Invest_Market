using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class EventsCategory : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Events> Events { get; set; }
    }
}
