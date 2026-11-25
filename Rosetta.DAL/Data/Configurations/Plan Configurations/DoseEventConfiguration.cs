using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Plan_Configurations
{
    public class DoseEventConfiguration : IEntityTypeConfiguration<DoseEvent>
    {
        public void Configure(EntityTypeBuilder<DoseEvent> builder)
        {
            builder.ToTable("DOSE_EVENT");

            builder.HasKey(de => de.DoseEventId);

            builder.Property(de => de.Status).HasMaxLength(50).IsRequired();

            builder.HasOne(de => de.Schedule)
                .WithMany(ms => ms.DoseEvents)
                .HasForeignKey(de => de.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
