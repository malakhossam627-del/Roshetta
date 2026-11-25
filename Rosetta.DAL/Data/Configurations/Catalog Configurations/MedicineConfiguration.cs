using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Catalog_Configurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("MEDICINE");

            builder.HasKey(m => m.MedicineId);

            builder.Property(m => m.NameEn).HasMaxLength(255).IsRequired();
            builder.Property(m => m.NameAr).HasMaxLength(255).IsRequired();
            builder.Property(m => m.IntendedUse).HasMaxLength(500);
            builder.Property(m => m.PrescriptionType).HasMaxLength(50);
            builder.Property(m => m.ContentStatus).HasMaxLength(50);
            builder.Property(m => m.ContentVersion).HasMaxLength(50);

            builder.HasOne(m => m.ReviewedByUser)
                .WithMany(u => u.ReviewedMedicines)
                .HasForeignKey(m => m.ReviewedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
