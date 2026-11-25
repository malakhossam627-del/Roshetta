using Microsoft.VisualBasic;
using Rosetta.DAL.Entites.Consent;
using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Interaction
{
    public class InteractionCheck
    {
        public int InteractionCheckId { get; set; }
        public int UserId { get; set; }
        public int RuleSetId { get; set; }
        public int? ConsentId { get; set; }
        public DateTime CheckedAt { get; set; } = DateTime.UtcNow;
        public string? ResultSummary { get; set; }

        public virtual AppUser User { get; set; } = null!;
        public virtual InteractionRuleset RuleSet { get; set; } = null!;
        public virtual ConsentRecord? Consent { get; set; }
        public virtual ICollection<InteractionCheckItem> CheckItems { get; set; } = new HashSet<InteractionCheckItem>();
        public virtual ICollection<InteractionCheckResult> CheckResults { get; set; } = new HashSet<InteractionCheckResult>();
        public virtual ICollection<Conversation.SafetyFlag> SafetyFlags { get; set; } = new HashSet<Conversation.SafetyFlag>();
    }
}
