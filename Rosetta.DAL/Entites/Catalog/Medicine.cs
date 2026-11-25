using Microsoft.VisualBasic;
using Rosetta.DAL.Entites.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Rosetta.DAL.Entites.Catalog
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public string? IntendedUse { get; set; }
        public string? SideEffects { get; set; }
        public string? Warnings { get; set; }
        public string? PrescriptionType { get; set; }
        public string? ContentStatus { get; set; }
        public string? ContentVersion { get; set; }
        public int? ReviewedByUserId { get; set; }
        public DateTime? ReviewedAt { get; set; }

        public virtual AppUser? ReviewedByUser { get; set; }
        public virtual ICollection<MedicineIngredient> MedicineIngredients { get; set; } = new HashSet<MedicineIngredient>();
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public virtual ICollection<Plan.PlanItem> PlanItems { get; set; } = new HashSet<Plan.PlanItem>();
        public virtual ICollection<Interaction.InteractionCheckItem> InteractionCheckItems { get; set; } = new HashSet<Interaction.InteractionCheckItem>();
    }
}
