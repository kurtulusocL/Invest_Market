using Investigation.Domain.Entities;
using Investigation.Shared.Dtos.FollowDtos;

namespace Investigation.Business.Services.Abstract
{
    public interface IFollowService
    {
        IQueryable<Follow> GetAllIncluding();
        IQueryable<Follow> GetAllIncludingCancelledOnFollow();
        IQueryable<Follow> GetAllIncludingCurrentlyFollowing();
        IQueryable<Follow> GetAllIncludingFollowsByUserId(string userId);
        IQueryable<Follow> GetAllIncludingFollowsByCompanyId(int? companyId);
        IQueryable<Follow> GetAllIncludingFolloweds();
        IQueryable<Follow> GetAllIncludingFollowedsByUserId(string userId);
        IQueryable<Follow> GetAllIncludingFollowedsByCompanyId(int? companyId);
        IQueryable<Follow> GetAllIncludingUnFollows();
        IQueryable<Follow> GetAllIncludingCurrentlyUnFollowingByUserId(string userId);
        IQueryable<Follow> GetAllIncludingUnFollowsByCompanyId(int? companyId);
        IQueryable<Follow> GetAllIncludingFollowedsForUserByUserId(string userId, int skip = 0, int take = 50);
        IQueryable<Follow> GetAllIncludingFollowedsForCompanyByCompanyId(int? companyId, int skip = 0, int take = 50);
        IQueryable<Follow> GetAllIncludingFollowersForUserByUserId(string userId, int skip = 0, int take = 50);
        IQueryable<Follow> GetAllIncludingFollowersForCompanyByCompanyId(int? companyId, int skip = 0, int take = 50);
        IQueryable<Follow> GetAllIncludingFollowedsSearchResultForUserByUserId(string userId, int skip, int take, string searchTerm);
        IQueryable<Follow> GetAllIncludingFollowersSearchResultForUserByUserId(string userId, int skip, int take, string searchTerm);
        IQueryable<Follow> GetAllIncludingFollowerCompaniesSearchResultByCompanyId(int companyId, int skip, int take, string searchTerm);
        IQueryable<Follow> GetAllIncludingFollowedCompaniesSearchResultByCompanyId(int companyId, int skip, int take, string searchTerm);
        IQueryable<Follow> GetAllIncludingForAdmin();
        Task<Follow> GetByIdAsync(int? id);
        Task<bool> CancelFollowerAsync(string? targetFollowerUserId, int? targetFollowerCompanyId);
        Task<bool> FollowAsync(bool isFollowed, string? followedUserId, int? followedCompanyId);
        Task<bool> UnfollowAsync(bool isFollowed, string? followedUserId, int? followedCompanyId);
        Task<FollowStatusDto> GetFollowStatusAsync(string? targetUserId, int? targetCompanyId);
        Task<bool> DeleteAsync(Follow entity, int id);
        Task<bool> DeleteAllByIdAsync(List<int> ids);
        Task<bool> SetActiveAsync(int id);
        Task<bool> SetDeActiveAsync(int id);
        Task<bool> SetDeletedAsync(int id);
        Task<bool> SetNotDeletedAsync(int id);
    }
}
