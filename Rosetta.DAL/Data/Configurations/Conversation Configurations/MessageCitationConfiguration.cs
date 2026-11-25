using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.Conversation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.Conversation_Configurations
{
    public class MessageCitationConfiguration : IEntityTypeConfiguration<MessageCitation>
    {
        public void Configure(EntityTypeBuilder<MessageCitation> builder)
        {
            builder.ToTable("MESSAGE_CITATION");

            builder.HasKey(mc => new { mc.MessageId, mc.ChunkId });

            builder.HasOne(mc => mc.Message)
                .WithMany(m => m.Citations)
                .HasForeignKey(mc => mc.MessageId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(mc => mc.Chunk)
                .WithMany(kc => kc.Citations)
                .HasForeignKey(mc => mc.ChunkId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
