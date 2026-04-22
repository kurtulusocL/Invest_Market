
namespace Investigation.ServerHub.Dtos
{
    public class CommentAnswerDto
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
        public int? CommentDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual CommentDto CommentDto { get; set; }

        public virtual ICollection<HitDto> HitsDto { get; set; }       
        public virtual ICollection<LikeDto> LikesDto { get; set; }       
        public virtual ICollection<ReportDto> ReportsDto { get; set; }

        public int HitCount { get; set; }
        public int LikeCount { get; set; }
        public int ReportCount { get; set; }
    }
}
