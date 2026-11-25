using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Catalog_Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("PRODUCT");

            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.BrandNameEn).HasMaxLength(255);
            builder.Property(p => p.BrandNameAr).HasMaxLength(255);
            builder.Property(p => p.Strength).HasMaxLength(100);
            builder.Property(p => p.DosageForm).HasMaxLength(100);
            builder.Property(p => p.PackSize).HasMaxLength(100);
            builder.Property(p => p.SKU).HasMaxLength(100);

            builder.HasIndex(p => p.SKU).IsUnique();

            builder.HasOne(p => p.Medicine)
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.MedicineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
