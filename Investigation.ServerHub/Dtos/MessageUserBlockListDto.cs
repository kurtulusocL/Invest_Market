
namespace Investigation.ServerHub.Dtos
{
    public class MessageUserBlockListDto
    {
        public int Id { get; set; }
        public bool IsBlocked { get; set; } = true;
        public bool IsRemoved { get; set; } = false;
        public DateTime BlockedAt { get; set; } = DateTime.UtcNow;
        public string BlockedUserName { get; set; }       
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string BlockerDtoId { get; set; }
        public string BlockedDtoId { get; set; }

        public AppUserDto BlockerDto { get; set; }
        public AppUserDto BlockedDto { get; set; }

    }
}
