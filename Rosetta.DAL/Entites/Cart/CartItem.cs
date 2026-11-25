using Rosetta.DAL.Entites.Pharmacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rosetta.DAL.Entites.Cart
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int InventoryId { get; set; }
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual Inventory Inventory { get; set; } = null!;
    }
}
