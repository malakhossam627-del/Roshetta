using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Interaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Interaction_Configurations
{
    public class CheckItemIngredientConfiguration : IEntityTypeConfiguration<CheckItemIngredient>
    {
        public void Configure(EntityTypeBuilder<CheckItemIngredient> builder)
        {
            builder.ToTable("CHECK_ITEM_INGREDIENT");

            builder.HasKey(cii => new { cii.CheckItemId, cii.IngredientId });

            builder.HasOne(cii => cii.CheckItem)
                .WithMany(ici => ici.Ingredients)
                .HasForeignKey(cii => cii.CheckItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cii => cii.Ingredient)
                .WithMany(i => i.CheckItemIngredients)
                .HasForeignKey(cii => cii.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
