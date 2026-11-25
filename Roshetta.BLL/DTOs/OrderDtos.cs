using System;
using System.Collections.Generic;

namespace Roshetta.BLL.DTOs
{
    public class OrderSummaryDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Placed";
        public decimal TotalAmount { get; set; }
        public int ItemsCount { get; set; }
        public string FirstItemName { get; set; } = string.Empty;
        public string PharmacyName { get; set; } = string.Empty;
        public bool RequiresRxReview { get; set; }
        public string? RxReviewStatus { get; set; }
    }

    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int InventoryId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Strength { get; set; }
        public string? DosageForm { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Placed";
        public decimal TotalAmount { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DeliveryFee { get; set; }
        public string PaymentMethod { get; set; } = "Cash on Delivery";
        public string PaymentStatus { get; set; } = "Pending";
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string PharmacyName { get; set; } = string.Empty;
        public string? PharmacyPhone { get; set; }
        public bool RequiresRxReview { get; set; }
        public string? RxReviewStatus { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public List<TrackingStepDto> Timeline { get; set; } = new List<TrackingStepDto>();
    }

    public class OrderTrackingDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string CurrentStatus { get; set; } = "Placed";
        public DateTime OrderDate { get; set; }
        public string EstimatedDelivery { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;
        public string PharmacyName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public int ItemsCount { get; set; }
        public List<TrackingStepDto> Steps { get; set; } = new List<TrackingStepDto>();
    }

    public class TrackingStepDto
    {
        public string StepKey { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? Timestamp { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCurrent { get; set; }
        public string IconClass { get; set; } = "bi-check-circle";
    }
}
