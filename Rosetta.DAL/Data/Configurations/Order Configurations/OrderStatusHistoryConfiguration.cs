using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Order_Configurations
{
    public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
        {
            builder.ToTable("ORDER_STATUS_HISTORY");

            builder.HasKey(osh => osh.StatusHistoryId);

            builder.Property(osh => osh.Status).HasMaxLength(50).IsRequired();

            builder.HasOne(osh => osh.Order)
                .WithMany(o => o.StatusHistory)
                .HasForeignKey(osh => osh.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(osh => osh.ChangedByUser)
                .WithMany(u => u.OrderStatusHistories)
                .HasForeignKey(osh => osh.ChangedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
