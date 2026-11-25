using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Interaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Interaction_Configurations
{
    public class InteractionCheckConfiguration : IEntityTypeConfiguration<InteractionCheck>
    {
        public void Configure(EntityTypeBuilder<InteractionCheck> builder)
        {
            builder.ToTable("INTERACTION_CHECK");

            builder.HasKey(ic => ic.InteractionCheckId);

            builder.HasOne(ic => ic.User)
                .WithMany(u => u.InteractionChecks)
                .HasForeignKey(ic => ic.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ic => ic.RuleSet)
                .WithMany(ir => ir.InteractionChecks)
                .HasForeignKey(ic => ic.RuleSetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ic => ic.Consent)
                .WithMany(cr => cr.InteractionChecks)
                .HasForeignKey(ic => ic.ConsentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
