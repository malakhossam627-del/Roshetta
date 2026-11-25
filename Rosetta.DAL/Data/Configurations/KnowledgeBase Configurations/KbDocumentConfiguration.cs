using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.KnowledgeBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.KnowledgeBase_Configurations
{
    public class KbDocumentConfiguration : IEntityTypeConfiguration<KbDocument>
    {
        public void Configure(EntityTypeBuilder<KbDocument> builder)
        {
            builder.ToTable("KB_DOCUMENT");

            builder.HasKey(kd => kd.DocumentId);

            builder.Property(kd => kd.Title).HasMaxLength(500).IsRequired();
            builder.Property(kd => kd.SourceName).HasMaxLength(255);
            builder.Property(kd => kd.SourceUrl).HasMaxLength(1000);
            builder.Property(kd => kd.Language).HasMaxLength(10);
            builder.Property(kd => kd.Version).HasMaxLength(50);
            builder.Property(kd => kd.ReviewStatus).HasMaxLength(50);

            builder.HasOne(kd => kd.ReviewedByUser)
                .WithMany(u => u.ReviewedDocuments)
                .HasForeignKey(kd => kd.ReviewedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
