using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Order_Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("ORDER_ITEM");

            builder.HasKey(oi => oi.OrderItemId);

            builder.Property(oi => oi.UnitPriceSnapshot).HasPrecision(18, 2);
            builder.Property(oi => oi.ProductNameSnapshot).HasMaxLength(255);
            builder.Property(oi => oi.LineTotal).HasPrecision(18, 2);

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Inventory)
                .WithMany(inv => inv.OrderItems)
                .HasForeignKey(oi => oi.InventoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
