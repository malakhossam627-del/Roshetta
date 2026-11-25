using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Interaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Interaction_Configurations
{
    public class InteractionCheckResultConfiguration : IEntityTypeConfiguration<InteractionCheckResult>
    {
        public void Configure(EntityTypeBuilder<InteractionCheckResult> builder)
        {
            builder.ToTable("INTERACTION_CHECK_RESULT");

            builder.HasKey(icr => icr.ResultId);

            builder.Property(icr => icr.SeveritySnapshot).HasMaxLength(50);

            builder.HasOne(icr => icr.InteractionCheck)
                .WithMany(ic => ic.CheckResults)
                .HasForeignKey(icr => icr.InteractionCheckId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(icr => icr.Interaction)
                .WithMany(i => i.CheckResults)
                .HasForeignKey(icr => icr.InteractionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(icr => icr.CheckItem)
                .WithMany(ici => ici.CheckResults)
                .HasForeignKey(icr => icr.CheckItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
