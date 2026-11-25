using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rosetta.DAL.Entites.KnowledgeBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Data.Configurations.KnowledgeBase_Configurations
{
    public class KbChunkConfiguration : IEntityTypeConfiguration<KbChunk>
    {
        public void Configure(EntityTypeBuilder<KbChunk> builder)
        {
            builder.ToTable("KB_CHUNK");

            builder.HasKey(kc => kc.ChunkId);

            builder.Property(kc => kc.EmbeddingReference).HasMaxLength(500);

            builder.HasOne(kc => kc.Document)
                .WithMany(kd => kd.Chunks)
                .HasForeignKey(kc => kc.DocumentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
