using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rosetta.DAL.Data.Contexts;
using Roshetta.PL.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Roshetta.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly RoshettaDbContext _context;

        public HomeController(RoshettaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var inventories = await _context.Inventories
                .Include(i => i.Product)
                    .ThenInclude(p => p.Medicine)
                .Where(i => i.Product.IsActive)
                .ToListAsync();

            return View(inventories);
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
