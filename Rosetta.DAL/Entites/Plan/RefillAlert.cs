using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Plan
{
    public class RefillAlert
    {
        public int RefillAlertId { get; set; }
        public int PlanItemId { get; set; }
        public DateTime TriggeredAt { get; set; }
        public decimal? ThresholdAtTrigger { get; set; }
        public string Status { get; set; } = "Pending";

        public virtual PlanItem PlanItem { get; set; } = null!;
        public virtual ICollection<Notification.Notification> Notifications { get; set; } = new HashSet<Notification.Notification>();
    }
}
