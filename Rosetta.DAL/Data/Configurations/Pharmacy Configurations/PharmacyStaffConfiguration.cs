using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Pharmacy_Configurations
{
    public class PharmacyStaffConfiguration : IEntityTypeConfiguration<PharmacyStaff>
    {
        public void Configure(EntityTypeBuilder<PharmacyStaff> builder)
        {
            builder.ToTable("PHARMACY_STAFF");

            builder.HasKey(ps => new { ps.PharmacyId, ps.UserId });

            builder.Property(ps => ps.StaffRole).HasMaxLength(100);

            builder.HasOne(ps => ps.Pharmacy)
                .WithMany(p => p.Staff)
                .HasForeignKey(ps => ps.PharmacyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ps => ps.User)
                .WithMany(u => u.PharmacyStaffRoles)
                .HasForeignKey(ps => ps.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
