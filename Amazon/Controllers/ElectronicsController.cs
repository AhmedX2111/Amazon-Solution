using Amazon.Data; // Adjust this to your actual namespace
using Amazon.Models; // Adjust this to your actual namespace
using Amazon.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Linq;
using System.Net;


namespace Amazon.Controllers
{
    /*[Authorize(Roles = "Admin")]*/
    public class ElectronicsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElectronicsController(ApplicationDbContext context)
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
                .ToListAsync();
            return View(laptops);
        }

        public IActionResult Details(int id)
        {
            var laptop = _context.Laptops
                .Where(l => l.Id == id)
                .Select(l => new LaptopViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description,
                    Price = l.Price,
                    Images= l.Images
                   
                })
                .FirstOrDefault();

            if (laptop == null)
            {
                return NotFound(); // Return a 404 view if the laptop is not found
            }

            return View(laptop);
        }

        public IActionResult Buy(int id)
        {
            var laptop = _context.Laptops
                .Where(l => l.Id == id)
                .Select(l => new LaptopViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description,
                    Price = l.Price,
                    
                })
                .FirstOrDefault();

            if (laptop == null)
            {
                return NotFound(); // Return a 404 view if the laptop is not found
            }
            return View(laptop);
        }

        [HttpPost]
        [Authorize]
        public IActionResult ConfirmPurchase(int id, string name, string customerAddress)
        {
            // Retrieve the laptop by ID
            var laptop = _context.Laptops
                .Where(l => l.Id == id)
                .Select(l => new LaptopViewModel
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description,
                    Price = l.Price,
                    
                })
                .FirstOrDefault();

            if (laptop == null)
            {
                return NotFound(); // Return a 404 view if the laptop is not found
            }

            // Check if the user has already purchased this laptop
            var existingPurchase = _context.Customers
                .FirstOrDefault(c => c.ProductName == laptop.Name && c.Name == name);

            

            // Save the new purchase
            var customer = new Customer
            {
                Name = name,
                Address = customerAddress,
                ProductName = laptop.Name,
                LaptopId = laptop.Id
                
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Return a success message
            ViewBag.Message = $"Order placed successfully for {laptop.Name}.";
            return View("OrderSuccess"); // Redirect to a success view
        }





    }
}
