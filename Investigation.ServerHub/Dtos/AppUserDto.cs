

namespace Investigation.ServerHub.Dtos
{
    public class AppUserDto
    {
        public string Id { get; set; }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime Birthdate { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsInvestor { get; set; }
        public bool IsCompany { get; set; }
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
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<BlogDto> BlogsDto { get; set; }
        public virtual ICollection<CancelMembershipDto> CancelMembershipsDto { get; set; }
        public virtual ICollection<CommentDto> CommentsDto { get; set; }
        public virtual ICollection<CommentAnswerDto> CommentAnswersDto { get; set; }
        public virtual ICollection<CompanyDto> CompaniesDto { get; set; }
        public virtual ICollection<HitDto> HitsDto { get; set; }
        public virtual ICollection<InvestorDto> InvestorsDto { get; set; }
        public virtual ICollection<LikeDto> LikesDto { get; set; }
        public virtual ICollection<PostDto> PostsDto { get; set; }
        public virtual ICollection<ProfileImageDto> ProfileImagesDto { get; set; }
        public virtual ICollection<ReportDto> ReportsDto { get; set; }
        public virtual ICollection<SavedContentDto> SavedContentsDto { get; set; }
        public virtual ICollection<SurveyDto> SurveysDto { get; set; }
        public virtual ICollection<SurveyAnswerDto> SurveyAnswersDto { get; set; }
        public virtual ICollection<SurveyResponseDto> SurveyResponsesDto { get; set; }
        public virtual ICollection<UserProfileImageDto> UserProfileImagesDto { get; set; }
        public virtual ICollection<UserSessionDto> UserSessionsDto { get; set; }
        public ICollection<MessageDto> SentMessagesDto { get; set; } = new List<MessageDto>();
        public ICollection<MessageDto> ReceivedMessagesDto { get; set; } = new List<MessageDto>();
        public ICollection<MessageUserBlockListDto> MessageUserBlockedUsersDto { get; set; } = new List<MessageUserBlockListDto>();
        public ICollection<MessageUserBlockListDto> MessageUserBlockedByUsersDto { get; set; } = new List<MessageUserBlockListDto>();

        public int BlogCount { get; set; }
        public int CancelMembershipCount { get; set; }
        public int CommentCount { get; set; }
        public int CommentAnswerCount { get; set; }
        public int CompanyCount { get; set; }
        public int HitCount { get; set; }
        public int InvestorCount { get; set; }
        public int LikeCount { get; set; }
        public int PostCount { get; set; }
        public int ProfileImageCount { get; set; }
        public int ReportCount { get; set; }
        public int SavedContentCount { get; set; }
        public int SurveyCount { get; set; }
        public int SurveyAnswerCount { get; set; }
        public int SurveyReponseCount { get; set; }
        public int UserProfileImageCount { get; set; }
        public int UserSessionCount { get; set; }
        public int SentMessageCount { get; set; }
        public int RecievedMessageCount { get; set; }
        public int BlockedMessageCount { get; set; }
        public int BlockedUserCount { get; set; }
    }
}