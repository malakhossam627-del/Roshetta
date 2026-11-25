using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Interaction
{
    public class InteractionRuleset
    {
        public int RuleSetId { get; set; }
        public string Version { get; set; } = string.Empty;
        public string Status { get; set; } = "Draft";
        public int? ApprovedByUserId { get; set; }
        public DateTime? ApprovedAt { get; set; }

        public virtual AppUser? ApprovedByUser { get; set; }
        public virtual ICollection<Interaction> Interactions { get; set; } = new HashSet<Interaction>();
        public virtual ICollection<InteractionCheck> InteractionChecks { get; set; } = new HashSet<InteractionCheck>();
    }
}
