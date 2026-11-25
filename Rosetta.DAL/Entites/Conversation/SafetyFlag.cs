using Rosetta.DAL.Entites.Core;
using Rosetta.DAL.Entites.Interaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Conversation
{
    public class SafetyFlag
    {
        public int SafetyFlagId { get; set; }
        public int UserId { get; set; }
        public int ConversationId { get; set; }
        public int MessageId { get; set; }
        public int? InteractionCheckId { get; set; }
        public string FlagType { get; set; } = string.Empty;
        public string? Severity { get; set; }
        public string? Reason { get; set; }
        public string Status { get; set; } = "Open";
        public int? ReviewedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReviewedAt { get; set; }

        public virtual AppUser User { get; set; } = null!;
        public virtual Conversation Conversation { get; set; } = null!;
        public virtual Message Message { get; set; } = null!;
        public virtual InteractionCheck? InteractionCheck { get; set; }
        public virtual AppUser? ReviewedByUser { get; set; }
    }
}
