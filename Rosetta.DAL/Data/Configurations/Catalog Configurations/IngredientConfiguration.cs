using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Catalog_Configurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("INGREDIENT");

            builder.HasKey(i => i.IngredientId);

            builder.Property(i => i.NameEn).HasMaxLength(255).IsRequired();
            builder.Property(i => i.NameAr).HasMaxLength(255).IsRequired();
        }
    }
}
