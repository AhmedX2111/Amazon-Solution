using Amazon.Data;
using Amazon.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var laptops = await _context.Laptops
                .Select(l => new LaptopViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description,
                    Price = l.Price,
                    Images = l.Images,
                    PurchaseCount = _context.Customers.Count(c => c.ProductName == l.Name)
                })
                .ToListAsync(); // Ensure this is awaited correctly

            return View(laptops); // Ensure the model passed is of type IEnumerable<LaptopViewModel>
        }
    }
}
