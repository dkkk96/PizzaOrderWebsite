namespace PizzaBuy.Models.ViewModel
{
    public class AddProductRequest
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }

        public string Type { get; set; }

        public bool ProductAvailable { get; set; }
    }
}
