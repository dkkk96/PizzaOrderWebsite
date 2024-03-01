using Microsoft.AspNetCore.Mvc;
using PizzaBuy.Models.Domain;
using PizzaBuy.Repositories;

namespace PizzaBuy.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public OrderController(IOrderRepository orderRepository, ICartItemRepository cartItemRepository)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
        }
        


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var orders = await _orderRepository.GetAllByUserIdAsync(userId);
            return View(orders);
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            

            var cartItemsList = await _cartItemRepository.GetAllAsync();
            List<CartItem> finalCartItems = new List<CartItem>();

            foreach (var item in cartItemsList)
            {
                if(userId == item.UserId && item.IsOrdered == false)
                {
                    finalCartItems.Add(item);
                }
            }


            if (finalCartItems == null || !finalCartItems.Any())
            {
                // Handle empty cart
                return RedirectToAction("Index", "Cart");
            }

            decimal totalAmount = 0;
            foreach (var cartItem in finalCartItems)
            {
                totalAmount += cartItem.Quantity * cartItem.ProductPrice;
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount
            };

            order = await _orderRepository.AddAsync(order);

            // Mark cart items as ordered
            foreach (var cartItem in finalCartItems)
            {
                cartItem.IsOrdered = true;
               // cartItem.OrderId = order.OrderId;
                await _cartItemRepository.UpdateAsync(cartItem);
            }

            // Clear the cart
            // This step depends on your implementation, you need to implement it in the Cart controller
            // For example, you can have a ClearCart action in the Cart controller
            // and call it here to clear the cart after placing the order

             return RedirectToAction("Index");
            //return RedirectToAction("OrderPlaced", "PaymentGateway");
        }

    }
}
