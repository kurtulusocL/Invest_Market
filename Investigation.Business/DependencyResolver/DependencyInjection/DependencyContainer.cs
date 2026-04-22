using System.Reflection;
using FluentValidation;
using Ganss.Xss;
using Investigation.Business.Constants.Factory;
using Investigation.Business.Constants.Handlers;
using Investigation.Business.Constants.Handlers.HandlerClass;
using Investigation.Business.Constants.Services;
using Investigation.Business.Extensions;
using Investigation.Business.Services.Abstract;
using Investigation.Business.Services.Concrete;
using Investigation.DataAccess.Abstract;
using Investigation.DataAccess.Abstract.ServiceAbstract;
using Investigation.DataAccess.Concrete.EntityFramework.Repositories;
using Investigation.DataAccess.Concrete.EntityFramework.Repositories.ServiceConcrete;
using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.Factory;
using Investigation.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Investigation.Business.DependencyResolver.DependencyInjection
{
    public static class DependencyContainer
    {
        public static void DependencyService(this IServiceCollection services)
        {
            services.AddScoped<DataMigrationService>();
            services.AddScoped<IMailService, MailManager>();
            services.AddSingleton<ICaptchaService, CaptchaManager>();
            services.AddScoped<EncryptionService>();
            services.AddScoped<PseudonymizationService>();
            services.AddScoped<IChatMessageService, ChatMessageService>();
            services.AddScoped<SecureIdHelper>();
            services.AddScoped<IWebHelperService, WebHelperService>();
            services.AddHostedService<HeartbeatCleanupService>();
            services.AddHostedService<AuditLogCleanupService>();
            services.AddHostedService<UserSessionCleanupService>();

            services.AddScoped<IAuthorizationHandler, ProfileOwnerRequirementHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CompanyOwnerOnly", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("CompanyUsers");
                    policy.Requirements.Add(new ProfileOwnerRequirement());
                });

                options.AddPolicy("InvestorOwnerOnly", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("InvestorUsers");
                    policy.Requirements.Add(new ProfileOwnerRequirement());
                });
            });

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AdditionalUserClaimsPrincipalFactory>();
            services.AddScoped<IAdminAuthService, AdminAuthManager>();
            services.AddScoped<IUserAuthService, UserAuthManager>();
            services.AddScoped<IUserAbstractServices, UserConcreteServices>();

            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IAboutService, AboutManager>();

            services.AddScoped<IAdRepository, AdRepository>();
            services.AddScoped<IAdService, AdManager>();

            services.AddScoped<IAdTargetRepository, AdTargetRepository>();
            services.AddScoped<IAdTargetService, AdTargetManager>();

            services.AddScoped<IAnnouncementCategoryRepository, AnnouncementCategoryRepository>();
            services.AddScoped<IAnnouncementCategoryService, AnnouncementCategoryManager>();

            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IAnnouncementService, AnnouncementManager>();

            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IAuditService, AuditManager>();

            services.AddScoped<IBannerImageRepository, BannerImageRepository>();
            services.AddScoped<IBannerImageService, BannerImageManager>();

            services.AddScoped<IBlackListRepository, BlackListRepository>();
            services.AddScoped<IBlackListService, BlackListManager>();

            services.AddScoped<IBlockedRepository, BlockedRepository>();
            services.AddScoped<IBlockedService, BlockedManager>();

            services.AddScoped<IBlockedMessageUserRepository, BlockedMessageUserRepository>();
            services.AddScoped<IBlockedMessageUserService, BlockedMessageUserManager>();

            services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
            services.AddScoped<IBlogCategoryService, BlogCategoryManager>();

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogManager>();

            services.AddScoped<ICancelMembershipCategoryRepository, CancelMembershipCategoryRepository>();
            services.AddScoped<ICancelMembershipCategoryService, CancelMembershipCategoryManager>();

            services.AddScoped<ICancelMembershipRepository, CancelMembershipRepository>();
            services.AddScoped<ICancelMembershipService, CancelMembershipManager>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<ICommentAnswerRepository, CommentAnswerRepository>();
            services.AddScoped<ICommentAnswerService, CommentAnswerManager>();

            services.AddScoped<ICompanyCategoryRepository, CompanyCategoryRepository>();
            services.AddScoped<ICompanyCategoryService, CompanyCategoryManager>();

            services.AddScoped<ICompanyFinanceRepository, CompanyFinanceRepository>();
            services.AddScoped<ICompanyFinanceService, CompanyFinanceManager>();

            services.AddScoped<ICompanyContactRepository, CompanyContactRepository>();
            services.AddScoped<ICompanyContactService, CompanyContactManager>();

            services.AddScoped<ICompanyPintechRepository, CompanyPintechRepository>();
            services.AddScoped<ICompanyPintechService, CompanyPintechManager>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyManager>();

            services.AddScoped<ICompanyStageRepository, CompanyStageRepository>();
            services.AddScoped<ICompanyStageService, CompanyStageManager>();

            services.AddScoped<ICompanyTeamRepository, CompanyTeamRepository>();
            services.AddScoped<ICompanyTeamService, CompanyTeamManager>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryService, CountryManager>();

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactManager>();

            services.AddScoped<IDataPolicyRepository, DataPolicyRepository>();
            services.AddScoped<IDataPolicyService, DataPolicyManager>();

            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IEventsService, EventsManager>();

            services.AddScoped<IEventsCategoryRepository, EventsCategoryRepository>();
            services.AddScoped<IEventsCategoryService, EventsCategoryManager>();

            services.AddScoped<IEventsParticipantRepository, EventsParticipantRepository>();
            services.AddScoped<IEventsParticipantService, EventsParticipantManager>();

            services.AddScoped<IExceptionLoggerRepository, ExceptionLoggerRepository>();
            services.AddScoped<IExceptionLoggerService, ExceptionLoggerManager>();

            services.AddScoped<IFrequentlyRepository, FrequentlyRepository>();
            services.AddScoped<IFrequentlyService, FrequentlyManager>();

            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IFollowService, FollowManager>();

            services.AddScoped<IHitRepository, HitRepository>();
            services.AddScoped<IHitService, HitManager>();

            services.AddScoped<IHowItWorksRepository, HowItWorksRepository>();
            services.AddScoped<IHowItWorksService, HowItWorksManager>();

            services.AddScoped<IInvestorCategoryRepository, InvestorCategoryRepository>();
            services.AddScoped<IInvestorCategoryService, InvestorCategoryManager>();

            services.AddScoped<IInvestorRepository, InvestorRepository>();
            services.AddScoped<IInvestorService, InvestorManager>();

            services.AddScoped<IKVKKRepository, KVKKRepository>();
            services.AddScoped<IKVKKService, KVKKManager>();

            services.AddScoped<ILayoutInfoRepository, LayoutInfoRepository>();
            services.AddScoped<ILayoutInfoService, LayoutInfoManager>();

            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<ILikeService, LikeManager>();

            services.AddScoped<ILogoRepository, LogoRepository>();
            services.AddScoped<ILogoService, LogoManager>();

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageManager>();

            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsManager>();

            services.AddScoped<IPersonalDataRepository, PersonalDataRepository>();
            services.AddScoped<IPersonalDataService, PersonalDataManager>();

            services.AddScoped<IPictureRepository, PictureRepository>();
            services.AddScoped<IPictureService, PictureManager>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostManager>();

            services.AddScoped<IProfileImageRepository, ProfileImageRepository>();
            services.AddScoped<IProfileImageService, ProfileImageManager>();

            services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();
            services.AddScoped<IQuestionOptionService, QuestionOptionManager>();

            services.AddScoped<IRecentlyInvestRepository, RecentlyInvestRepository>();
            services.AddScoped<IRecentlyInvestService, RecentlyInvestManager>();

            services.AddScoped<IReportCategoryRepository, ReportCategoryRepository>();
            services.AddScoped<IReportCategoryService, ReportCategoryManager>();

            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportService, ReportManager>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleManager>();

            services.AddScoped<ISavedContentRepository, SavedContentRepository>();
            services.AddScoped<ISavedContentService, SavedContentManager>();

            services.AddScoped<ISectorNewsRepository, SectorNewsRepository>();
            services.AddScoped<ISectorNewsService, SectorNewsManager>();

            services.AddScoped<ISectorRepository, SectorRepository>();
            services.AddScoped<ISectorService, SectorManager>();

            services.AddScoped<ISecuritySettingRepository, SecuritySettingRepository>();
            services.AddScoped<ISecuritySettingService, SecuritySettingManager>();

            services.AddScoped<ISendMessageRepository, SendMessageRepository>();
            services.AddScoped<ISendMessageService, SendMessageManager>();

            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<ISliderService, SliderManager>();

            services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
            services.AddScoped<ISocialMediaService, SocialMediaManager>();

            services.AddScoped<ISubSectorRepository, SubSectorRepository>();
            services.AddScoped<ISubSectorService, SubSectorManager>();

            services.AddScoped<ISurveyAnalyticsRepository, SurveyAnalyticsRepository>();
            services.AddScoped<ISurveyAnalyticsService, SurveyAnalyticsManager>();

            services.AddScoped<ISurveyAnswerRepository, SurveyAnswerRepository>();
            services.AddScoped<ISurveyAnswerService, SurveyAnswerManager>();

            services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
            services.AddScoped<ISurveyQuestionService, SurveyQuestionManager>();

            services.AddScoped<ISurveyResponseRepository, SurveyResponseRepository>();
            services.AddScoped<ISurveyResponseService, SurveyResponseManager>();

            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<ISurveyService, SurveyManager>();

            services.AddScoped<IUserAgreementRepository, UserAgreementRepository>();
            services.AddScoped<IUserAgreementService, UserAgreementManager>();

            services.AddScoped<IUserProfileImageRepository, UserProfileImageRepository>();
            services.AddScoped<IUserProfileImageService, UserProfileImageManager>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserManager>();

            services.AddScoped<IUserSessionRepository, UserSessionRepository>();
            services.AddScoped<IUserSessionService, UserSessionManager>();

            services.AddScoped<IUserSocialMediaRepository, UserSocialMediaRepository>();
            services.AddScoped<IUserSocialMediaService, UserSocialMediaManager>();

            services.AddScoped<IVisibilitySettingRepository, VisibilitySettingRepository>();
            services.AddScoped<IVisibilitySettingService, VisibilitySettingManager>();

            services.AddScoped<IWhatWeOfferRepository, WhatWeOfferRepository>();
            services.AddScoped<IWhatWeOfferService, WhatWeOfferManager>();

            services.AddSingleton<IHtmlSanitizer>(_ => HtmlSanitizerFactory.Create());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationServices();
        }
    }
}
