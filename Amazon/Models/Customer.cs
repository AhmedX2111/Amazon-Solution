namespace Amazon.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? ProductName { get; set; }
		public string? Address { get; set; }
		

		// Foreign Key
		public int LaptopId { get; set; } // Foreign key reference to Laptop
        public Laptop Laptop { get; set; }  // Navigation property

        // Navigation property to related products
        public ICollection<Laptop> PurchasedLaptops { get; set; }
    }
}
