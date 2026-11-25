using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Pharmacy_Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("INVENTORY");

            builder.HasKey(inv => inv.InventoryId);

            builder.Property(inv => inv.UnitPrice)
                .HasPrecision(18, 2);

            builder.HasOne(inv => inv.Pharmacy)
                .WithMany(p => p.Inventories)
                .HasForeignKey(inv => inv.PharmacyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(inv => inv.Product)
                .WithMany(p => p.Inventories)
                .HasForeignKey(inv => inv.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
