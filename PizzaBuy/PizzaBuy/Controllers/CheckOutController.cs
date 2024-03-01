using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaBuy.Models.Domain;
using PizzaBuy.Repositories;
using Stripe.Checkout;

namespace PizzaBuy.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderRepository _orderRepository;

        public CheckOutController(ICartItemRepository cartItemRepository, IOrderRepository orderRepository)
        {
            _cartItemRepository = cartItemRepository;
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var cartItemsList = await _cartItemRepository.GetAllAsync();
            List<CartItem> finalCartItems = cartItemsList
                .Where(item => userId == item.UserId && !item.IsOrdered)
                .ToList();

            var domain = "https://localhost:7161/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "CheckOut/OrderConfirmation",
                CancelUrl = domain + "Account/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };

            foreach (var item in finalCartItems)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.ProductPrice * 100),
                        Currency = "inr",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.ProductName.ToString(),
                        }
                    },
                    Quantity = item.Quantity,
                };
                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public async Task<IActionResult> OrderConfirmation()
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(TempData["Session"].ToString());

            if (session.PaymentStatus == "paid")
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                var cartItemsList = await _cartItemRepository.GetAllAsync();
                List<CartItem> finalCartItems = cartItemsList
                    .Where(item => userId == item.UserId && !item.IsOrdered)
                    .ToList();

                if (!finalCartItems.Any())
                {
                    // Handle empty cart
                    return RedirectToAction("Index", "Cart");
                }

                decimal totalAmount = finalCartItems.Sum(item => item.Quantity * item.ProductPrice);

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
                    await _cartItemRepository.UpdateAsync(cartItem);
                }

                

                return RedirectToAction("Index", "Order"); 
            }

            return RedirectToAction("Login", "Account"); 
        }
    }
}
