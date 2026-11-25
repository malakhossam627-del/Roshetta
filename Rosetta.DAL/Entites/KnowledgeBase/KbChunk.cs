using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.KnowledgeBase
{
    public class KbChunk
    {
        public int ChunkId { get; set; }
        public int DocumentId { get; set; }
        public int ChunkIndex { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? EmbeddingReference { get; set; }

        public virtual KbDocument Document { get; set; } = null!;
        public virtual ICollection<Conversation.MessageCitation> Citations { get; set; } = new HashSet<Conversation.MessageCitation>();
    }
}
