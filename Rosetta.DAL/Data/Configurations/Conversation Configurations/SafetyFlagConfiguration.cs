using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Conversation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Conversation_Configurations
{
    public class SafetyFlagConfiguration : IEntityTypeConfiguration<SafetyFlag>
    {
        public void Configure(EntityTypeBuilder<SafetyFlag> builder)
        {
            builder.ToTable("SAFETY_FLAG");

            builder.HasKey(sf => sf.SafetyFlagId);

            builder.Property(sf => sf.FlagType).HasMaxLength(100).IsRequired();
            builder.Property(sf => sf.Severity).HasMaxLength(50);
            builder.Property(sf => sf.Status).HasMaxLength(50).IsRequired();

            builder.HasOne(sf => sf.User)
                .WithMany(u => u.SafetyFlagsAsUser)
                .HasForeignKey(sf => sf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sf => sf.Conversation)
                .WithMany(c => c.SafetyFlags)
                .HasForeignKey(sf => sf.ConversationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sf => sf.Message)
                .WithMany(m => m.SafetyFlags)
                .HasForeignKey(sf => sf.MessageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sf => sf.InteractionCheck)
                .WithMany(ic => ic.SafetyFlags)
                .HasForeignKey(sf => sf.InteractionCheckId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sf => sf.ReviewedByUser)
                .WithMany(u => u.SafetyFlagsAsReviewer)
                .HasForeignKey(sf => sf.ReviewedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
