using Roshetta.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roshetta.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<(bool Success, string Message, int OrderId, string OrderNumber)> PlaceOrderAsync(int userId, CheckoutDto checkoutDto);
        Task<List<OrderSummaryDto>> GetUserOrdersAsync(int userId, string? statusFilter = null);
        Task<OrderDetailsDto?> GetOrderDetailsAsync(int orderId, int userId);
        Task<OrderTrackingDto?> GetOrderTrackingAsync(int orderId, int userId);
        Task<(bool Success, string Message)> ReorderAsync(int orderId, int userId);
    }
}
