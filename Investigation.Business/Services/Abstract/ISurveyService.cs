using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface ISurveyService
    {
        IQueryable<Survey> GetAllIncludingAsync();
        IQueryable<Survey> GetAllIncludingByOnlineAsync();
        IQueryable<Survey> GetAllIncludingByOfflineAsync();
        IQueryable<Survey> GetAllIncludingByStartDateAsync();
        IQueryable<Survey> GetAllIncludingByCloseDateAsync();
        IQueryable<Survey> GetAllIncludingOpenSurveyByCompanyIdAsync(int? companyId);
        IQueryable<Survey> GetAllIncludingOpenSurveyByInvestorIdAsync(int? investorId);
        IQueryable<Survey> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<Survey> GetAllIncludingByInvestorIdAsync(int? investorId);
        IQueryable<Survey> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<Survey> GetAllIncludingForAdminAsync();
        IQueryable<Survey> GetAllIncludingSurveyForInvestorByInvestorIdAsync(int? investorId);
        IQueryable<Survey> GetAllIncludingSurveyForCompanyByCompanyIdAsync(int? companyId);
        Task<IEnumerable<Survey>> GetAllIncludingMostPopularSurveysAsync();
        Task<IEnumerable<Survey>> GetAllIncludingLessPopularSurveysAsync();
        IQueryable<Survey> GetAllIncludingSurveysForPublicUser();
        IQueryable<Survey> GetAllIncludingMostHitSurveyAsync();
        IQueryable<Survey> GetAllIncludingMostLikedSurveyAsync();
        IQueryable<Survey> GetAllIncludingMostSavedSurveyAsync();
        IQueryable<Survey> GetAllIncludingMostResponsedSurveyAsync();
        IQueryable<Survey> GetAllIncludingLessResponsedSurveyAsync();
        IQueryable<Survey> GetAllIncludingSurveyTodayAsync();
        Task<IEnumerable<Survey>> GetAllForSignalRAsync();
        Task<Survey> GetByIdAsync(int? id);
        Task<Survey?> GetBySlugAsync(string slug);
        Task<bool> SubmitSurveyAnswersAsync(int surveyId, Dictionary<int, int> answers);
        Task<bool> CreateCompanySurveyAsync(string title, string desc, DateTime startDate, DateTime closedDate, int? companyId, string appUserId);
        Task<bool> CreateInvestorSurveyAsync(string title, string desc, DateTime startDate, DateTime closedDate, int? investorId, string appUserId);
        Task<bool> UpdateCompanySurveyAsync(string title, string desc, DateTime startDate, DateTime closedDate, int? companyId, string appUserId, int id);
        Task<bool> UpdateInvestorSurveyAsync(string title, string desc, DateTime startDate, DateTime closedDate, int? investorId, string appUserId, int id);
        Task<bool> DeleteAsync(Survey entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetCurrentlyOnlineSurveyAsync(int id);
        Task<bool> SetOfflineSurveyAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Survey> GetAllIncludingPopularSurveys();
        IQueryable<Survey> GetAllIncludingLastSurveyForIndex();
        IQueryable<Survey> GetAllIncludingLastSurveyForTimeline();
        IQueryable<Survey> GetAllIncludingSurveyForInvestorDetail(int? investorId);
        IQueryable<Survey> GetAllIncludingSurveyByCompanyId(int? companyId);
        IQueryable<Survey> GetAllForSitemap();
        Survey GetSurveyById(int? id);
        int SurveyCounter();
    }
}
