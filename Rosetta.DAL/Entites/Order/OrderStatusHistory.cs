using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Order
{
    public class OrderStatusHistory
    {
        public int StatusHistoryId { get; set; }
        public int OrderId { get; set; }
        public int ChangedByUserId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public virtual CustomerOrder Order { get; set; } = null!;
        public virtual AppUser ChangedByUser { get; set; } = null!;
    }
}
