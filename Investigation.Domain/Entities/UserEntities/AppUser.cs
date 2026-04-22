using Investigation.Shared.Domain;
using Microsoft.AspNetCore.Identity;

namespace Investigation.Domain.Entities.UserEntities
{
    public class AppUser : IdentityUser, IEntity
    {
        public string NameSurname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsInvestor { get; set; }
        public bool IsCompany { get; set; }
        public bool IsFollowable { get; set; } = true;
        public int? ConfirmCode { get; set; }
        public string? EncryptedEmail { get; set; }
        public string? EncryptedUserName { get; set; }
        public string? EncryptedBirthdate { get; set; }
        public bool IsAcceptedPolicies { get; set; }
        public bool IsLoginConfirmCodeActive { get; set; } = false;
        public bool IsRegisterConfirmCodeActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? SuspendedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<CancelMembership> CancelMemberships { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CommentAnswer> CommentAnswers { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Follow> MyFollowings { get; set; } = new HashSet<Follow>();
        public virtual ICollection<Follow> MyFollowers { get; set; } = new HashSet<Follow>();
        public virtual ICollection<Hit> Hits { get; set; }
        public virtual ICollection<Investor> Investors { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<ProfileImage> ProfileImages { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SavedContent> SavedContents { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }
        public virtual ICollection<SurveyResponse> SurveyResponses { get; set; }
        public virtual ICollection<UserProfileImage> UserProfileImages { get; set; }
        public virtual ICollection<UserSession> UserSessions { get; set; }

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
        public ICollection<MessageUserBlockList> MessageUserBlockedUsers { get; set; } = new List<MessageUserBlockList>();
        public ICollection<MessageUserBlockList> MessageUserBlockedByUsers { get; set; } = new List<MessageUserBlockList>();

        public AppUser()
        {
            EmailConfirmed = true;
            PhoneNumberConfirmed = true;
        }
    }
}
