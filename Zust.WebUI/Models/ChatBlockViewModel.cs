using Zust.Entity.Entities;

namespace Zust.WebUI.Models
{
    public class ChatBlockViewModel
    {

        public int Id { get; set; }
        public string? ReceiverId { get; set; }
        public CustomUser? Receiver { get; set; }
        public string? SenderId { get; set; }
        public virtual List<Message>? Messages { get; set; }
        public int UnReadMessageCount { get; set; }
    }
}
