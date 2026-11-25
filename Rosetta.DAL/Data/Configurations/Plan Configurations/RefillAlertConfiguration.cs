using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Plan_Configurations
{
    public class RefillAlertConfiguration : IEntityTypeConfiguration<RefillAlert>
    {
        public void Configure(EntityTypeBuilder<RefillAlert> builder)
        {
            builder.ToTable("REFILL_ALERT");

            builder.HasKey(ra => ra.RefillAlertId);

            builder.Property(ra => ra.Status).HasMaxLength(50).IsRequired();
            builder.Property(ra => ra.ThresholdAtTrigger).HasPrecision(18, 2);

            builder.HasOne(ra => ra.PlanItem)
                .WithMany(pi => pi.RefillAlerts)
                .HasForeignKey(ra => ra.PlanItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
