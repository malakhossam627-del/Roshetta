using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Plan_Configurations
{
    public class MedicationScheduleConfiguration : IEntityTypeConfiguration<MedicationSchedule>
    {
        public void Configure(EntityTypeBuilder<MedicationSchedule> builder)
        {
            builder.ToTable("MEDICATION_SCHEDULE");

            builder.HasKey(ms => ms.ScheduleId);

            builder.Property(ms => ms.ScheduleType).HasMaxLength(50).IsRequired();

            builder.HasOne(ms => ms.PlanItem)
                .WithMany(pi => pi.Schedules)
                .HasForeignKey(ms => ms.PlanItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
