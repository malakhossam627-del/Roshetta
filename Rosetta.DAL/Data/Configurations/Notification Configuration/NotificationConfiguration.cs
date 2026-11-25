using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Notification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Notification_Configuration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("NOTIFICATION");

            builder.HasKey(n => n.NotificationId);

            builder.Property(n => n.NotificationType).HasMaxLength(100).IsRequired();
            builder.Property(n => n.Channel).HasMaxLength(50);
            builder.Property(n => n.Status).HasMaxLength(50).IsRequired();

            builder.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(n => n.DoseEvent)
                .WithMany(de => de.Notifications)
                .HasForeignKey(n => n.DoseEventId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(n => n.Order)
                .WithMany(o => o.Notifications)
                .HasForeignKey(n => n.OrderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(n => n.RefillAlert)
                .WithMany(ra => ra.Notifications)
                .HasForeignKey(n => n.RefillAlertId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
