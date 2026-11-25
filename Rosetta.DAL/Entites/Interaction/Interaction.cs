using Rosetta.DAL.Entites.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Interaction
{
    public class Interaction
    {
        public int InteractionId { get; set; }
        public int RuleSetId { get; set; }
        public int IngredientAId { get; set; }
        public int IngredientBId { get; set; }
        public string? Severity { get; set; }
        public string? ClinicalEffect { get; set; }
        public string? RecommendedAction { get; set; }
        public string? SourceReference { get; set; }

        public virtual InteractionRuleset RuleSet { get; set; } = null!;
        public virtual Ingredient IngredientA { get; set; } = null!;
        public virtual Ingredient IngredientB { get; set; } = null!;
        public virtual ICollection<InteractionCheckResult> CheckResults { get; set; } = new HashSet<InteractionCheckResult>();
    }
}
