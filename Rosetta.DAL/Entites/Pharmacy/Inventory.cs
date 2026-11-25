using Rosetta.DAL.Entites.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Pharmacy
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int PharmacyId { get; set; }
        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual PharmacyModel Pharmacy { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual ICollection<Order.OrderItem> OrderItems { get; set; } = new HashSet<Order.OrderItem>();
        public virtual ICollection<Cart.CartItem> CartItems { get; set; } = new HashSet<Cart.CartItem>();
    }
}
