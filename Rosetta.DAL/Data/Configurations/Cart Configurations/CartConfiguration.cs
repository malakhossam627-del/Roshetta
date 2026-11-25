using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Cart_Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("CART");

            builder.HasKey(c => c.CartId);

            builder.Property(c => c.Status).HasMaxLength(50).IsRequired();

            builder.HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Pharmacy)
            .WithMany(p => p.Carts)
            .HasForeignKey(c => c.PharmacyId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
