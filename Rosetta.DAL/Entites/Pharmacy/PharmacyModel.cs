using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Pharmacy
{
    public class PharmacyModel
    {
        public int PharmacyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? AddressText { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Order.CustomerOrder> Orders { get; set; } = new HashSet<Order.CustomerOrder>();
        public virtual ICollection<Cart.Cart> Carts { get; set; } = new HashSet<Cart.Cart>();
        public virtual ICollection<PharmacyStaff> Staff { get; set; } = new HashSet<PharmacyStaff>();
        public virtual ICollection<Inventory> Inventories { get; set; } = new HashSet<Inventory>();
    }
}
