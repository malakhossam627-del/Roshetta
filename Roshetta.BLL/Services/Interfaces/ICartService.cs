using Roshetta.BLL.DTOs;
using System.Threading.Tasks;

namespace Roshetta.BLL.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartAsync(int userId);
        Task<(bool Success, string Message)> AddToCartAsync(int userId, int inventoryId, int quantity = 1);
        Task<(bool Success, string Message)> UpdateQuantityAsync(int userId, int cartItemId, int quantity);
        Task<(bool Success, string Message)> RemoveFromCartAsync(int userId, int cartItemId);
        Task<(bool Success, string Message)> ClearCartAsync(int userId);
        Task<int> GetCartItemCountAsync(int userId);
    }
}
