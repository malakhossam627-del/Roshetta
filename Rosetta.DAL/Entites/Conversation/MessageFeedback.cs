using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Conversation
{
    public class MessageFeedback
    {
        public int FeedbackId { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Message Message { get; set; } = null!;
        public virtual AppUser User { get; set; } = null!;
    }
}
