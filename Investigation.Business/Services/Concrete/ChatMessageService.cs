using Ganss.Xss;
using Investigation.Business.Constants.Hubs;
using Investigation.Business.Constants.Services;
using Investigation.Business.Constants.Utilities.ChatBlockModel;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Investigation.Domain.Entities.UserEntities;
using Investigation.Shared.ViewModels.ChatMessage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Investigation.Business.Services.Concrete
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly EncryptionService _encryptionService;
        private readonly UserManager<AppUser> _userManager;
        readonly IUserService _userService;
        readonly IHubContext<ChatHub> _hubContext;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public ChatMessageService(ApplicationDbContext context, EncryptionService encryptionService, UserManager<AppUser> userManager, IUserService userService, IHtmlSanitizer htmlSanitizer, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _encryptionService = encryptionService;
            _userManager = userManager;
            _userService = userService;
            _htmlSanitizer = htmlSanitizer;
            _hubContext = hubContext;
        }

        public async Task<List<MessageVM>> GetConversationAsync(string currentUserId, string otherUserId)
        {
            try
            {
                var isRemoved = await _context.MessageUserBlockLists.FirstOrDefaultAsync(x => x.BlockerId == currentUserId && x.BlockedId == otherUserId);

                var otherUser = await _userManager.FindByIdAsync(otherUserId);
                var otherUserName = otherUser?.UserName ?? "Unknown User";

                if (isRemoved != null && isRemoved.IsRemoved)
                {
                    return new List<MessageVM>();
                }

                var query = _context.Messages.Where(m =>
                        (m.SenderId == currentUserId && m.ReceiverId == otherUserId) ||
                        (m.SenderId == otherUserId && m.ReceiverId == currentUserId));

                if (isRemoved != null && isRemoved.BlockedAt != default(DateTime))
                {
                    query = query.Where(m => m.SentAt > isRemoved.BlockedAt);
                }

                var encryptedMessages = await query.OrderBy(m => m.SentAt).ToListAsync();

                var unreadMessages = encryptedMessages.Where(m => m.ReceiverId == currentUserId && !m.IsRead).ToList();
                if (unreadMessages.Any())
                {
                    foreach (var msg in unreadMessages)
                    {
                        msg.IsRead = true;
                    }
                    await _context.SaveChangesAsync();

                    foreach (var msg in unreadMessages)
                    {
                        await _hubContext.Clients.User(msg.SenderId).SendAsync("MessageRead", msg.Id);
                    }
                }

                var viewModels = new List<MessageVM>();
                foreach (var msg in encryptedMessages)
                {
                    ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                    string safeContent = _htmlSanitizer.Sanitize(msg.Content ?? string.Empty);

                    var decrypted = _encryptionService.Decrypt(safeContent);
                    viewModels.Add(new MessageVM
                    {
                        Id = msg.Id,
                        SenderId = msg.SenderId,
                        SenderName = msg.SenderId == currentUserId ? "Me" : otherUserName,
                        Content = decrypted,
                        SentAt = msg.SentAt,
                        IsRead = msg.IsRead,
                        IsSentByMe = msg.SenderId == currentUserId
                    });
                }
                return viewModels;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<List<AppUser>> GetMessagedUsersAsync(string currentUserId)
        {
            try
            {
                var hiddenUserIds = await _context.MessageUserBlockLists.Where(m => m.BlockerId == currentUserId && (m.IsBlocked || m.IsRemoved))
             .Select(m => m.BlockedId).ToHashSetAsync();

                var conversedUserIds = await _context.Messages.Where(m =>
                        (m.SenderId == currentUserId && !hiddenUserIds.Contains(m.ReceiverId)) || (m.ReceiverId == currentUserId && !hiddenUserIds.Contains(m.SenderId)))
                    .Select(m => m.SenderId == currentUserId ? m.ReceiverId : m.SenderId).Distinct().ToListAsync();

                if (!conversedUserIds.Any())
                {
                    return new List<AppUser>();
                }

                var users = new List<AppUser>();
                foreach (var id in conversedUserIds)
                {
                    var user = await _userService.GetByIdAsync(id);
                    if (user != null)
                    {
                        users.Add(user);
                    }
                }

                var orderedUsers = users.OrderByDescending(u =>
                {
                    var lastDate = _context.Messages
                        .Where(m =>
                            (m.SenderId == currentUserId && m.ReceiverId == u.Id) ||
                            (m.SenderId == u.Id && m.ReceiverId == currentUserId))
                        .Select(m => (DateTime?)m.SentAt).Max();
                    return lastDate ?? DateTime.MinValue;
                }).ToList();

                return orderedUsers;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<List<string>> GetMyBlockedUserIdsAsync(string blockerId)
        {
            try
            {
                if (string.IsNullOrEmpty(blockerId))
                    return new List<string>();

                var list = await _context.MessageUserBlockLists.Where(x => x.BlockerId == blockerId && x.IsBlocked).Select(x => x.BlockedId).ToListAsync();
                return list ?? new List<string>();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<List<UserConversationVM>> GetMyMessageInboxAsync(string currentUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(currentUserId))
                    throw new ArgumentException("User ID cannot be empty");

                var partnerIds = await _context.Messages.Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId).Select(m => m.SenderId == currentUserId ? m.ReceiverId : m.SenderId).Distinct().ToListAsync();

                var conversations = new List<UserConversationVM>();
                foreach (var partnerId in partnerIds)
                {
                    var partner = await _userManager.FindByIdAsync(partnerId);
                    if (partner == null) continue;

                    var messages = await _context.Messages.Where(m => (m.SenderId == currentUserId && m.ReceiverId == partnerId) || (m.SenderId == partnerId && m.ReceiverId == currentUserId)).OrderBy(m => m.SentAt).ToListAsync();

                    var unreadCount = messages.Count(m => m.ReceiverId == currentUserId && !m.IsRead);
                    var lastMessage = messages.LastOrDefault();

                    conversations.Add(new UserConversationVM
                    {
                        PartnerId = partnerId,
                        PartnerName = partner.UserName ?? partner.Email,
                        PartnerEmail = partner.Email,
                        LastMessage = lastMessage?.Content ?? "",
                        LastMessageTime = lastMessage?.SentAt,
                        UnreadCount = unreadCount,
                        Messages = messages.Select(m => new MessageVM
                        {
                            Id = m.Id,
                            SenderId = m.SenderId,
                            ReceiverId = m.ReceiverId,
                            Content = m.Content,
                            SentAt = m.SentAt,
                            IsRead = m.IsRead,
                            IsSentByMe = m.SenderId == currentUserId
                        }).ToList()
                    });
                }
                return conversations.OrderByDescending(c => c.LastMessageTime).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            try
            {
                return await _context.Messages.CountAsync(m => m.ReceiverId == userId && !m.IsRead);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task HardDeleteConversationAsync(string userId1, string userId2)
        {
            try
            {
                var messages = await _context.Messages.Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) || (m.SenderId == userId2 && m.ReceiverId == userId1)).ToListAsync();
                if (messages.Any())
                {
                    _context.Messages.RemoveRange(messages);
                    await _context.SaveChangesAsync();
                }

                var blockEntries = await _context.MessageUserBlockLists.Where(m => (m.BlockerId == userId1 && m.BlockedId == userId2) || (m.BlockerId == userId2 && m.BlockedId == userId1)).ToListAsync();
                if (blockEntries.Any())
                {
                    _context.MessageUserBlockLists.RemoveRange(blockEntries);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task HardDeleteMessageAsync(int messageId)
        {
            try
            {
                var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
                if (message == null)
                {
                    return;
                }
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task HardDeleteMessagesAsync(List<int> messageIds)
        {
            try
            {
                var messages = await _context.Messages.Where(m => messageIds.Contains(m.Id)).ToListAsync();
                if (messages.Any())
                {
                    _context.Messages.RemoveRange(messages);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<bool> HasBlockedMeAsync(string blockerId, string blockedId)
        {
            try
            {
                return await _context.MessageUserBlockLists.AnyAsync(x => x.BlockerId == blockedId && x.BlockedId == blockerId && x.IsBlocked);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<bool> HasConversationAsync(string userId, string otherUserId)
        {
            try
            {
                return await IsUserParticipantAsync(userId, otherUserId);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<bool> IsConversationRemovedForUserAsync(string currentUserId, string otherUserId)
        {
            try
            {
                return await _context.MessageUserBlockLists.AnyAsync(x => x.BlockerId == currentUserId && x.BlockedId == otherUserId && x.IsRemoved);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<bool> IsUserBlockedByMeAsync(string blockerId, string blockedId)
        {
            try
            {
                return await _context.MessageUserBlockLists.AnyAsync(x => x.BlockerId == blockerId && x.BlockedId == blockedId && x.IsBlocked);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<bool> IsUserParticipantAsync(string userId, string otherUserId)
        {
            try
            {
                return await _context.Messages.AnyAsync(m => (m.SenderId == userId && m.ReceiverId == otherUserId) || (m.SenderId == otherUserId && m.ReceiverId == userId));
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task MarkMessagesAsReadAsync(string receiverId, List<int> messageIds)
        {
            try
            {
                if (!messageIds.Any()) return;
                var messages = await _context.Messages.Where(m => messageIds.Contains(m.Id) && m.ReceiverId == receiverId && !m.IsRead).ToListAsync();

                if (messages.Any())
                {
                    messages.ForEach(m => m.IsRead = true);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task RemoveConversationForUserAsync(string currentUserId, string otherUserId)
        {
            try
            {
                var existingEntry = await _context.MessageUserBlockLists.FirstOrDefaultAsync(x => x.BlockerId == currentUserId && x.BlockedId == otherUserId);
                if (existingEntry == null)
                {
                    var newEntry = new MessageUserBlockList
                    {
                        BlockerId = currentUserId,
                        BlockedId = otherUserId,
                        IsBlocked = false,
                        IsRemoved = true,
                        BlockedAt = DateTime.UtcNow,
                        BlockedUserName = ""
                    };
                    _context.MessageUserBlockLists.Add(newEntry);
                }
                else
                {
                    existingEntry.IsRemoved = true;
                    existingEntry.BlockedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task ResetRemovedStatusAsync(string userId1, string userId2)
        {
            try
            {
                var entry = await _context.MessageUserBlockLists.FirstOrDefaultAsync(x => (x.BlockerId == userId1 && x.BlockedId == userId2) || (x.BlockerId == userId2 && x.BlockedId == userId1));

                if (entry != null && entry.IsRemoved)
                {
                    entry.IsRemoved = false;
                    entry.BlockedAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }

        public async Task<BlockResult> ToggleBlockAsync(string blockerId, string blockedId)
        {
            try
            {
                if (blockerId == blockedId)
                    return new BlockResult { Success = false, Message = "You cannot block yourself." };

                var blockedUser = await _userManager.FindByIdAsync(blockedId);
                if (blockedUser == null)
                    return new BlockResult { Success = false, Message = "No user to block was found." };

                var blockerUser = await _userManager.FindByIdAsync(blockerId);
                if (blockerUser == null)
                    return new BlockResult { Success = false, Message = "Session error." };

                var record = await _context.MessageUserBlockLists.FirstOrDefaultAsync(x => x.BlockerId == blockerId && x.BlockedId == blockedId);
                bool isNowBlocked;
                if (record == null)
                {
                    record = new MessageUserBlockList
                    {
                        BlockerId = blockerId,
                        BlockedId = blockedId,
                        BlockedUserName = blockedUser.UserName ?? "Bilinmeyen",
                        IsBlocked = true,
                        BlockedAt = DateTime.UtcNow
                    };
                    _context.MessageUserBlockLists.Add(record);
                    isNowBlocked = true;
                }
                else
                {
                    record.IsBlocked = !record.IsBlocked;
                    if (record.IsBlocked)
                        record.BlockedAt = DateTime.UtcNow;

                    isNowBlocked = record.IsBlocked;
                }
                await _context.SaveChangesAsync();
                return new BlockResult
                {
                    Success = true,
                    Message = isNowBlocked ? "The user has been blocked." : "The block has been removed.",
                    IsNowBlocked = isNowBlocked
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred", ex);
            }
        }
    }
}
