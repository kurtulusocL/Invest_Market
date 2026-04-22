using FluentValidation;
using Investigation.Business.Constants.Validations.FluentValidator.DTOValidation.AuthDtoValidation.AdminAuthDtoValidation;
using Investigation.Business.Constants.Validations.FluentValidator.DTOValidation.AuthDtoValidation.UserAuthDtoValidation;
using Investigation.Business.Constants.Validations.FluentValidator.EntityValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Investigation.Business.Extensions
{
    public static class FluentValidationExtensions
    {
        public static void AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AboutValidator>();
            services.AddValidatorsFromAssemblyContaining<AdValidator>();
            services.AddValidatorsFromAssemblyContaining<AdTargetValidator>();
            services.AddValidatorsFromAssemblyContaining<AnnouncementValidator>();
            services.AddValidatorsFromAssemblyContaining<AnnouncementCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<BannerImageValidator>();
            services.AddValidatorsFromAssemblyContaining<BlackListValidator>();
            services.AddValidatorsFromAssemblyContaining<BlogValidator>();
            services.AddValidatorsFromAssemblyContaining<BlogCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<CancelMembershipValidator>();
            services.AddValidatorsFromAssemblyContaining<CancelMembershipCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<CommentValidator>();
            services.AddValidatorsFromAssemblyContaining<CommentAnswerValidator>();
            services.AddValidatorsFromAssemblyContaining<CompanyValidator>();
            services.AddValidatorsFromAssemblyContaining<CompanyCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<CompanyContactValidator>();
            services.AddValidatorsFromAssemblyContaining<CompanyFinanceValidator>();
            services.AddValidatorsFromAssemblyContaining<CompanyPintechValidator>();
            services.AddValidatorsFromAssemblyContaining<CompanyStageValidator>();
            services.AddValidatorsFromAssemblyContaining<CompanyTeamValidator>();
            services.AddValidatorsFromAssemblyContaining<ContactValidator>();
            services.AddValidatorsFromAssemblyContaining<CountryValidator>();
            services.AddValidatorsFromAssemblyContaining<DataPolicyValidator>();
            services.AddValidatorsFromAssemblyContaining<EventsValidator>();
            services.AddValidatorsFromAssemblyContaining<EventsCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<EventsParticipantValidator>();
            services.AddValidatorsFromAssemblyContaining<FrequentlyValidator>();
            services.AddValidatorsFromAssemblyContaining<HowItWorksValidator>();
            services.AddValidatorsFromAssemblyContaining<InvestorValidator>();
            services.AddValidatorsFromAssemblyContaining<InvestorCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<KvkkValidator>();
            services.AddValidatorsFromAssemblyContaining<LayoutInfoValidator>();
            services.AddValidatorsFromAssemblyContaining<LogoValidator>();
            services.AddValidatorsFromAssemblyContaining<MessageValidator>();
            services.AddValidatorsFromAssemblyContaining<MessageUserBlockListValidator>();
            services.AddValidatorsFromAssemblyContaining<NewsValidator>();
            services.AddValidatorsFromAssemblyContaining<PersonalDataValidator>();
            services.AddValidatorsFromAssemblyContaining<PictureValidator>();
            services.AddValidatorsFromAssemblyContaining<PostValidator>();
            services.AddValidatorsFromAssemblyContaining<ProfileImageValidator>();
            services.AddValidatorsFromAssemblyContaining<QuestionOptionValidator>();
            services.AddValidatorsFromAssemblyContaining<RecentlyInvestValidator>();
            services.AddValidatorsFromAssemblyContaining<ReportValidator>();
            services.AddValidatorsFromAssemblyContaining<ReportCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<SavedContentValidator>();
            services.AddValidatorsFromAssemblyContaining<SectorValidator>();
            services.AddValidatorsFromAssemblyContaining<SectorNewsValidator>();
            services.AddValidatorsFromAssemblyContaining<SendMessageValidator>();
            services.AddValidatorsFromAssemblyContaining<SliderValidator>();
            services.AddValidatorsFromAssemblyContaining<SocialMediaValidator>();
            services.AddValidatorsFromAssemblyContaining<SubsectorValidator>();
            services.AddValidatorsFromAssemblyContaining<SurveyValidator>();
            services.AddValidatorsFromAssemblyContaining<SurveyAnswerValidator>();
            services.AddValidatorsFromAssemblyContaining<SurveyQuestionValidator>();
            services.AddValidatorsFromAssemblyContaining<UserAgreementValidator>();
            services.AddValidatorsFromAssemblyContaining<UserProfileImageValidator>();
            services.AddValidatorsFromAssemblyContaining<UserSessionValidator>();
            services.AddValidatorsFromAssemblyContaining<UserSocialMediaValidator>();
            services.AddValidatorsFromAssemblyContaining<WhatWeOfferValidator>();

            services.AddValidatorsFromAssemblyContaining<UserChangePasswordDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UserConfirmCodeDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UserForgotPasswordDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UserResetPasswordDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UserProfileUpdateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UserLoginDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidator>();

            services.AddValidatorsFromAssemblyContaining<AdminChangePasswordDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<AdminConfirmCodeDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<AdminLoginDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<AdminRegisterDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<AdminUpdateProfileDtoValidator>();
        }
    }
}
