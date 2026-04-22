
namespace Investigation.ServerHub.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public int? BlogDtoId { get; set; }
        public int? CompanyDtoId { get; set; }
        public int? PostDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual BlogDto BlogDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
        public virtual PostDto PostDto { get; set; }

        public virtual ICollection<HitDto> HitsDto { get; set; }       
        public virtual ICollection<CommentAnswerDto> CommentAnswersDto { get; set; }       
        public virtual ICollection<LikeDto> LikesDto { get; set; }       
        public virtual ICollection<ReportDto> ReportsDto { get; set; }

        public int HitCount { get; set; }
        public int CommentAnswerCount { get; set; }
        public int LikeCount { get; set; }
        public int ReportCount { get; set; }
    }
}
