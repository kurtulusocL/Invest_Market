using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Abstract
{
    public interface IReportService
    {
        IQueryable<Report> GetAllIncludingAsync();
        IQueryable<Report> GetAllIncludingByTodaysReportAsync();
        IQueryable<Report> GetAllIncludingByFixedReportAsync();
        IQueryable<Report> GetAllIncludingByNotFixedReportAsync();
        IQueryable<Report> GetAllIncludingByReportCategoryIdAsync(int reportCategoryId);
        IQueryable<Report> GetAllIncludingByUserIdAsync(string appUserId);
        IQueryable<Report> GetAllIncludingByAnnouncementIdAsync(int? announcementId);
        IQueryable<Report> GetAllIncludingByBlogIdAsync(int? blogId);
        IQueryable<Report> GetAllIncludingByCommentIdAsync(int? commentId);
        IQueryable<Report> GetAllIncludingByCommentAnswerIdAsync(int? commentAnswerId);
        IQueryable<Report> GetAllIncludingByCompanyIdAsync(int? companyId);
        IQueryable<Report> GetAllIncludingByInvestorIdAsync(int? investorId);
        IQueryable<Report> GetAllIncludingByNewsIdAsync(int? newsId);
        IQueryable<Report> GetAllIncludingByPostIdAsync(int? postId);
        IQueryable<Report> GetAllIncludingBySectorNewsIdAsync(int? sectorNewsId);
        IQueryable<Report> GetAllIncludingBySurveyIdAsync(int? surveyId);
        IQueryable<Report> GetAllIncludingForAdminAsync();
        IQueryable<Report> GetAllIncludingReportsForUserByUserIdAsync(string userId);
        IQueryable<Report> GetAllIncludingReportsForReportOwnerByUserIdAsync(string userId);
        Task<IEnumerable<Report>> GetAllForSignalRAsync();
        Task<Report> GetByIdAsync(int? id);
        Task<bool> CreateAnnouncementReportAsync(string title, string subject, int? announcementId, string appUserId, int reportCategoryId);
        Task<bool> CreateBlogReportAsync(string title, string subject, int? blogId, string appUserId, int reportCategoryId);
        Task<bool> CreateCommentReportAsync(string title, string subject, int? commentId, string appUserId, int reportCategoryId);
        Task<bool> CreateCommentAnswerReportAsync(string title, string subject, int? commentAnswerId, string appUserId, int reportCategoryId);
        Task<bool> CreateCompanyReportAsync(string title, string subject, int? companyId, string appUserId, int reportCategoryId);
        Task<bool> CreateInvestorReportAsync(string title, string subject, int? investorId, string appUserId, int reportCategoryId);
        Task<bool> CreateNewsReportAsync(string title, string subject, int? newsId, string appUserId, int reportCategoryId);
        Task<bool> CreatePostReportAsync(string title, string subject, int? postId, string appUserId, int reportCategoryId);
        Task<bool> CreateSectorNewsReportAsync(string title, string subject, int? sectorNewsId, string appUserId, int reportCategoryId);
        Task<bool> CreateSurveyReportAsync(string title, string subject, int? surveyId, string appUserId, int reportCategoryId);
        Task<bool> DeleteAsync(Report entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetFixedReportAsync(int id);
        Task<bool> SetNotFixedReportAsync(int id);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
        IQueryable<Report> GetAllIncludingTodaysReportsForAdminHeader();
        int ReportCounter();
    }
}
