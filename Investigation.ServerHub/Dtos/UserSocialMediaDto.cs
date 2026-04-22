
namespace Investigation.ServerHub.Dtos
{
    public class UserSocialMediaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public int? CompanyDtoId { get; set; }
        public int? InvestorDtoId { get; set; }

        public virtual CompanyDto CompanyDto { get; set; }
        public virtual InvestorDto InvestorDto { get; set; }
    }
}
