using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Consent
{
    public class ConsentRecord
    {
        public int ConsentId { get; set; }
        public int UserId { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public bool IsGranted { get; set; }
        public string? PolicyVersion { get; set; }
        public DateTime? GrantedAt { get; set; }
        public DateTime? WithdrawnAt { get; set; }

        public virtual AppUser User { get; set; } = null!;
        public virtual ICollection<Interaction.InteractionCheck> InteractionChecks { get; set; } = new HashSet<Interaction.InteractionCheck>();
    }
}
