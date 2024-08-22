using Amazon.Data;
using Amazon.ViewModels;
using Amazon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
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

        public IActionResult Details(int id)
        {
            var laptop = _context.Laptops
                .Include(l => l.Customers) // Adjust according to your data model
                .SingleOrDefault(l => l.Id == id);

            if (laptop == null)
            {
                return NotFound();
            }

            // Initialize Customers if null
            var customers = laptop.Customers ?? new List<Customer>();

            var viewModel = new LaptopDetailsViewModel
            {
                Laptop = new LaptopViewModel
                {
                    Id = laptop.Id,
                    Name = laptop.Name,
                    Description = laptop.Description,
                    Price = laptop.Price,
                    PurchaseCount = laptop.PurchaseCount
                },
                Customers = (laptop.Customers ?? new List<Customer>())
                .Select(c => new CustomerViewModels
                {
                    Name = c.Name,
                    Address = c.Address,
                    NumberOfProductsPurchased = c.PurchasedLaptops?.Count ?? 0 // Handle null for PurchasedLaptops
                })
                .ToList()
            };

            return View(viewModel);
        }


    }
}
