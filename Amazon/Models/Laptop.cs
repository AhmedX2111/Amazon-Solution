using System.ComponentModel.DataAnnotations.Schema;

namespace Amazon.Models
{
	public class Laptop
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int PurchaseCount { get; set; }

        public string Images { get; set; }

        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }

        // Navigation properties
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();  // This should be correct for a one-to-many relationship



    }
    
    
}
