
namespace Investigation.ServerHub.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public bool IsFixed { get; set; }
        public DateTime? FixedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public int ReportCategoryDtoId { get; set; }
        public int? AnnouncementDtoId { get; set; }
        public int? BlogDtoId { get; set; }
        public int? CommentDtoId { get; set; }
        public int? CommentAnswerDtoId { get; set; }
        public int? CompanyDtoId { get; set; }
        public int? InvestorDtoId { get; set; }
        public int? NewsDtoId { get; set; }
        public int? PostDtoId { get; set; }
        public int? SectorNewsDtoId { get; set; }
        public int? SurveyDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual ReportCategoryDto ReportCategoryDto { get; set; }
        public virtual AnnouncementDto AnnouncementDto { get; set; }
        public virtual BlogDto Blog { get; set; }
        public virtual CommentDto CommentDto { get; set; }
        public virtual CommentAnswerDto CommentAnswerDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
        public virtual InvestorDto InvestorDto { get; set; }
        public virtual NewsDto NewsDto { get; set; }
        public virtual PostDto PostDto { get; set; }
        public virtual SectorNewsDto SectorNewsDto { get; set; }
        public virtual SurveyDto SurveyDto { get; set; }

    }
}
