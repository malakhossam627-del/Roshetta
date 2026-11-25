using Microsoft.EntityFrameworkCore;
using Rosetta.DAL.Data.Contexts;
using Rosetta.DAL.Entites.Order;
using Roshetta.BLL.DTOs;
using Roshetta.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roshetta.BLL.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly RoshettaDbContext _context;
        private readonly ICartService _cartService;

        public OrderService(RoshettaDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<(bool Success, string Message, int OrderId, string OrderNumber)> PlaceOrderAsync(int userId, CheckoutDto checkoutDto)
        {
            var cart = await _cartService.GetCartAsync(userId);
            if (cart.IsEmpty)
            {
                return (false, "Your cart is empty. Please add medicines before checkout.", 0, string.Empty);
            }

            // Verify stock for all items
            foreach (var item in cart.Items)
            {
                var inv = await _context.Inventories.FindAsync(item.InventoryId);
                if (inv == null || inv.StockQuantity < item.Quantity)
                {
                    return (false, $"Insufficient stock for {item.ProductName}. Please adjust your cart.", 0, string.Empty);
                }
            }

            // Generate unique OrderNumber: RSH-YYYYMMDD-XXXX
            var orderNumber = $"RSH-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";
            while (await _context.CustomerOrders.AnyAsync(o => o.OrderNumber == orderNumber))
            {
                orderNumber = $"RSH-{DateTime.UtcNow:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";
            }

            bool hasRx = cart.HasRxItems;
            string initialStatus = hasRx ? "Pharmacist Review" : "Placed";
            string? rxStatus = hasRx ? "PendingReview" : null;

            var order = new CustomerOrder
            {
                OrderNumber = orderNumber,
                CustomerUserId = userId,
                PharmacyId = cart.PharmacyId,
                Status = initialStatus,
                RxReviewStatus = rxStatus,
                TotalAmount = cart.Total,
                CreatedAt = DateTime.UtcNow
            };

            _context.CustomerOrders.Add(order);
            await _context.SaveChangesAsync();

            // Create Order Items and deduct inventory stock
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    InventoryId = item.InventoryId,
                    Quantity = item.Quantity,
                    UnitPriceSnapshot = item.UnitPrice,
                    ProductNameSnapshot = item.ProductName + (!string.IsNullOrEmpty(item.Strength) ? $" ({item.Strength})" : ""),
                    LineTotal = item.LineTotal
                };
                _context.OrderItems.Add(orderItem);

                // Deduct stock safely
                var inv = await _context.Inventories.FindAsync(item.InventoryId);
                if (inv != null)
                {
                    inv.StockQuantity -= item.Quantity;
                    inv.UpdatedAt = DateTime.UtcNow;
                }
            }

            // Save Address for user if provided
            var fullAddress = $"{checkoutDto.Building}, {checkoutDto.Street}, {checkoutDto.City}, {checkoutDto.Governorate}";
            var userAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.UserId == userId);
            if (userAddress == null)
            {
                _context.Addresses.Add(new Rosetta.DAL.Entites.Core.Address
                {
                    UserId = userId,
                    Governorate = checkoutDto.Governorate,
                    City = checkoutDto.City,
                    Street = checkoutDto.Street,
                    Building = checkoutDto.Building,
                    IsDefault = true
                });
            }

            // Create Payment record
            var payment = new Payment
            {
                OrderId = order.OrderId,
                Method = checkoutDto.PaymentMethod,
                Amount = order.TotalAmount,
                Status = checkoutDto.PaymentMethod == "Cash on Delivery" ? "Pending" : "Completed",
                PaidAt = checkoutDto.PaymentMethod == "Cash on Delivery" ? null : DateTime.UtcNow
            };
            _context.Payments.Add(payment);

            // Record initial OrderStatusHistory
            var statusHistory = new OrderStatusHistory
            {
                OrderId = order.OrderId,
                ChangedByUserId = userId,
                Status = initialStatus,
                Notes = hasRx
                    ? $"Order placed with prescription medicines. Awaiting pharmacist prescription verification. Delivery to: {fullAddress}"
                    : $"Order placed with Cash on Delivery. Delivery to: {fullAddress}",
                ChangedAt = DateTime.UtcNow
            };
            _context.OrderStatusHistories.Add(statusHistory);

            await _context.SaveChangesAsync();

            // Clear the active cart
            await _cartService.ClearCartAsync(userId);

            return (true, "Order placed successfully!", order.OrderId, orderNumber);
        }

        public async Task<List<OrderSummaryDto>> GetUserOrdersAsync(int userId, string? statusFilter = null)
        {
            var query = _context.CustomerOrders
                .Include(o => o.Pharmacy)
                .Include(o => o.OrderItems)
                .Where(o => o.CustomerUserId == userId);

            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All")
            {
                if (statusFilter == "Active")
                {
                    query = query.Where(o => o.Status != "Delivered" && o.Status != "Cancelled" && o.Status != "Rejected");
                }
                else if (statusFilter == "Delivered")
                {
                    query = query.Where(o => o.Status == "Delivered");
                }
                else if (statusFilter == "Cancelled")
                {
                    query = query.Where(o => o.Status == "Cancelled" || o.Status == "Rejected");
                }
            }

            var orders = await query
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new OrderSummaryDto
                {
                    OrderId = o.OrderId,
                    OrderNumber = o.OrderNumber,
                    OrderDate = o.CreatedAt,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    ItemsCount = o.OrderItems.Sum(oi => oi.Quantity),
                    FirstItemName = o.OrderItems.Select(oi => oi.ProductNameSnapshot).FirstOrDefault() ?? "Medication",
                    PharmacyName = o.Pharmacy.Name,
                    RequiresRxReview = !string.IsNullOrEmpty(o.RxReviewStatus),
                    RxReviewStatus = o.RxReviewStatus
                })
                .ToListAsync();

            return orders;
        }

        public async Task<OrderDetailsDto?> GetOrderDetailsAsync(int orderId, int userId)
        {
            var order = await _context.CustomerOrders
                .Include(o => o.Pharmacy)
                .Include(o => o.CustomerUser)
                    .ThenInclude(u => u.Addresses)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Inventory)
                        .ThenInclude(i => i.Product)
                            .ThenInclude(p => p.Medicine)
                .Include(o => o.StatusHistory)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.OrderId == orderId && o.CustomerUserId == userId);

            if (order == null)
                return null;

            var userAddress = order.CustomerUser.Addresses.FirstOrDefault(a => a.IsDefault)
                ?? order.CustomerUser.Addresses.FirstOrDefault();

            var addressText = userAddress != null
                ? $"{userAddress.Building}, {userAddress.Street}, {userAddress.City}, {userAddress.Governorate}"
                : "Standard Delivery Address";

            var payment = order.Payments.FirstOrDefault();
            var subtotal = order.OrderItems.Sum(i => i.LineTotal);
            var deliveryFee = order.TotalAmount - subtotal;
            if (deliveryFee < 0) deliveryFee = 0;

            var dto = new OrderDetailsDto
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                OrderDate = order.CreatedAt,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Subtotal = subtotal,
                DeliveryFee = deliveryFee,
                PaymentMethod = payment?.Method ?? "Cash on Delivery",
                PaymentStatus = payment?.Status ?? "Pending",
                CustomerName = order.CustomerUser.FullName,
                CustomerPhone = order.CustomerUser.Phone ?? "N/A",
                DeliveryAddress = addressText,
                PharmacyName = order.Pharmacy.Name,
                PharmacyPhone = order.Pharmacy.Phone,
                RequiresRxReview = !string.IsNullOrEmpty(order.RxReviewStatus),
                RxReviewStatus = order.RxReviewStatus,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    OrderItemId = oi.OrderItemId,
                    InventoryId = oi.InventoryId,
                    ProductName = oi.ProductNameSnapshot ?? oi.Inventory.Product.BrandNameEn ?? "Medicine",
                    Strength = oi.Inventory.Product.Strength,
                    DosageForm = oi.Inventory.Product.DosageForm,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPriceSnapshot,
                    LineTotal = oi.LineTotal
                }).ToList(),
                Timeline = BuildTimeline(order)
            };

            return dto;
        }

        public async Task<OrderTrackingDto?> GetOrderTrackingAsync(int orderId, int userId)
        {
            var order = await _context.CustomerOrders
                .Include(o => o.Pharmacy)
                .Include(o => o.CustomerUser)
                    .ThenInclude(u => u.Addresses)
                .Include(o => o.OrderItems)
                .Include(o => o.StatusHistory)
                .FirstOrDefaultAsync(o => o.OrderId == orderId && o.CustomerUserId == userId);

            if (order == null)
                return null;

            var userAddress = order.CustomerUser.Addresses.FirstOrDefault(a => a.IsDefault)
                ?? order.CustomerUser.Addresses.FirstOrDefault();

            var addressText = userAddress != null
                ? $"{userAddress.Building}, {userAddress.Street}, {userAddress.City}, {userAddress.Governorate}"
                : "Standard Delivery Address";

            var estDelivery = order.CreatedAt.AddHours(2).ToString("hh:mm tt, dd MMM yyyy");

            return new OrderTrackingDto
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                CurrentStatus = order.Status,
                OrderDate = order.CreatedAt,
                EstimatedDelivery = estDelivery,
                DeliveryAddress = addressText,
                PharmacyName = order.Pharmacy.Name,
                TotalAmount = order.TotalAmount,
                ItemsCount = order.OrderItems.Sum(oi => oi.Quantity),
                Steps = BuildTimeline(order)
            };
        }

        public async Task<(bool Success, string Message)> ReorderAsync(int orderId, int userId)
        {
            var order = await _context.CustomerOrders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId && o.CustomerUserId == userId);

            if (order == null)
                return (false, "Order not found.");

            int addedCount = 0;
            foreach (var item in order.OrderItems)
            {
                var result = await _cartService.AddToCartAsync(userId, item.InventoryId, item.Quantity);
                if (result.Success)
                {
                    addedCount++;
                }
            }

            if (addedCount == 0)
            {
                return (false, "Could not reorder items. Products may currently be out of stock.");
            }

            return (true, $"Added {addedCount} product(s) to your cart from order {order.OrderNumber}.");
        }

        private List<TrackingStepDto> BuildTimeline(CustomerOrder order)
        {
            // Defined Lifecycle per Project Proposal:
            // Placed -> Pharmacist review for Rx (if applicable) -> Confirmed -> Packed -> Out for delivery -> Delivered
            bool isRx = !string.IsNullOrEmpty(order.RxReviewStatus);

            var steps = new List<TrackingStepDto>();

            // Step 1: Placed
            steps.Add(new TrackingStepDto
            {
                StepKey = "Placed",
                Title = "Order Placed",
                Description = "Your order was successfully received by Roshetta.",
                IconClass = "bi-bag-check-fill"
            });

            // Step 2: Pharmacist Review (if Rx) or Order Confirmation
            if (isRx)
            {
                steps.Add(new TrackingStepDto
                {
                    StepKey = "Pharmacist Review",
                    Title = "Pharmacist Rx Review",
                    Description = "Licensed pharmacist is verifying prescription requirements.",
                    IconClass = "bi-clipboard2-pulse-fill"
                });
            }

            // Step 3: Confirmed
            steps.Add(new TrackingStepDto
            {
                StepKey = "Confirmed",
                Title = "Order Confirmed",
                Description = "Pharmacy accepted and verified stock.",
                IconClass = "bi-patch-check-fill"
            });

            // Step 4: Packed
            steps.Add(new TrackingStepDto
            {
                StepKey = "Packed",
                Title = "Packed & Prepared",
                Description = "Medicines safely packaged in temperature-controlled bag.",
                IconClass = "bi-box-seam-fill"
            });

            // Step 5: Out for delivery
            steps.Add(new TrackingStepDto
            {
                StepKey = "Out for delivery",
                Title = "Out for Delivery",
                Description = "Courier is on the way to your delivery address.",
                IconClass = "bi-truck"
            });

            // Step 6: Delivered
            steps.Add(new TrackingStepDto
            {
                StepKey = "Delivered",
                Title = "Delivered",
                Description = "Order delivered to customer successfully.",
                IconClass = "bi-house-check-fill"
            });

            // Map status history timestamps
            var historyDict = order.StatusHistory
                .GroupBy(h => h.Status, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(h => h.ChangedAt).First().ChangedAt, StringComparer.OrdinalIgnoreCase);

            string currentStatus = order.Status;
            int currentIndex = steps.FindIndex(s => string.Equals(s.StepKey, currentStatus, StringComparison.OrdinalIgnoreCase));
            if (currentIndex == -1)
            {
                if (currentStatus.Contains("Review", StringComparison.OrdinalIgnoreCase))
                    currentIndex = steps.FindIndex(s => s.StepKey == "Pharmacist Review");
                else if (currentStatus.Contains("Deliver", StringComparison.OrdinalIgnoreCase))
                    currentIndex = steps.FindIndex(s => s.StepKey == "Delivered");
                else
                    currentIndex = 0;
            }

            for (int i = 0; i < steps.Count; i++)
            {
                var step = steps[i];
                if (historyDict.TryGetValue(step.StepKey, out var time))
                {
                    step.Timestamp = time;
                }
                else if (i == 0)
                {
                    step.Timestamp = order.CreatedAt;
                }

                if (i < currentIndex)
                {
                    step.IsCompleted = true;
                    step.IsCurrent = false;
                }
                else if (i == currentIndex)
                {
                    step.IsCompleted = true;
                    step.IsCurrent = true;
                }
                else
                {
                    step.IsCompleted = false;
                    step.IsCurrent = false;
                }
            }

            return steps;
        }
    }
}
