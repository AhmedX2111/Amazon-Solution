using Amazon.Data;
using Amazon.Models;
using Amazon.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Amazon.Services
{
    public class LaptopService
    {
        
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IWebHostEnvironment _env;

        public LaptopService(IServiceScopeFactory scopeFactory, IWebHostEnvironment env)
        {
            
            _scopeFactory = scopeFactory;
            _env = env;
        }
        public async Task ImportLaptopsFromJsonAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var filePath = Path.Combine(_env.WebRootPath, "images", "laptops.json");

                if (!System.IO.File.Exists(filePath))
                {
                    throw new FileNotFoundException("The JSON file was not found.", filePath);
                }

                var json = System.IO.File.ReadAllText(filePath);
                var laptops = JsonConvert.DeserializeObject<List<LaptopDto>>(json);

                foreach (var laptopDto in laptops)
                {
                    var laptop = new Laptop
                    {
                        Id = laptopDto.Id,
                        Name = laptopDto.Name,
                        Description = laptopDto.Description,
                        Price = laptopDto.Price,
                      /*  Images = laptopDto.Images // Convert list of images to JSON string*/
                    };

                    context.Laptops.Add(laptop);
                }

                await context.SaveChangesAsync();
            }
        }
    }

    public class LaptopDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}
    

