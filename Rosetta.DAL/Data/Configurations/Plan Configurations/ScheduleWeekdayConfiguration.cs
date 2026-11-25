using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Plan_Configurations
{
    public class ScheduleWeekdayConfiguration : IEntityTypeConfiguration<ScheduleWeekday>
    {
        public void Configure(EntityTypeBuilder<ScheduleWeekday> builder)
        {
            builder.ToTable("SCHEDULE_WEEKDAY");

            builder.HasKey(sw => new { sw.ScheduleId, sw.DayOfWeek });

            builder.HasOne(sw => sw.Schedule)
                .WithMany(ms => ms.Weekdays)
                .HasForeignKey(sw => sw.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
