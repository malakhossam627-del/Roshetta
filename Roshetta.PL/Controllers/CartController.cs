using Microsoft.AspNetCore.Mvc;
using Roshetta.BLL.DTOs;
using Roshetta.BLL.Services.Interfaces;
using System.Threading.Tasks;

namespace Roshetta.PL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICurrentUserService _currentUserService;

        public CartController(ICartService cartService, ICurrentUserService currentUserService)
        {
            _cartService = cartService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _currentUserService.GetCurrentUserId();
            var cart = await _cartService.GetCartAsync(userId);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int inventoryId, int quantity = 1)
        {
            var userId = _currentUserService.GetCurrentUserId();
            var result = await _cartService.AddToCartAsync(userId, inventoryId, quantity);

            if (IsAjaxRequest())
            {
                var totalCount = await _cartService.GetCartItemCountAsync(userId);
                var cart = await _cartService.GetCartAsync(userId);
                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    cartCount = totalCount,
                    subtotal = cart.Subtotal.ToString("N2"),
                    total = cart.Total.ToString("N2")
                });
            }

            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var userId = _currentUserService.GetCurrentUserId();
            var result = await _cartService.UpdateQuantityAsync(userId, cartItemId, quantity);

            if (IsAjaxRequest())
            {
                var cart = await _cartService.GetCartAsync(userId);
                var item = cart.Items.Find(i => i.CartItemId == cartItemId);
                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    cartCount = cart.TotalItemsCount,
                    itemLineTotal = item?.LineTotal.ToString("N2") ?? "0.00",
                    subtotal = cart.Subtotal.ToString("N2"),
                    deliveryFee = cart.DeliveryFee.ToString("N2"),
                    total = cart.Total.ToString("N2")
                });
            }

            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            var userId = _currentUserService.GetCurrentUserId();
            var result = await _cartService.RemoveFromCartAsync(userId, cartItemId);

            if (IsAjaxRequest())
            {
                var cart = await _cartService.GetCartAsync(userId);
                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    cartCount = cart.TotalItemsCount,
                    subtotal = cart.Subtotal.ToString("N2"),
                    deliveryFee = cart.DeliveryFee.ToString("N2"),
                    total = cart.Total.ToString("N2"),
                    isEmpty = cart.IsEmpty
                });
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Clear()
        {
            var userId = _currentUserService.GetCurrentUserId();
            var result = await _cartService.ClearCartAsync(userId);

            if (IsAjaxRequest())
            {
                return Json(new { success = result.Success, message = result.Message, cartCount = 0 });
            }

            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Count()
        {
            var userId = _currentUserService.GetCurrentUserId();
            var count = await _cartService.GetCartItemCountAsync(userId);
            return Json(new { count });
        }

        private bool IsAjaxRequest()
        {
            return Request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
                   Request.Headers["Accept"].ToString().Contains("application/json");
        }
    }
}
