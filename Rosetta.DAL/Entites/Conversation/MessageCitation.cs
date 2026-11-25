using Rosetta.DAL.Entites.KnowledgeBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Conversation
{
    public class MessageCitation
    {
        public int MessageId { get; set; }
        public int ChunkId { get; set; }
        public int CitationOrder { get; set; }

        public virtual Message Message { get; set; } = null!;
        public virtual KbChunk Chunk { get; set; } = null!;
    }
}
