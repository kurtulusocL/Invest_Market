
namespace Investigation.ServerHub.Dtos
{
    public class CompanyCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<CompanyDto> CompaniesDto { get; set; }
        public int CompanyCount { get; set; }
    }
}
