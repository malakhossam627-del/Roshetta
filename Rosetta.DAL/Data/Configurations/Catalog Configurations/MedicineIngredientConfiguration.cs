using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Catalog_Configurations
{
    public class MedicineIngredientConfiguration : IEntityTypeConfiguration<MedicineIngredient>
    {
        public void Configure(EntityTypeBuilder<MedicineIngredient> builder)
        {
            builder.ToTable("MEDICINE_INGREDIENT");

            builder.HasKey(mi => new { mi.MedicineId, mi.IngredientId });

            builder.Property(mi => mi.IngredientStrength).HasMaxLength(100);

            builder.HasOne(mi => mi.Medicine)
                .WithMany(m => m.MedicineIngredients)
                .HasForeignKey(mi => mi.MedicineId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(mi => mi.Ingredient)
                .WithMany(i => i.MedicineIngredients)
                .HasForeignKey(mi => mi.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
