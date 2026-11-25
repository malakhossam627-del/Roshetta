using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Conversation
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Active";

        public virtual AppUser User { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
        public virtual ICollection<SafetyFlag> SafetyFlags { get; set; } = new HashSet<SafetyFlag>();
    }
}
