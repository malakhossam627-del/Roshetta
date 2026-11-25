using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Plan
{
    public class ScheduleWeekday
    {
        public int ScheduleId { get; set; }
        public int DayOfWeek { get; set; }

        public virtual MedicationSchedule Schedule { get; set; } = null!;
    }
}
