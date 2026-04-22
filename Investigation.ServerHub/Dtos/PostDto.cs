
namespace Investigation.ServerHub.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsCommentable { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public int? CompanyDtoId { get; set; }
        public int? InvestorDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
        public virtual InvestorDto InvestorDto { get; set; }

        public virtual ICollection<CommentDto> CommentsDto { get; set; }
        public int CommentCount { get; set; }
        public virtual ICollection<HitDto> HitsDto { get; set; }
        public int HitCount { get; set; }
        public virtual ICollection<LikeDto> LikesDto { get; set; }
        public int LikeCount { get; set; }
        public virtual ICollection<PictureDto> PicturesDto { get; set; }
        public int PictureCount { get; set; }
        public virtual ICollection<ReportDto> ReportsDto { get; set; }
        public int ReportCount { get; set; }
        public virtual ICollection<SavedContentDto> SavedContentsDto { get; set; }
        public int SavedContentCount { get; set; }
    }
}
