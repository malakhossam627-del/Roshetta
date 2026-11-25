using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Rosetta.DAL.Entites.Catalog
{
    public class Product
    {
        public int ProductId { get; set; }
        public int MedicineId { get; set; }
        public string? BrandNameEn { get; set; }
        public string? BrandNameAr { get; set; }
        public string? Strength { get; set; }
        public string? DosageForm { get; set; }
        public string? PackSize { get; set; }
        public string? SKU { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual Medicine Medicine { get; set; } = null!;
        public virtual ICollection<Pharmacy.Inventory> Inventories { get; set; } = new HashSet<Pharmacy.Inventory>();
        public virtual ICollection<Plan.PlanItem> PlanItems { get; set; } = new HashSet<Plan.PlanItem>();
    }
}
