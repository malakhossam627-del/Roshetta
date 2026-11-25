using Microsoft.AspNetCore.Mvc;
using Roshetta.BLL.Services.Interfaces;
using System.Threading.Tasks;

namespace Roshetta.PL.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUserService;

        public OrdersController(IOrderService orderService, ICurrentUserService currentUserService)
        {
            _orderService = orderService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? status = "All")
        {
            var userId = _currentUserService.GetCurrentUserId();
            var orders = await _orderService.GetUserOrdersAsync(userId, status);
            ViewBag.ActiveFilter = status ?? "All";
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = _currentUserService.GetCurrentUserId();
            var order = await _orderService.GetOrderDetailsAsync(id, userId);

            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found or you do not have permission to view it.";
                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Track(int id)
        {
            var userId = _currentUserService.GetCurrentUserId();
            var trackingInfo = await _orderService.GetOrderTrackingAsync(id, userId);

            if (trackingInfo == null)
            {
                TempData["ErrorMessage"] = "Order tracking information not found.";
                return RedirectToAction(nameof(Index));
            }

            return View(trackingInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reorder(int id)
        {
            var userId = _currentUserService.GetCurrentUserId();
            var result = await _orderService.ReorderAsync(id, userId);

            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index", "Cart");
            }

            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
