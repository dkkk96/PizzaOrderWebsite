namespace PizzaBuy.Models.Domain
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        
        public int Quantity { get; set; }
        public bool IsOrdered { get; set; }


        // Navigation property to the associated product
        public Product Product { get; set; }
        public string UserId { get; set; }
    }
}
