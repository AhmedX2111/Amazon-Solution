using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amazon.ViewModels
{
    public class LaptopFormViewModel
    {


        public int Id { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  // Specify precision and scale here

		public string Images { get; set; }
		public string? Address { get; set; }
		public byte[] FileContent { get; set; }  // To store the uploaded file name
		public string? FilePath { get; set; }
        [Display(Name = "Upload Image")]
        public IFormFile? FormFile { get; set; } // For uploading files
    }
}
