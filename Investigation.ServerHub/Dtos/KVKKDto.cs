

namespace Investigation.ServerHub.Dtos
{
    public class KVKKDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Desc { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
