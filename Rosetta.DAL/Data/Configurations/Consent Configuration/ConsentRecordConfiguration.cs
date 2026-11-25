using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Consent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Consent_Configuration
{
    public class ConsentRecordConfiguration : IEntityTypeConfiguration<ConsentRecord>
    {
        public void Configure(EntityTypeBuilder<ConsentRecord> builder)
        {
            builder.ToTable("CONSENT_RECORD");

            builder.HasKey(cr => cr.ConsentId);

            builder.Property(cr => cr.Purpose).HasMaxLength(500).IsRequired();
            builder.Property(cr => cr.PolicyVersion).HasMaxLength(50);

            builder.HasOne(cr => cr.User)
                .WithMany(u => u.ConsentRecords)
                .HasForeignKey(cr => cr.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
