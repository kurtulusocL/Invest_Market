
namespace Investigation.ServerHub.Dtos
{
    public class LikeDto
    {
        public int Id { get; set; }
        public int CurrentValue { get; set; } = 0;
        public bool IsLiked { get; set; }

        public string AppUserDtoId { get; set; }
        public int? BlogDtoId { get; set; }
        public int? CommentDtoId { get; set; }
        public int? CommentAnswerDtoId { get; set; }
        public int? CompanyDtoId { get; set; }
        public int? InvestorDtoId { get; set; }
        public int? PostDtoId { get; set; }
        public int? SurveyDtoId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual BlogDto BlogDto { get; set; }
        public virtual CommentDto CommentDto { get; set; }
        public virtual CommentAnswerDto CommentAnswerDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
        public virtual InvestorDto InvestorDto { get; set; }
        public virtual PostDto PostDto { get; set; }
        public virtual SurveyDto SurveyDto { get; set; }
    }
}
