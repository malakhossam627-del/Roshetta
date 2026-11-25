using Rosetta.DAL.Entites.Core;
using Rosetta.DAL.Entites.Order;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Notification
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public int? DoseEventId { get; set; }
        public int? OrderId { get; set; }
        public int? RefillAlertId { get; set; }
        public string NotificationType { get; set; } = string.Empty;
        public string? Channel { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime? ScheduledAtUtc { get; set; }
        public DateTime? SentAtUtc { get; set; }
        public int RetryCount { get; set; }

        public virtual AppUser User { get; set; } = null!;
        public virtual DoseEvent? DoseEvent { get; set; }
        public virtual CustomerOrder? Order { get; set; }
        public virtual RefillAlert? RefillAlert { get; set; }
    }
}
