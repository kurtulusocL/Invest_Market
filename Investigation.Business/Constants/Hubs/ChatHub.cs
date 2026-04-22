using Ganss.Xss;
using Investigation.Business.Constants.Services;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Concrete.EntityFramework.Context.MSSQL;
using Investigation.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Investigation.Business.Constants.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly EncryptionService _encryptionService;
        readonly IChatMessageService _chatMessageService;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public ChatHub(ApplicationDbContext context, EncryptionService encryptionService, IChatMessageService chatMessageService, IHtmlSanitizer htmlSanitizer)
        {
            _context = context;
            _encryptionService = encryptionService;
            _chatMessageService = chatMessageService;
            _htmlSanitizer = htmlSanitizer;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string receiverId, string plainMessage)
        {
            var senderId = Context.UserIdentifier;

            if (string.IsNullOrEmpty(senderId))
            {
                await Clients.Caller.SendAsync("Error", "Identity verification error.");
                return;
            }

            if (string.IsNullOrEmpty(receiverId))
            {
                await Clients.Caller.SendAsync("Error", "Invalid recipient.");
                return;
            }

            if (receiverId == senderId)
            {
                await Clients.Caller.SendAsync("Error", "You cannot send messages to yourself.");
                return;
            }

            var receiverExists = await _context.Users.AnyAsync(u => u.Id == receiverId);
            if (!receiverExists)
            {
                await Clients.Caller.SendAsync("Error", "User not found.");
                return;
            }

            if (string.IsNullOrWhiteSpace(plainMessage))
            {
                await Clients.Caller.SendAsync("Error", "The message cannot be empty.");
                return;
            }

            ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));

            bool iBlockedThem = await _chatMessageService.IsUserBlockedByMeAsync(senderId, receiverId);
            bool theyBlockedMe = await _chatMessageService.HasBlockedMeAsync(senderId, receiverId);

            if (iBlockedThem || theyBlockedMe)
            {
                await Clients.Caller.SendAsync("ReceiveSystemMessage",
                    "Message could not be sent: There is a block between you and the user.");
                return;
            }

            var removedEntry = await _context.MessageUserBlockLists
                .FirstOrDefaultAsync(x => x.BlockerId == senderId && x.BlockedId == receiverId);

            if (removedEntry?.IsRemoved == true)
            {
                removedEntry.IsRemoved = false;
                removedEntry.BlockedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            var encryptedContent = _encryptionService.Encrypt(plainMessage.Trim());
            string safeEncryptedContent = _htmlSanitizer.Sanitize(encryptedContent ?? string.Empty);

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = safeEncryptedContent,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var decryptedContent = _encryptionService.Decrypt(safeEncryptedContent);
            string safeDecryptedContent = _htmlSanitizer.Sanitize(decryptedContent ?? string.Empty);

            var messageDto = new
            {
                Id = message.Id,
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = safeDecryptedContent,
                SentAt = message.SentAt.ToString("o"),
                IsRead = false
            };

            await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", messageDto);
            await Clients.Caller.SendAsync("ReceiveMessage", messageDto);
            await Clients.User(receiverId).SendAsync("UpdateUnreadCount", await _chatMessageService.GetUnreadCountAsync(receiverId));            
        }
        public async Task MarkAsRead(int messageId)
        {
            var userId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(userId)) return;

            var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId && m.ReceiverId == userId && m.IsRead == false);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
                await Clients.User(message.SenderId).SendAsync("MessageRead", messageId);
            }
        }
        public async Task ConversationRemoved(string otherUserId)
        {
            await Clients.User(Context.UserIdentifier).SendAsync("ConversationRemoved", otherUserId);
        }
    }
}
