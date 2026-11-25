using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Plan
{
    public class DoseEvent
    {
        public int DoseEventId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime ScheduledAtUtc { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime? ActionedAtUtc { get; set; }
        public DateTime? SnoozedUntilUtc { get; set; }

        public virtual MedicationSchedule Schedule { get; set; } = null!;
        public virtual ICollection<Notification.Notification> Notifications { get; set; } = new HashSet<Notification.Notification>();
    }
}
