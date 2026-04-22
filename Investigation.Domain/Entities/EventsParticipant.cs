using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class EventsParticipant : BaseEntity
    {
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public DateTime JoinTime { get; set; }
        public string? ShortDescription { get; set; }
        public string ImageUrl { get; set; }

        public int? EventsId { get; set; }
        public virtual Events Events { get; set; }
    }
}
