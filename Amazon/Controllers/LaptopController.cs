using Amazon.Data;
using Amazon.Models;
using Amazon.Services;
using Amazon.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Controllers
{
    public class LaptopController : Controller
    {
        private readonly LaptopService _laptopService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly ApplicationDbContext _context;

		public LaptopController(LaptopService laptopService, IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _laptopService = laptopService;
		    _webHostEnvironment = webHostEnvironment;
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> Create(LaptopFormViewModel model)
		{
			if (ModelState.IsValid)
			{
				var laptop = new Laptop
				{
					Name = model.Name,
					Description = model.Description,
					Price = model.Price
					
				};

				if (model.FormFile != null)
				{
					// Get the path to the "wwwroot" folder
					string wwwRootPath = _webHostEnvironment.WebRootPath;

					// Create a unique filename for the file and combine it with the wwwroot path
					string fileName = Path.GetFileNameWithoutExtension(model.FormFile.FileName);
					string extension = Path.GetExtension(model.FormFile.FileName);
					fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;  // Updated fileName with timestamp
					string path = Path.Combine(wwwRootPath + "/images/", fileName);

					// Store the image path in the FilePath property
					laptop.FilePath = "/images/" + fileName;

					// Copy the uploaded file to the server
					using (var fileStream = new FileStream(path, FileMode.Create))
					{
						await model.FormFile.CopyToAsync(fileStream);
					}
				}

				// Save the laptop entity to the database
				_context.Laptops.Add(laptop);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			return View(model);
		}
	}
}
