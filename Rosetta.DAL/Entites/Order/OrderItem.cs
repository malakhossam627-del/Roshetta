using Rosetta.DAL.Entites.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Order
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int InventoryId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPriceSnapshot { get; set; }
        public string? ProductNameSnapshot { get; set; }
        public decimal LineTotal { get; set; }

        public virtual CustomerOrder Order { get; set; } = null!;
        public virtual Inventory Inventory { get; set; } = null!;
    }
}
