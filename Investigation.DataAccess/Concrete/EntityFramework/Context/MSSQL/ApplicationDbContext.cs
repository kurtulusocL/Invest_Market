using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdTarget> AdTargets { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementCategory> AnnouncementCategories { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<BannerImage> BannerImages { get; set; }
        public DbSet<BlackList> BlackLists { get; set; }
        public DbSet<Blocked> Blockeds { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<CancelMembership> CancelMemberships { get; set; }
        public DbSet<CancelMembershipCategory> CancelMembershipCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentAnswer> CommentAnswers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyCategory> CompanyCategories { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<CompanyFinance> CompanyFinances { get; set; }
        public DbSet<CompanyPintech> CompanyPinteches { get; set; }
        public DbSet<CompanyStage> CompanyStages { get; set; }
        public DbSet<CompanyTeam> CompanyTeams { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DataPolicy> DataPolicies { get; set; }
        public DbSet<Events> Eventses { get; set; }
        public DbSet<EventsCategory> EventCategories { get; set; }
        public DbSet<EventsParticipant> EventsParticipants { get; set; }
        public DbSet<ExceptionLogger> ExceptionLoggers { get; set; }
        public DbSet<Frequently> Frequentlies { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Hit> Hits { get; set; }
        public DbSet<HowItWorks> HowItWorkses { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<InvestorCategory> InvestorCategories { get; set; }
        public DbSet<KVKK> KVKKs { get; set; }
        public DbSet<LayoutInfo> LayoutInfos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Logo> Logos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageUserBlockList> MessageUserBlockLists { get; set; }
        public DbSet<News> Newses { get; set; }
        public DbSet<PersonalData> PersonalDatas { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<RecentlyInvest> RecentlyInvests { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportCategory> ReportCategories { get; set; }
        public DbSet<SavedContent> SavedContents { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<SectorNews> SectorNews { get; set; }
        public DbSet<SecuritySetting> SecuritySettings { get; set; }
        public DbSet<SendMessage> SendMessages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<SubSector> SubSectors { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyAnalytics> SurveyAnalytics { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<UserAgreement> UserAgreements { get; set; }
        public DbSet<UserProfileImage> UserProfileImages { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserSocialMedia> UserSocialMedias { get; set; }
        public DbSet<VisibilitySetting> VisibilitySettings { get; set; }
        public DbSet<WhatWeOffer> WhatWeOffers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var navigation in entityType.GetNavigations())
                {
                    navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
                }
            }

            modelBuilder.Entity<Blog>().HasOne(us => us.AppUser).WithMany(u => u.Blogs).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CancelMembership>().HasOne(us => us.AppUser).WithMany(u => u.CancelMemberships).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>().HasOne(us => us.AppUser).WithMany(u => u.Comments).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CommentAnswer>().HasOne(us => us.AppUser).WithMany(u => u.CommentAnswers).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Company>().HasOne(us => us.AppUser).WithMany(u => u.Companies).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Follow>().HasOne(f => f.FollowedCompany).WithMany(c => c.CompanyFollowers).HasForeignKey(f => f.FollowedCompanyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Follow>().HasOne(f => f.FollowerCompany).WithMany(c => c.CompanyFollowings).HasForeignKey(f => f.FollowerCompanyId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Follow>().HasOne(f => f.FollowerUser).WithMany(u => u.MyFollowings).HasForeignKey(f => f.FollowerUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Follow>().HasOne(f => f.FollowedUser).WithMany(u => u.MyFollowers).HasForeignKey(f => f.FollowedUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Hit>().HasOne(us => us.AppUser).WithMany(u => u.Hits).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Investor>().HasOne(us => us.AppUser).WithMany(u => u.Investors).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Like>().HasOne(us => us.AppUser).WithMany(u => u.Likes).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Post>().HasOne(us => us.AppUser).WithMany(u => u.Posts).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProfileImage>().HasOne(us => us.AppUser).WithMany(u => u.ProfileImages).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Report>().HasOne(us => us.AppUser).WithMany(u => u.Reports).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SavedContent>().HasOne(us => us.AppUser).WithMany(u => u.SavedContents).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Survey>().HasOne(us => us.AppUser).WithMany(u => u.Surveys).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SurveyAnswer>().HasOne(us => us.AppUser).WithMany(u => u.SurveyAnswers).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SurveyResponse>().HasOne(us => us.AppUser).WithMany(u => u.SurveyResponses).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserProfileImage>().HasOne(us => us.AppUser).WithMany(u => u.UserProfileImages).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserSession>().HasOne(us => us.AppUser).WithMany(u => u.UserSessions).HasForeignKey(us => us.AppUserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>().HasOne(m => m.Sender).WithMany(u => u.SentMessages).HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>().HasOne(m => m.Receiver).WithMany(u => u.ReceivedMessages).HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageUserBlockList>().HasOne(b => b.Blocker).WithMany(u => u.MessageUserBlockedUsers).HasForeignKey(b => b.BlockerId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MessageUserBlockList>().HasOne(b => b.Blocked).WithMany(u => u.MessageUserBlockedByUsers).HasForeignKey(b => b.BlockedId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AdTarget>().Property(t => t.TargetCategoryIdsJson).HasColumnName("TargetCategoryIds");

            modelBuilder.Entity<About>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_About_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_About_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Ad>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Ad_Id").IsUnique();
                entity.HasIndex(e => new { e.StartDate, e.FinishDate }).HasDatabaseName("IX_Ad_StartDate_FinishDate");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Ad_IsActive_IsDeleted");
            });

            modelBuilder.Entity<AdTarget>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_AdTarget_Id").IsUnique();
                entity.HasIndex(e => e.AdId).HasDatabaseName("IX_AdTarget_AdId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_AdTarget_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Announcement_Id").IsUnique();
                entity.HasIndex(e => e.AnnouncementCategoryId).HasDatabaseName("IX_Announcement_AnnouncementCategoryId");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_Announcement_InvestorId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Announcement_CompanyId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Announcement_IsActive_IsDeleted");
            });

            modelBuilder.Entity<AnnouncementCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_AnnouncementCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_AnnouncementCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<AppRole>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_AppRole_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_AppRole_IsActive_IsDeleted");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_AppUser_Id").IsUnique();
                entity.HasIndex(e => e.IsAdmin).HasDatabaseName("IX_AppUser_IsAdmin");
                entity.HasIndex(e => new { e.IsLoginConfirmCodeActive, e.IsRegisterConfirmCodeActive }).HasDatabaseName("IX_AppUser_IsLoginConfirmCodeActive_IsRegisterConfirmCodeActive");
                entity.HasIndex(e => new { e.CreatedDate, e.UpdatedDate, e.DeletedDate }).HasDatabaseName("IX_AppUser_CreatedDate_UpdatedDate_DeletedDate");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_AppUser_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Audit_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Audit_IsActive_IsDeleted");
                entity.HasIndex(e => new { e.RemoteIpAddress, e.IpAddressVPN }).HasDatabaseName("IX_Audit_RemoteIpAddress_IpAddressVPN");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Audit_IsActive_IsDeleted");
            });

            modelBuilder.Entity<BannerImage>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_BannerImage_Id").IsUnique();
                entity.HasIndex(e => e.ControllerName).HasDatabaseName("IX_BannerImage_ControllerName").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_BannerImage_IsActive_IsDeleted");
            });

            modelBuilder.Entity<BlackList>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_BlackList_Id").IsUnique();
                entity.HasIndex(e => e.AuditId).HasDatabaseName("IX_BlackList_AuditId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_BlackList_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Blocked>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Blocked_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Blocked_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Blog_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_Blog_AppUserId");
                entity.HasIndex(e => e.BlogCategoryId).HasDatabaseName("IX_Blog_BlogCategoryId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Blog_CompanyId");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_Blog_InvestorId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Blog_IsActive_IsDeleted");
            });

            modelBuilder.Entity<BlogCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_BlogCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_BlogCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CancelMembership>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CancelMembership_Id").IsUnique();
                entity.HasIndex(e => e.CancelMembershipCategoryId).HasDatabaseName("IX_CancelMembership_CancelMembershipCategoryId");
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_CancelMembership_AppUserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CancelMembership_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CancelMembershipCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CancelMembershipCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CancelMembershipCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Comment_Id").IsUnique();
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Comment_BlogId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Comment_CompanyId");
                entity.HasIndex(e => e.PostId).HasDatabaseName("IX_Comment_PostId");
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_Comment_AppUserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Comment_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CommentAnswer>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CommentAnswer_Id").IsUnique();
                entity.HasIndex(e => e.CommentId).HasDatabaseName("IX_CommentAnswer_BlogId");
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_CommentAnswer_AppUserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CommentAnswer_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Company_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_Company_AppUserId");
                entity.HasIndex(e => e.CompanyCategoryId).HasDatabaseName("IX_Company_CompanyCategoryId");
                entity.HasIndex(e => e.CountryId).HasDatabaseName("IX_Company_CountryId");
                entity.HasIndex(e => e.SectorId).HasDatabaseName("IX_Company_SectorId");
                entity.HasIndex(e => e.SubSectorId).HasDatabaseName("IX_Company_SubSectorId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Company_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyContact>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyContact_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_CompanyContact_CompanyId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyContact_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyFinance>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyFinance_Id").IsUnique();
                entity.HasOne(e => e.VisibilitySetting).WithOne(i => i.CompanyFinance).HasForeignKey<VisibilitySetting>(i => i.CompanyFinanceId).IsRequired(false);
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_CompanyFinance_CompanyId");
                entity.HasIndex(e => e.VisibilitySettingId).HasDatabaseName("IX_CompanyFinance_VisibilitySettingId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyFinance_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyPintech>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyPintech_Id").IsUnique();
                entity.HasOne(e => e.VisibilitySetting).WithOne(i => i.CompanyPintech).HasForeignKey<VisibilitySetting>(i => i.CompanyPintechId).IsRequired(false);
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_CompanyPintech_CompanyId");
                entity.HasIndex(e => e.VisibilitySettingId).HasDatabaseName("IX_CompanyPintech_VisibilitySettingId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyPintech_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyStage>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyStage_Id").IsUnique();
                entity.HasOne(e => e.VisibilitySetting).WithOne(i => i.CompanyStage).HasForeignKey<VisibilitySetting>(i => i.CompanyStageId).IsRequired(false);
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_CompanyStage_CompanyId");
                entity.HasIndex(e => e.VisibilitySettingId).HasDatabaseName("IX_CompanyStage_VisibilitySettingId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyStage_IsActive_IsDeleted");
            });

            modelBuilder.Entity<CompanyTeam>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_CompanyTeam_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_CompanyTeam_CompanyId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_CompanyTeam_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Contact_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Contact_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Country_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Country_IsActive_IsDeleted");
            });

            modelBuilder.Entity<DataPolicy>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_DataPolicy_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_DataPolicy_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Events_Id").IsUnique();
                entity.HasIndex(e => e.EventsCategoryId).HasDatabaseName("IX_Events_EventsCategoryId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Events_IsActive_IsDeleted");
            });

            modelBuilder.Entity<EventsCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_EventsCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_EventsCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<EventsParticipant>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_EventsParticipant_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_EventsParticipant_IsActive_IsDeleted");
            });

            modelBuilder.Entity<ExceptionLogger>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_ExceptionLogger_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_ExceptionLogger_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Frequently>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Frequently_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Frequently_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Follow_Id").IsUnique();
                entity.HasIndex(e => e.FollowedUserId).HasDatabaseName("IX_Follow_FollowedUserId");
                entity.HasIndex(e => e.FollowerUserId).HasDatabaseName("IX_Follow_FollowerUserId");
                entity.HasIndex(e => e.FollowedCompanyId).HasDatabaseName("IX_Follow_FollowedCompanyId");
                entity.HasIndex(e => e.FollowedCompanyId).HasDatabaseName("IX_Follow_FollowedCompanyId");
                entity.HasIndex(e => e.IsFollowed).HasDatabaseName("IX_Follow_IsFollowed");
                entity.HasIndex(e => e.IsCanceled).HasDatabaseName("IX_Follow_IsCanceled");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Follow_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Hit>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Hit_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_Hit_AppUserId");
                entity.HasIndex(e => e.AdId).HasDatabaseName("IX_Hit_AdId");
                entity.HasIndex(e => e.AnnouncementId).HasDatabaseName("IX_Hit_AnnouncementId");
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Hit_BlogId");
                entity.HasIndex(e => e.CommentId).HasDatabaseName("IX_Hit_CommentId");
                entity.HasIndex(e => e.CommentAnswerId).HasDatabaseName("IX_Hit_CommentAnswerId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Hit_CompanyId");
                entity.HasIndex(e => e.CompanyFinanceId).HasDatabaseName("IX_Hit_CompanyFinanceId");
                entity.HasIndex(e => e.CompanyPintechId).HasDatabaseName("IX_Hit_CompanyPintechId");
                entity.HasIndex(e => e.CompanyStageId).HasDatabaseName("IX_Hit_CompanyStageId");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_Hit_InvestorId");
                entity.HasIndex(e => e.PostId).HasDatabaseName("IX_Hit_PostId");
                entity.HasIndex(e => e.SurveyId).HasDatabaseName("IX_Hit_SurveyId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Hit_IsActive_IsDeleted");
            });

            modelBuilder.Entity<HowItWorks>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_HowItWorks_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_HowItWorks_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Investor>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Investor_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_Investor_AppUserId");
                entity.HasIndex(e => e.CountryId).HasDatabaseName("IX_Investor_CountryId");
                entity.HasIndex(e => e.InvestorCategoryId).HasDatabaseName("IX_Investor_InvestorCategoryId");
                entity.HasIndex(e => e.IsLookingForCompany).HasDatabaseName("IX_Investor_IsLookingForCompany");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Investor_IsActive_IsDeleted");
            });

            modelBuilder.Entity<InvestorCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_InvestorCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_InvestorCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<KVKK>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_KVKK_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_KVKK_IsActive_IsDeleted");
            });

            modelBuilder.Entity<LayoutInfo>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_LayoutInfo_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_LayoutInfo_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Like_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_Like_AppUserId");
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Like_BlogId");
                entity.HasIndex(e => e.CommentId).HasDatabaseName("IX_Like_CommentId");
                entity.HasIndex(e => e.CommentAnswerId).HasDatabaseName("IX_Like_CommentAnswerId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Like_CompanyId");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_Like_InvestorId");
                entity.HasIndex(e => e.PostId).HasDatabaseName("IX_Like_PostId");
                entity.HasIndex(e => e.SurveyId).HasDatabaseName("IX_Like_SurveyId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Like_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Logo>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Logo_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Logo_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Message_Id").IsUnique();
                entity.HasIndex(e => e.SenderId).HasDatabaseName("IX_Message_SenderId");
                entity.HasIndex(e => e.ReceiverId).HasDatabaseName("IX_Message_ReceiverId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Message_IsActive_IsDeleted");
            });

            modelBuilder.Entity<MessageUserBlockList>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_MessageUserBlockList_Id").IsUnique();
                entity.HasIndex(e => e.BlockedId).HasDatabaseName("IX_MessageUserBlockList_BlockedId");
                entity.HasIndex(e => e.BlockerId).HasDatabaseName("IX_MessageUserBlockList_BlockerId");
                entity.HasIndex(e => new { e.IsBlocked, e.IsRemoved }).HasDatabaseName("IX_MessageUserBlockList_IsBlocked_IsRemoved");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_MessageUserBlockList_IsActive_IsDeleted");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_News_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_News_IsActive_IsDeleted");
            });

            modelBuilder.Entity<PersonalData>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_PersonalData_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_PersonalData_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Picture_Id").IsUnique();
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Picture_BlogId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Picture_CompanyId");
                entity.HasIndex(e => e.PostId).HasDatabaseName("IX_Picture_PostId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Picture_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Post_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_Post_AppUserId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Post_CompanyId");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_Post_InvestorId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Post_IsActive_IsDeleted");
            });

            modelBuilder.Entity<ProfileImage>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_ProfileImage_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_ProfileImage_AppUserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_ProfileImage_IsActive_IsDeleted");
            });

            modelBuilder.Entity<QuestionOption>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_QuestionOption_Id").IsUnique();
                entity.HasIndex(e => e.SurveyQuestionId).HasDatabaseName("IX_QuestionOption_SurveyQuestionId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_QuestionOption_IsActive_IsDeleted");
            });

            modelBuilder.Entity<RecentlyInvest>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_RecentlyInvest_Id").IsUnique();
                entity.HasIndex(e => e.IsExit).HasDatabaseName("IX_RecentlyInvest_IsExit");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_RecentlyInvest_InvestorId");
                entity.HasIndex(e => e.SectorId).HasDatabaseName("IX_RecentlyInvest_SectorId");
                entity.HasIndex(e => e.SubSectorId).HasDatabaseName("IX_RecentlyInvest_SubSectorId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_RecentlyInvest_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Report_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_Report_AppUserId");
                entity.HasIndex(e => e.ReportCategoryId).HasDatabaseName("IX_Report_ReportCategoryId");
                entity.HasIndex(e => e.AnnouncementId).HasDatabaseName("IX_Report_AnnouncementId");
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_Report_BlogId");
                entity.HasIndex(e => e.CommentId).HasDatabaseName("IX_Report_CommentId");
                entity.HasIndex(e => e.CommentAnswerId).HasDatabaseName("IX_Report_CommentAnswerId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Report_CompanyId");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_Report_InvestorId");
                entity.HasIndex(e => e.NewsId).HasDatabaseName("IX_Report_NewsId");
                entity.HasIndex(e => e.PostId).HasDatabaseName("IX_Report_PostId");
                entity.HasIndex(e => e.SectorNewsId).HasDatabaseName("IX_Report_SectorNewsId");
                entity.HasIndex(e => e.SurveyId).HasDatabaseName("IX_Report_SurveyId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Report_IsActive_IsDeleted");
            });

            modelBuilder.Entity<ReportCategory>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_ReportCategory_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_ReportCategory_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SavedContent>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SavedContent_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_SavedContent_AppUserId");
                entity.HasIndex(e => e.BlogId).HasDatabaseName("IX_SavedContent_BlogId");
                entity.HasIndex(e => e.SectorNewsId).HasDatabaseName("IX_SavedContent_SectorNewsId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_SavedContent_CompanyId");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_SavedContent_InvestorId");
                entity.HasIndex(e => e.PostId).HasDatabaseName("IX_SavedContent_PostId");
                entity.HasIndex(e => e.SurveyId).HasDatabaseName("IX_SavedContent_SurveyId");
                entity.HasIndex(e => e.IsSaved).HasDatabaseName("IX_SavedContent_IsSaved");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SavedContent_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Sector_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Sector_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SectorNews>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SectorNews_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SectorNews_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SecuritySetting>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SecuritySetting_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SecuritySetting_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SendMessage>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SendMessage_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SendMessage_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Slider_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Slider_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SocialMedia>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SocialMedia_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SocialMedia_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SubSector>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SubSector_Id").IsUnique();
                entity.HasIndex(e => e.SectorId).HasDatabaseName("IX_SubSector_SectorId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SubSector_IsActive_IsDeleted");
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_Survey_Id").IsUnique();
                entity.HasIndex(e => e.IsOnline).HasDatabaseName("IX_Survey_IsOnline");
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_Survey_AppUserId");
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_Survey_CompanyId");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_Survey_InvestorId");
                entity.HasIndex(e => new { e.StartDate, e.ClosedDate }).HasDatabaseName("IX_Survey_StartDate_ClosedDate");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_Survey_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SurveyAnalytics>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SurveyAnalytics_Id").IsUnique();
                entity.HasIndex(e => e.SurveyId).HasDatabaseName("IX_SurveyAnalytics_SurveyId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SurveyAnalytics_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SurveyAnswer>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SurveyAnswer_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_SurveyAnswer_AppUserId");
                entity.HasIndex(e => e.SurveyResponseId).HasDatabaseName("IX_SurveyAnswer_SurveyResponseId");
                entity.HasIndex(e => e.SurveyQuestionId).HasDatabaseName("IX_SurveyAnswer_SurveyQuestionId");
                entity.HasIndex(e => e.QuestionOptionId).HasDatabaseName("IX_SurveyAnswer_QuestionOptionId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SurveyAnswer_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SurveyQuestion>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SurveyQuestion_Id").IsUnique();
                entity.HasIndex(e => e.SurveyId).HasDatabaseName("IX_SurveyQuestion_SurveyId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SurveyQuestion_IsActive_IsDeleted");
            });

            modelBuilder.Entity<SurveyResponse>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_SurveyResponse_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_SurveyResponse_AppUserId");
                entity.HasIndex(e => e.SurveyId).HasDatabaseName("IX_SurveyResponse_SurveyId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_SurveyResponse_IsActive_IsDeleted");
            });

            modelBuilder.Entity<UserAgreement>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_UserAgreement_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_UserAgreement_IsActive_IsDeleted");
            });

            modelBuilder.Entity<UserProfileImage>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_UserProfileImage_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_UserProfileImage_AppUserId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_UserProfileImage_IsActive_IsDeleted");
            });

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_UserSession_Id").IsUnique();
                entity.HasIndex(e => e.AppUserId).HasDatabaseName("IX_UserSession_AppUserId");
                entity.HasIndex(e => e.IsOnline).HasDatabaseName("IX_UserSession_IsOnline");
                entity.HasIndex(e => e.LoginDate).HasDatabaseName("IX_UserSession_LoginDate");
                entity.HasIndex(e => e.LogoutDate).HasDatabaseName("IX_UserSession_LogoutDate");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_UserSession_IsActive_IsDeleted");
            });

            modelBuilder.Entity<UserSocialMedia>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_UserSocialMedia_Id").IsUnique();
                entity.HasIndex(e => e.CompanyId).HasDatabaseName("IX_UserSocialMedia_CompanyId");
                entity.HasIndex(e => e.InvestorId).HasDatabaseName("IX_UserSocialMedia_InvestorId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_UserSocialMedia_IsActive_IsDeleted");
            });

            modelBuilder.Entity<VisibilitySetting>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_VisibilitySetting_Id").IsUnique();
                entity.HasIndex(e => e.CompanyFinanceId).HasDatabaseName("IX_VisibilitySetting_CompanyFinanceId");
                entity.HasIndex(e => e.CompanyPintechId).HasDatabaseName("IX_VisibilitySetting_CompanyPintechId");
                entity.HasIndex(e => e.CompanyStageId).HasDatabaseName("IX_VisibilitySetting_CompanyStageId");
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_VisibilitySetting_IsActive_IsDeleted");
            });

            modelBuilder.Entity<WhatWeOffer>(entity =>
            {
                entity.HasIndex(e => e.Id).HasDatabaseName("IX_WhatWeOffer_Id").IsUnique();
                entity.HasIndex(e => new { e.IsActive, e.IsDeleted }).HasDatabaseName("IX_WhatWeOffer_IsActive_IsDeleted");
            });
        }
    }
}
