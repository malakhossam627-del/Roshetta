using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Plan_Configurations
{
    public class ScheduleTimeConfiguration : IEntityTypeConfiguration<ScheduleTime>
    {
        public void Configure(EntityTypeBuilder<ScheduleTime> builder)
        {
            builder.ToTable("SCHEDULE_TIME");

            builder.HasKey(st => st.ScheduleTimeId);

            builder.HasOne(st => st.Schedule)
                .WithMany(ms => ms.ScheduleTimes)
                .HasForeignKey(st => st.ScheduleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
