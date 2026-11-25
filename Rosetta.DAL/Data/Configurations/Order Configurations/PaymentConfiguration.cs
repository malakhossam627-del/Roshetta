using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Order_Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("PAYMENT");

            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.Method).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Amount).HasPrecision(18, 2);
            builder.Property(p => p.Status).HasMaxLength(50).IsRequired();

            builder.HasIndex(p => p.OrderId).IsUnique();

            builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
