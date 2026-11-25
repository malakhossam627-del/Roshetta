using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Interaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Interaction_Configurations
{
    public class InteractionCheckItemConfiguration : IEntityTypeConfiguration<InteractionCheckItem>
    {
        public void Configure(EntityTypeBuilder<InteractionCheckItem> builder)
        {
            builder.ToTable("INTERACTION_CHECK_ITEM");

            builder.HasKey(ici => ici.CheckItemId);

            builder.Property(ici => ici.EnteredName).HasMaxLength(255);

            builder.HasOne(ici => ici.InteractionCheck)
                .WithMany(ic => ic.CheckItems)
                .HasForeignKey(ici => ici.InteractionCheckId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ici => ici.PlanItem)
                .WithMany(pi => pi.InteractionCheckItems)
                .HasForeignKey(ici => ici.PlanItemId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ici => ici.Medicine)
                .WithMany(m => m.InteractionCheckItems)
                .HasForeignKey(ici => ici.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
