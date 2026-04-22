using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Domain.EntityFramework;

namespace Investigation.Domain.Entities
{
    public class Like : BaseEntity
    {
        public int CurrentValue { get; set; } = 0;
        public bool IsLiked { get; set; }

        public string AppUserId { get; set; }
        public int? BlogId { get; set; }
        public int? CommentId { get; set; }
        public int? CommentAnswerId { get; set; }
        public int? CompanyId { get; set; }
        public int? InvestorId { get; set; }
        public int? PostId { get; set; }
        public int? SurveyId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual CommentAnswer CommentAnswer { get; set; }
        public virtual Company Company { get; set; }
        public virtual Investor Investor { get; set; }
        public virtual Post Post { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
