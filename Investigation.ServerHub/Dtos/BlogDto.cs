
namespace Investigation.ServerHub.Dtos
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string? Detail { get; set; }
        public string Content { get; set; }
        public string CoverImage { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public string AppUserDtoId { get; set; }
        public int BlogCategoryDtoId { get; set; }
        public int? CompanyDtoId { get; set; }
        public int? InvestorDtoId { get; set; }

        public virtual AppUserDto AppUserDto { get; set; }
        public virtual BlogCategoryDto BlogCategoryDto { get; set; }
        public virtual CompanyDto CompanyDto { get; set; }
        public virtual InvestorDto InvestorDto { get; set; }
        public virtual ICollection<CommentDto> CommentsDto { get; set; }       
        public virtual ICollection<HitDto> HitsDto { get; set; }       
        public virtual ICollection<LikeDto> LikesDto { get; set; }       
        public virtual ICollection<PictureDto> PicturesDto { get; set; }        
        public virtual ICollection<ReportDto> ReportsDto { get; set; } 
        public virtual ICollection<SavedContentDto> SavedContentsDto { get; set; }

        public int CommentCount { get; set; }
        public int HitCount { get; set; }
        public int LikeCount { get; set; }
        public int PictureCount { get; set; }
        public int ReportCount { get; set; }
        public int SavedContentCount { get; set; }
    }
}
