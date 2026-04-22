
namespace Investigation.ServerHub.Dtos
{
    public class UserSessionDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public bool IsOnline { get; set; }
        public int? OnlineDurationSeconds { get; set; }
        public DateTime? LastHeartbeat { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public virtual AppUserDto AppUserDto { get; set; }
    }
}
