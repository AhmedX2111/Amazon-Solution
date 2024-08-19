using Amazon.Models;
using Amazon.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Amazon.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly LaptopService _laptopService;

        public HomeController(ILogger<HomeController> logger, LaptopService laptopService)
		{
			_logger = logger;
            _laptopService = laptopService;
        }

		public IActionResult Index()
		{
            var laptops = _laptopService.ImportLaptopsFromJsonAsync();
            return View(laptops);
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
