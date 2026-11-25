using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Interaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Interaction_Configurations
{
    public class InteractionConfiguration : IEntityTypeConfiguration<Interaction>
    {
        public void Configure(EntityTypeBuilder<Interaction> builder)
        {
            builder.ToTable("INTERACTION");

            builder.HasKey(i => i.InteractionId);

            builder.Property(i => i.Severity).HasMaxLength(50);
            builder.Property(i => i.SourceReference).HasMaxLength(500);

            builder.HasOne(i => i.RuleSet)
                .WithMany(ir => ir.Interactions)
                .HasForeignKey(i => i.RuleSetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.IngredientA)
                .WithMany(ing => ing.InteractionsAsIngredientA)
                .HasForeignKey(i => i.IngredientAId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.IngredientB)
                .WithMany(ing => ing.InteractionsAsIngredientB)
                .HasForeignKey(i => i.IngredientBId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
