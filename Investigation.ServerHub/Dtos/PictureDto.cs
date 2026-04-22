
namespace Investigation.ServerHub.Dtos
{
    public class PictureDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? BlogDtoId { get; set; }
        public int? CompanyDtoId { get; set; }
        public int? PostDtoId { get; set; }

        public virtual BlogDto BlogDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
        public virtual PostDto PostDto { get; set; }

    }
}
