using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.KnowledgeBase
{
    public class KbDocument
    {
        public int DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? SourceName { get; set; }
        public string? SourceUrl { get; set; }
        public string? Language { get; set; }
        public string? Version { get; set; }
        public string? ReviewStatus { get; set; }
        public int? ReviewedByUserId { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public DateTime? PublishedAt { get; set; }

        public virtual AppUser? ReviewedByUser { get; set; }
        public virtual ICollection<KbChunk> Chunks { get; set; } = new HashSet<KbChunk>();
    }
}
