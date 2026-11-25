using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Interaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Interaction_Configurations
{
    public class InteractionRulesetConfiguration : IEntityTypeConfiguration<InteractionRuleset>
    {
        public void Configure(EntityTypeBuilder<InteractionRuleset> builder)
        {
            builder.ToTable("INTERACTION_RULESET");

            builder.HasKey(ir => ir.RuleSetId);

            builder.Property(ir => ir.Version).HasMaxLength(50).IsRequired();
            builder.Property(ir => ir.Status).HasMaxLength(50).IsRequired();

            builder.HasOne(ir => ir.ApprovedByUser)
                .WithMany(u => u.ApprovedRulesets)
                .HasForeignKey(ir => ir.ApprovedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
