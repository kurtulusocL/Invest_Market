using Investigation.Business.Constants.Utilities.ChatBlockModel;
using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.ViewModels.ChatMessage;

namespace Investigation.Business.Services.Abstract
{
    public interface IChatMessageService
    {
        Task<List<AppUser>> GetMessagedUsersAsync(string currentUserId);
        Task<List<MessageVM>> GetConversationAsync(string currentUserId, string otherUserId);
        Task MarkMessagesAsReadAsync(string receiverId, List<int> messageIds);
        Task<BlockResult> ToggleBlockAsync(string blockerId, string blockedId);
        Task<bool> IsUserBlockedByMeAsync(string blockerId, string blockedId); 
        Task<bool> HasBlockedMeAsync(string blockerId, string blockedId); 
        Task<List<string>> GetMyBlockedUserIdsAsync(string blockerId);
        Task RemoveConversationForUserAsync(string currentUserId, string otherUserId);
        Task<bool> IsConversationRemovedForUserAsync(string currentUserId, string otherUserId);
        Task HardDeleteConversationAsync(string userId1, string userId2);
        Task HardDeleteMessageAsync(int messageId);
        Task HardDeleteMessagesAsync(List<int> messageIds);
        Task ResetRemovedStatusAsync(string userId1, string userId2);
        Task<List<UserConversationVM>> GetMyMessageInboxAsync(string currentUserId);
        Task<bool> IsUserParticipantAsync(string userId, string otherUserId);
        Task<bool> HasConversationAsync(string userId, string otherUserId);
        Task<int> GetUnreadCountAsync(string userId);
    }
}
