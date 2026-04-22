
namespace Investigation.ServerHub.Dtos
{
    public class BannerImageDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string ControllerName { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
