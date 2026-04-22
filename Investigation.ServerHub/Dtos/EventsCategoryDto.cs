
namespace Investigation.ServerHub.Dtos
{
    public class EventsCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<EventsDto> EventsDto { get; set; }
        public int EventsCount { get; set; }
    }
}
