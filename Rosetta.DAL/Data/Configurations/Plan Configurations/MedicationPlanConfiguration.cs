using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Plan_Configurations
{
    public class MedicationPlanConfiguration : IEntityTypeConfiguration<MedicationPlan>
    {
        public void Configure(EntityTypeBuilder<MedicationPlan> builder)
        {
            builder.ToTable("MEDICATION_PLAN");

            builder.HasKey(mp => mp.PlanId);

            builder.Property(mp => mp.PlanName).HasMaxLength(255).IsRequired();
            builder.Property(mp => mp.Status).HasMaxLength(50).IsRequired();
            builder.Property(mp => mp.TimeZone).HasMaxLength(100);

            builder.HasOne(mp => mp.User)
                .WithMany(u => u.MedicationPlans)
                .HasForeignKey(mp => mp.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
