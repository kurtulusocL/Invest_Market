
namespace Investigation.Shared.ViewModels.ChatMessage
{
    public class UserConversationVM
    {
        public string PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerEmail { get; set; }
        public string LastMessage { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public int UnreadCount { get; set; }
        public List<MessageVM> Messages { get; set; } = new();
    }
}
