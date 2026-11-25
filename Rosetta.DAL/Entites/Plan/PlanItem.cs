using Microsoft.VisualBasic;
using Rosetta.DAL.Entites.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Plan
{
    public class PlanItem
    {
        public int PlanItemId { get; set; }
        public int PlanId { get; set; }
        public int MedicineId { get; set; }
        public int? ProductId { get; set; }
        public string? MedicineNameEntered { get; set; }
        public decimal? DoseAmount { get; set; }
        public string? DoseUnit { get; set; }
        public string? Instructions { get; set; }
        public decimal? SupplyRemaining { get; set; }
        public decimal? RefillThreshold { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual MedicationPlan Plan { get; set; } = null!;
        public virtual Medicine Medicine { get; set; } = null!;
        public virtual Product? Product { get; set; }
        public virtual ICollection<MedicationSchedule> Schedules { get; set; } = new HashSet<MedicationSchedule>();
        public virtual ICollection<RefillAlert> RefillAlerts { get; set; } = new HashSet<RefillAlert>();
        public virtual ICollection<Interaction.InteractionCheckItem> InteractionCheckItems { get; set; } = new HashSet<Interaction.InteractionCheckItem>();
    }
}
