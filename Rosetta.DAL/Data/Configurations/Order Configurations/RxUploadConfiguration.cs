using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Order_Configurations
{
    public class RxUploadConfiguration : IEntityTypeConfiguration<RxUpload>
    {
        public void Configure(EntityTypeBuilder<RxUpload> builder)
        {
            builder.ToTable("RX_UPLOAD");

            builder.HasKey(ru => ru.RxUploadId);

            builder.Property(ru => ru.FileReference).HasMaxLength(500).IsRequired();
            builder.Property(ru => ru.ReviewStatus).HasMaxLength(50);

            builder.HasOne(ru => ru.Order)
                .WithMany(o => o.RxUploads)
                .HasForeignKey(ru => ru.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ru => ru.UploadedByUser)
                .WithMany(u => u.RxUploads)
                .HasForeignKey(ru => ru.UploadedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
