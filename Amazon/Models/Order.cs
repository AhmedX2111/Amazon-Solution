namespace Amazon.Models
{
    public class Order
    {
        public int Id { get; set; }

        /*public int LaptopId { get; set; } */ // Foreign key to Laptop
        public Laptop Laptop { get; set; }  // Navigation property

        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
    }
}
