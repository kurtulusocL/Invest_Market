using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Events : BaseEntity
    {
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public DateTime StartedDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOnline { get; set; }
        public int DurationDay { get; set; }
        public string Content { get; set; }
        public string Location { get; set; }
        public string? RedirectUrl { get; set; }
        public string? ImageUrl { get; set; }
        public int Hit { get; set; } = 0;
        public int Like { get; set; } = 0;

        public int EventsCategoryId { get; set; }
        public virtual EventsCategory EventsCategory { get; set; }

        public virtual ICollection<EventsParticipant> EventsParticipants { get; set; }
    }
}
