
namespace Investigation.ServerHub.Dtos
{
    public class HitDto
    {
        public int Id { get; set; }
        public int CurrentValue { get; set; } = 0;

        public string AppUserDtoId { get; set; }
        public int? AdDtoId { get; set; }
        public int? AnnouncementDtoId { get; set; }
        public int? BlogDtoId { get; set; }
        public int? CommentDtoId { get; set; }
        public int? CommentAnswerDtoId { get; set; }
        public int? CompanyDtoId { get; set; }
        public int? CompanyFinanceDtoId { get; set; }
        public int? CompanyPintechDtoId { get; set; }
        public int? CompanyStageDtoId { get; set; }
        public int? InvestorDtoId { get; set; }
        public int? PostDtoId { get; set; }
        public int? SurveyDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual AdDto AdDto { get; set; }
        public virtual AnnouncementDto AnnouncementDto { get; set; }
        public virtual BlogDto BlogDto { get; set; }
        public virtual CommentDto CommentDto { get; set; }
        public virtual CommentAnswerDto CommentAnswerDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
        public virtual CompanyFinanceDto CompanyFinanceDto { get; set; }
        public virtual CompanyPintechDto CompanyPintechDto { get; set; }
        public virtual CompanyStageDto CompanyStageDto { get; set; }
        public virtual InvestorDto InvestorDto { get; set; }
        public virtual PostDto PostDto { get; set; }
        public virtual SurveyDto SurveyDto { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
