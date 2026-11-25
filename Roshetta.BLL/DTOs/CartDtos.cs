using System;
using System.Collections.Generic;
using System.Linq;

namespace Roshetta.BLL.DTOs
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? BrandNameAr { get; set; }
        public string? Strength { get; set; }
        public string? DosageForm { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int AvailableStock { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
        public string? PrescriptionType { get; set; } // OTC or Rx
        public bool IsRx => string.Equals(PrescriptionType, "Rx", StringComparison.OrdinalIgnoreCase);
    }

    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int PharmacyId { get; set; }
        public string PharmacyName { get; set; } = string.Empty;
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();

        public decimal Subtotal => Items.Sum(i => i.LineTotal);
        public decimal DeliveryFee => Subtotal >= 200m || Items.Count == 0 ? 0m : 25m;
        public decimal Discount { get; set; } = 0m;
        public decimal Total => Math.Max(0m, Subtotal + DeliveryFee - Discount);
        public int TotalItemsCount => Items.Sum(i => i.Quantity);
        public bool HasRxItems => Items.Any(i => i.IsRx);
        public bool IsEmpty => Items.Count == 0;
    }

    public class AddToCartRequest
    {
        public int InventoryId { get; set; }
        public int Quantity { get; set; } = 1;
    }

    public class UpdateQuantityRequest
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
