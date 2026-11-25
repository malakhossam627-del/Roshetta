using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Core_Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AUDIT_LOG");

            builder.HasKey(al => al.AuditLogId);

            builder.Property(al => al.Action).HasMaxLength(100).IsRequired();
            builder.Property(al => al.EntityType).HasMaxLength(100).IsRequired();

            builder.HasOne(al => al.ActorUser)
                .WithMany(u => u.AuditLogsAsActor)
                .HasForeignKey(al => al.ActorUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
