using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Plan
{
    public class MedicationPlan
    {
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = "Active";
        public string? TimeZone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual AppUser User { get; set; } = null!;
        public virtual ICollection<PlanItem> PlanItems { get; set; } = new HashSet<PlanItem>();
    }
}
