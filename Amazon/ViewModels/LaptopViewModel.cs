using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Amazon.ViewModels
{
    public class LaptopViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  // Specify precision and scale here
		public string Images { get; set; }
		public string? Address { get; set; }

        // New properties for customer details
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public int PurchaseCount { get; set; }
    }
}
