namespace Amazon.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        // Foreign Key to Laptop
        public int LaptopId { get; set; }
        public Laptop Laptop { get; set; } // Navigation property to Laptop

        // Foreign Key to Customer
        
       

        public DateTime PurchaseDate { get; set; }
    }
}
