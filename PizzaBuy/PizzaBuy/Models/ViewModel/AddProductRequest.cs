using System.ComponentModel.DataAnnotations;

namespace PizzaBuy.Models.ViewModel
{
    public class AddProductRequest
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]

        public decimal ProductPrice { get; set; }
        [Required]
        public string ProductImage { get; set; }
        [Required]

        public string Type { get; set; }

        public bool ProductAvailable { get; set; }
    }
}
