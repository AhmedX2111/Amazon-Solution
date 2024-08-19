using System.ComponentModel.DataAnnotations.Schema;

namespace Amazon.Models
{
	public class Laptop
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }  // Specify precision and scale here
		public string Images { get; set; } 

		public byte[] FileContent { get; set; }  // To store the uploaded file name
        public string? FilePath { get; set; }  // To store the uploaded file path

        [NotMapped]
        public IFormFile? FormFile { get; set; }  // File to be uploaded, not mapped to DB



    }
}
