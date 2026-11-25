using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Plan_Configurations
{
    public class PlanItemConfiguration : IEntityTypeConfiguration<PlanItem>
    {
        public void Configure(EntityTypeBuilder<PlanItem> builder)
        {
            builder.ToTable("PLAN_ITEM");

            builder.HasKey(pi => pi.PlanItemId);

            builder.Property(pi => pi.MedicineNameEntered).HasMaxLength(255);
            builder.Property(pi => pi.DoseAmount).HasPrecision(18, 4);
            builder.Property(pi => pi.DoseUnit).HasMaxLength(50);
            builder.Property(pi => pi.SupplyRemaining).HasPrecision(18, 2);
            builder.Property(pi => pi.RefillThreshold).HasPrecision(18, 2);

            builder.HasOne(pi => pi.Plan)
                .WithMany(p => p.PlanItems)
                .HasForeignKey(pi => pi.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pi => pi.Medicine)
                .WithMany(m => m.PlanItems)
                .HasForeignKey(pi => pi.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.PlanItems)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
