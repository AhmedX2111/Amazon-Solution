using Amazon.Models;

namespace Amazon.ViewModels
{
    public class LaptopDetailsViewModel
    {
        /*public Laptop Laptop { get; set; }*/
        public LaptopViewModel Laptop { get; set; }
        public List<CustomerViewModels> Customers { get; set; }
    }
}
