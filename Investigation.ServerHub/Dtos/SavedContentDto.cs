
namespace Investigation.ServerHub.Dtos
{
    public class SavedContentDto
    {
        public int Id { get; set; }
        public bool IsSaved { get; set; }
        public DateTime SaveDate { get; set; }
        public DateTime? DisSaveDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public int? BlogDtoId { get; set; }
        public int? SectorNewsDtoId { get; set; }
        public int? CompanyDtoId { get; set; }
        public int? InvestorDtoId { get; set; }
        public int? PostDtoId { get; set; }
        public int? SurveyDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual BlogDto BlogDto { get; set; }
        public virtual SectorNewsDto SectorNewsDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
        public virtual InvestorDto InvestorDto { get; set; }
        public virtual PostDto PostDto { get; set; }
        public virtual SurveyDto SurveyDto { get; set; }

    }
}
