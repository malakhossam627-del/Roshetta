using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;

namespace Rosetta.DAL.Entites.Core
{
    public class AppUser
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? PreferredLanguage { get; set; }
        public string? TimeZone { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public virtual ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public virtual ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
        public virtual ICollection<AuditLog> AuditLogsAsActor { get; set; } = new HashSet<AuditLog>();
        public virtual ICollection<Conversation.Conversation> Conversations { get; set; } = new HashSet<Conversation.Conversation>();
        public virtual ICollection<KnowledgeBase.KbDocument> ReviewedDocuments { get; set; } = new HashSet<KnowledgeBase.KbDocument>();
        public virtual ICollection<Plan.MedicationPlan> MedicationPlans { get; set; } = new HashSet<Plan.MedicationPlan>();
        public virtual ICollection<Consent.ConsentRecord> ConsentRecords { get; set; } = new HashSet<Consent.ConsentRecord>();
        public virtual ICollection<Catalog.Medicine> ReviewedMedicines { get; set; } = new HashSet<Catalog.Medicine>();
        public virtual ICollection<Interaction.InteractionRuleset> ApprovedRulesets { get; set; } = new HashSet<Interaction.InteractionRuleset>();
        public virtual ICollection<Order.CustomerOrder> OrdersAsCustomer { get; set; } = new HashSet<Order.CustomerOrder>();
        public virtual ICollection<Order.CustomerOrder> OrdersAsReviewer { get; set; } = new HashSet<Order.CustomerOrder>();
        public virtual ICollection<Cart.Cart> Carts { get; set; } = new HashSet<Cart.Cart>();
        public virtual ICollection<Pharmacy.PharmacyStaff> PharmacyStaffRoles { get; set; } = new HashSet<Pharmacy.PharmacyStaff>();
        public virtual ICollection<Interaction.InteractionCheck> InteractionChecks { get; set; } = new HashSet<Interaction.InteractionCheck>();
        public virtual ICollection<Order.OrderStatusHistory> OrderStatusHistories { get; set; } = new HashSet<Order.OrderStatusHistory>();
        public virtual ICollection<Order.RxUpload> RxUploads { get; set; } = new HashSet<Order.RxUpload>();
        public virtual ICollection<Conversation.SafetyFlag> SafetyFlagsAsUser { get; set; } = new HashSet<Conversation.SafetyFlag>();
        public virtual ICollection<Conversation.SafetyFlag> SafetyFlagsAsReviewer { get; set; } = new HashSet<Conversation.SafetyFlag>();
        public virtual ICollection<Conversation.MessageFeedback> MessageFeedbacks { get; set; } = new HashSet<Conversation.MessageFeedback>();
        public virtual ICollection<Notification.Notification> Notifications { get; set; } = new HashSet<Notification.Notification>();
    }
}

