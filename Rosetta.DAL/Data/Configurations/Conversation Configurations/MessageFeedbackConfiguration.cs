using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Conversation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Conversation_Configurations
{
    public class MessageFeedbackConfiguration : IEntityTypeConfiguration<MessageFeedback>
    {
        public void Configure(EntityTypeBuilder<MessageFeedback> builder)
        {
            builder.ToTable("MESSAGE_FEEDBACK");

            builder.HasKey(mf => mf.FeedbackId);

            builder.HasOne(mf => mf.Message)
                .WithMany(m => m.Feedbacks)
                .HasForeignKey(mf => mf.MessageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mf => mf.User)
                .WithMany(u => u.MessageFeedbacks)
                .HasForeignKey(mf => mf.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
