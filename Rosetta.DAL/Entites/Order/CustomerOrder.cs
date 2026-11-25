using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Order
{
    public class CustomerOrder
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public int CustomerUserId { get; set; }
        public int PharmacyId { get; set; }
        public string Status { get; set; } = "Pending";
        public string? RxReviewStatus { get; set; }
        public int? ReviewedByUserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual AppUser CustomerUser { get; set; } = null!;
        public virtual Rosetta.DAL.Entites.Pharmacy.PharmacyModel Pharmacy { get; set; } = null!;
        public virtual AppUser? ReviewedByUser { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<OrderStatusHistory> StatusHistory { get; set; } = new HashSet<OrderStatusHistory>();
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        public virtual ICollection<RxUpload> RxUploads { get; set; } = new HashSet<RxUpload>();
        public virtual ICollection<Notification.Notification> Notifications { get; set; } = new HashSet<Notification.Notification>();
    }
}
