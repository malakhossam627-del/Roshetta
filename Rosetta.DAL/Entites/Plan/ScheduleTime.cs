using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Plan
{
    public class ScheduleTime
    {
        public int ScheduleTimeId { get; set; }
        public int ScheduleId { get; set; }
        public TimeSpan LocalTime { get; set; }

        public virtual MedicationSchedule Schedule { get; set; } = null!;
    }
}
