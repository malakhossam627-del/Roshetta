using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Core
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public int ActorUserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

        public virtual AppUser ActorUser { get; set; } = null!;
    }
}
