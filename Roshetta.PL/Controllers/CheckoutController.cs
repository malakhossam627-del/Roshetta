using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rosetta.DAL.Data.Contexts;
using Roshetta.BLL.DTOs;
using Roshetta.BLL.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Roshetta.PL.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUserService;
        private readonly RoshettaDbContext _context;

        public CheckoutController(
            ICartService cartService,
            IOrderService orderService,
            ICurrentUserService currentUserService,
            RoshettaDbContext context)
        {
            _cartService = cartService;
            _orderService = orderService;
            _currentUserService = currentUserService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = _currentUserService.GetCurrentUserId();
            var cart = await _cartService.GetCartAsync(userId);

            if (cart.IsEmpty)
            {
                TempData["ErrorMessage"] = "Your cart is empty. Please add medications before proceeding to checkout.";
                return RedirectToAction("Index", "Cart");
            }

            var user = await _context.AppUsers
                .Include(u => u.Addresses)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            var defaultAddress = user?.Addresses.FirstOrDefault(a => a.IsDefault)
                ?? user?.Addresses.FirstOrDefault();

            var checkoutModel = new CheckoutDto
            {
                FullName = user?.FullName ?? _currentUserService.GetCurrentUserName(),
                Phone = user?.Phone ?? "01012345678",
                Governorate = defaultAddress?.Governorate ?? "Cairo",
                City = defaultAddress?.City ?? "Zamalek",
                Street = defaultAddress?.Street ?? "26th of July St.",
                Building = defaultAddress?.Building ?? "Bldg 14, Apt 4B",
                PaymentMethod = "Cash on Delivery",
                Cart = cart
            };

            return View(checkoutModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutDto model)
        {
            var userId = _currentUserService.GetCurrentUserId();
            var cart = await _cartService.GetCartAsync(userId);
            model.Cart = cart;

            if (cart.IsEmpty)
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var result = await _orderService.PlaceOrderAsync(userId, model);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View("Index", model);
            }

            TempData["SuccessMessage"] = "Order placed successfully!";
            return RedirectToAction(nameof(Confirmation), new { orderId = result.OrderId, orderNumber = result.OrderNumber });
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(int orderId, string? orderNumber)
        {
            var userId = _currentUserService.GetCurrentUserId();
            var orderDetails = await _orderService.GetOrderDetailsAsync(orderId, userId);

            if (orderDetails == null)
            {
                return RedirectToAction("Index", "Orders");
            }

            var confirmationDto = new OrderConfirmationDto
            {
                OrderId = orderDetails.OrderId,
                OrderNumber = orderDetails.OrderNumber,
                OrderDate = orderDetails.OrderDate,
                TotalAmount = orderDetails.TotalAmount,
                CustomerName = orderDetails.CustomerName,
                Phone = orderDetails.CustomerPhone,
                DeliveryAddress = orderDetails.DeliveryAddress,
                PaymentMethod = orderDetails.PaymentMethod,
                EstimatedDelivery = orderDetails.OrderDate.AddHours(2).ToString("hh:mm tt, dd MMM yyyy"),
                RequiresRxReview = orderDetails.RequiresRxReview,
                Items = orderDetails.Items.Select(i => new CartItemDto
                {
                    ProductName = i.ProductName,
                    Strength = i.Strength,
                    DosageForm = i.DosageForm,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            return View(confirmationDto);
        }
    }
}
