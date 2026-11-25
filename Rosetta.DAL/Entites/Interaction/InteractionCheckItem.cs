using Rosetta.DAL.Entites.Catalog;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Interaction
{
    public class InteractionCheckItem
    {
        public int CheckItemId { get; set; }
        public int InteractionCheckId { get; set; }
        public int PlanItemId { get; set; }
        public int MedicineId { get; set; }
        public string? EnteredName { get; set; }

        public virtual InteractionCheck InteractionCheck { get; set; } = null!;
        public virtual PlanItem PlanItem { get; set; } = null!;
        public virtual Medicine Medicine { get; set; } = null!;
        public virtual ICollection<CheckItemIngredient> Ingredients { get; set; } = new HashSet<CheckItemIngredient>();
        public virtual ICollection<InteractionCheckResult> CheckResults { get; set; } = new HashSet<InteractionCheckResult>();
    }
}
