
namespace Investigation.ServerHub.Dtos
{
    public class EventsParticipantDto
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public DateTime JoinTime { get; set; }
        public string? ShortDescription { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? EventsDtoId { get; set; }
        public virtual EventsDto EventsDto { get; set; }
    }
}
