using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Order_Configurations
{
    public class CustomerOrderConfiguration : IEntityTypeConfiguration<CustomerOrder>
    {
        public void Configure(EntityTypeBuilder<CustomerOrder> builder)
        {
            builder.ToTable("CUSTOMER_ORDER");

            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderNumber).HasMaxLength(100).IsRequired();
            builder.Property(o => o.Status).HasMaxLength(50).IsRequired();
            builder.Property(o => o.RxReviewStatus).HasMaxLength(50);
            builder.Property(o => o.TotalAmount).HasPrecision(18, 2);

            builder.HasIndex(o => o.OrderNumber).IsUnique();

            builder.HasOne(o => o.CustomerUser)
                .WithMany(u => u.OrdersAsCustomer)
                .HasForeignKey(o => o.CustomerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Pharmacy)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PharmacyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.ReviewedByUser)
                .WithMany(u => u.OrdersAsReviewer)
                .HasForeignKey(o => o.ReviewedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
