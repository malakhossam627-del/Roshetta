using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Pharmacy_Configurations
{
    public class PharmacyConfiguration : IEntityTypeConfiguration<PharmacyModel>
    {
        public void Configure(EntityTypeBuilder<PharmacyModel> builder)
        {
            builder.ToTable("PHARMACY");

            builder.HasKey(p => p.PharmacyId);

            builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
            builder.Property(p => p.LicenseNumber).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Phone).HasMaxLength(50);
            builder.Property(p => p.AddressText).HasMaxLength(500);

            builder.HasIndex(p => p.LicenseNumber).IsUnique();
        }
    }
}
