using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Plan
{
    public class MedicationSchedule
    {
        public int ScheduleId { get; set; }
        public int PlanItemId { get; set; }
        public string ScheduleType { get; set; } = string.Empty;
        public int? IntervalHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual PlanItem PlanItem { get; set; } = null!;
        public virtual ICollection<ScheduleWeekday> Weekdays { get; set; } = new HashSet<ScheduleWeekday>();
        public virtual ICollection<DoseEvent> DoseEvents { get; set; } = new HashSet<DoseEvent>();
        public virtual ICollection<ScheduleTime> ScheduleTimes { get; set; } = new HashSet<ScheduleTime>();
    }
}
