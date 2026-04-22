
namespace Investigation.ServerHub.Dtos
{
    public class EventsDto
    {
        public int Id { get; set; }
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
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int EventsCategoryDtoId { get; set; }
        public virtual EventsCategoryDto EventsCategoryDto { get; set; }

        public virtual ICollection<EventsParticipantDto> EventsParticipantsDto { get; set; }
        public int ParticipantCount { get; set; }
    }
}
