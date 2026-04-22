
namespace Investigation.ServerHub.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string SenderDtoId { get; set; }
        public string ReceiverDtoId { get; set; }

        public AppUserDto SenderDto { get; set; }
        public AppUserDto ReceiverDto { get; set; }
    }
}
