using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rosetta.DAL.Data.Contexts;
using Roshetta.BLL.Services.Interfaces;
using Roshetta.PL.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Roshetta.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoshettaDbContext _context;
        private readonly ICartService _cartService;
        private readonly ICurrentUserService _currentUserService;

        public HomeController(
            RoshettaDbContext context,
            ICartService cartService,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _cartService = cartService;
            _currentUserService = currentUserService;
        }

        // Dashboard View (Wireframe - 3)
        public async Task<IActionResult> Index()
        {
            var userId = _currentUserService.GetCurrentUserId();
            var orders = await _context.CustomerOrders
                .Where(o => o.CustomerUserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .Take(5)
                .ToListAsync();

            var medicines = await _context.Inventories
                .Include(i => i.Product)
                    .ThenInclude(p => p.Medicine)
                .Where(i => i.Product.IsActive)
                .Take(4)
                .ToListAsync();

            ViewBag.TotalOrdersCount = await _context.CustomerOrders.CountAsync(o => o.CustomerUserId == userId);
            ViewBag.ActiveOrdersCount = await _context.CustomerOrders.CountAsync(o => o.CustomerUserId == userId && o.Status != "Delivered" && o.Status != "Cancelled");
            ViewBag.MedicinesCount = await _context.Inventories.CountAsync();
            ViewBag.UserName = _currentUserService.GetCurrentUserName();

            return View(medicines);
        }

        // Pharmacy Catalog View (Wireframe - 4)
        public async Task<IActionResult> Pharmacy(string? category = "All")
        {
            var query = _context.Inventories
                .Include(i => i.Product)
                    .ThenInclude(p => p.Medicine)
                .Where(i => i.Product.IsActive);

            if (!string.IsNullOrEmpty(category) && category != "All")
            {
                if (category == "Pain Relief")
                    query = query.Where(i => i.Product.BrandNameEn!.Contains("Panadol") || i.Product.Medicine.NameEn.Contains("Panadol"));
                else if (category == "Antibiotics")
                    query = query.Where(i => i.Product.BrandNameEn!.Contains("Augmentin") || i.Product.Medicine.PrescriptionType == "Rx");
                else if (category == "Heart Health")
                    query = query.Where(i => i.Product.BrandNameEn!.Contains("Concor"));
                else if (category == "Digestive")
                    query = query.Where(i => i.Product.BrandNameEn!.Contains("Antinal"));
                else if (category == "Supplements")
                    query = query.Where(i => i.Product.BrandNameEn!.Contains("Limitless") || i.Product.BrandNameEn!.Contains("Retard"));
            }

            var items = await query.ToListAsync();
            ViewBag.ActiveCategory = category ?? "All";
            return View(items);
        }

        // My Medication Plan View (Wireframe - 5)
        public IActionResult Medications()
        {
            ViewBag.UserName = _currentUserService.GetCurrentUserName();
            return View();
        }

        // Landing Page View (Wireframe - 1)
        public async Task<IActionResult> Landing()
        {
            var medicines = await _context.Inventories
                .Include(i => i.Product)
                    .ThenInclude(p => p.Medicine)
                .Where(i => i.Product.IsActive)
                .Take(6)
                .ToListAsync();

            return View(medicines);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
