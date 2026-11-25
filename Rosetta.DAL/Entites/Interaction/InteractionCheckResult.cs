using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Interaction
{
    public class InteractionCheckResult
    {
        public int ResultId { get; set; }
        public int InteractionCheckId { get; set; }
        public int InteractionId { get; set; }
        public int CheckItemId { get; set; }
        public string? SeveritySnapshot { get; set; }
        public string? ExplanationSnapshot { get; set; }

        public virtual InteractionCheck InteractionCheck { get; set; } = null!;
        public virtual Interaction Interaction { get; set; } = null!;
        public virtual InteractionCheckItem CheckItem { get; set; } = null!;
    }
}
