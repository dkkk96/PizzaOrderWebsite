namespace PizzaBuy.Models.Domain
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation property for CartItems related to this order
        public ICollection<CartItem> CartItems { get; set; }
    }
}
