using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Conversation
{
    public class Message
    {
        public int MessageId { get; set; }
        public int ConversationId { get; set; }
        public string SenderType { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? SafetyStatus { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Conversation Conversation { get; set; } = null!;
        public virtual ICollection<MessageCitation> Citations { get; set; } = new HashSet<MessageCitation>();
        public virtual ICollection<MessageFeedback> Feedbacks { get; set; } = new HashSet<MessageFeedback>();
        public virtual ICollection<SafetyFlag> SafetyFlags { get; set; } = new HashSet<SafetyFlag>();
    }
}
