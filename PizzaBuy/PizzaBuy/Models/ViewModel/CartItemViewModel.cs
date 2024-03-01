namespace PizzaBuy.Models.ViewModel
{
    public class CartItemViewModel
    {
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
