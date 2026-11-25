using Microsoft.EntityFrameworkCore;
using Rosetta.DAL.Entites.Cart;
using Rosetta.DAL.Entites.Catalog;
using Rosetta.DAL.Entites.Consent;
using Rosetta.DAL.Entites.Conversation;
using Rosetta.DAL.Entites.Core;
using Rosetta.DAL.Entites.Interaction;
using Rosetta.DAL.Entites.KnowledgeBase;
using Rosetta.DAL.Entites.Notification;
using Rosetta.DAL.Entites.Order;
using Rosetta.DAL.Entites.Pharmacy;
using Rosetta.DAL.Entites.Plan;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rosetta.DAL.Data.Contexts
{
    public class RoshettaDbContext : DbContext
    {
        public RoshettaDbContext(DbContextOptions<RoshettaDbContext> options) : base(options)
        {
        }
        // ===== Core =====
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        // ===== Catalog =====
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<MedicineIngredient> MedicineIngredients { get; set; }
        public DbSet<Product> Products { get; set; }

        // ===== Pharmacy =====
        public DbSet<PharmacyModel> Pharmacies { get; set; }
        public DbSet<PharmacyStaff> PharmacyStaff { get; set; }
        public DbSet<Inventory> Inventories { get; set; }

        // ===== Order =====
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<RxUpload> RxUploads { get; set; }

        // ===== Cart =====
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // ===== Plan =====
        public DbSet<MedicationPlan> MedicationPlans { get; set; }
        public DbSet<PlanItem> PlanItems { get; set; }
        public DbSet<MedicationSchedule> MedicationSchedules { get; set; }
        public DbSet<ScheduleWeekday> ScheduleWeekdays { get; set; }
        public DbSet<DoseEvent> DoseEvents { get; set; }
        public DbSet<ScheduleTime> ScheduleTimes { get; set; }
        public DbSet<RefillAlert> RefillAlerts { get; set; }

        // ===== Interaction =====
        public DbSet<InteractionRuleset> InteractionRulesets { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<InteractionCheck> InteractionChecks { get; set; }
        public DbSet<InteractionCheckItem> InteractionCheckItems { get; set; }
        public DbSet<CheckItemIngredient> CheckItemIngredients { get; set; }
        public DbSet<InteractionCheckResult> InteractionCheckResults { get; set; }

        // ===== Conversation =====
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageCitation> MessageCitations { get; set; }
        public DbSet<MessageFeedback> MessageFeedbacks { get; set; }
        public DbSet<SafetyFlag> SafetyFlags { get; set; }

        // ===== KnowledgeBase =====
        public DbSet<KbDocument> KbDocuments { get; set; }
        public DbSet<KbChunk> KbChunks { get; set; }

        // ===== Consent =====
        public DbSet<ConsentRecord> ConsentRecords { get; set; }

        // ===== Notification =====
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        #region DbSets

        #endregion
    }
}