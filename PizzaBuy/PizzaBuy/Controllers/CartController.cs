using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaBuy.Models.Domain;
using PizzaBuy.Models.ViewModel;
using PizzaBuy.Repositories;

namespace PizzaBuy.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICartItemRepository cartItemRepository;

        public CartController(IProductRepository productRepository , ICartItemRepository cartItemRepository)
        {
            this.productRepository = productRepository;
            this.cartItemRepository = cartItemRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "User")]
        [HttpPost]

        public async Task<IActionResult> AddToCart(string productID, string UserId, string productName, decimal productPrice)
        {
            if (!Guid.TryParse(productID, out Guid productIdGuid))
            {
                // Handle invalid productId
                return BadRequest("Invalid product ID format");
            }

            var existingCartItem = await cartItemRepository.GetByIdAsync(productIdGuid, UserId);




            var product = await productRepository.GetAsync(productIdGuid);

            if (existingCartItem != null)
            {
                // If the cart item already exists, update its quantity
                existingCartItem.Quantity++;
                await cartItemRepository.UpdateAsync(existingCartItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = productID,
                    ProductName = productName,
                    ProductPrice = productPrice,
                    Quantity = 1,
                    UserId = UserId,
                    Product = product,
                    IsOrdered = false,

                };
                await cartItemRepository.AddAsync(cartItem);

            }



            

           
            TempData["notifyMessage"] = "Product Added To Cart";
            return RedirectToAction("VegMenu", "Menu");

            
        }


        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> CartItems()
        {

            var cartItemsList = await cartItemRepository.GetAllAsync();
            return View(cartItemsList);
        }


        [HttpPost]
        public async Task<IActionResult> IncrementCartItem(Guid cartItemId)
        {
            var cartItem = await cartItemRepository.GetAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
                await cartItemRepository.UpdateAsync(cartItem);
            }
            return RedirectToAction("CartItems");
        }

        [HttpPost]
        public async Task<IActionResult> DecrementCartItem(Guid cartItemId)
        {
            var cartItem = await cartItemRepository.GetAsync(cartItemId);
            if (cartItem != null && cartItem.Quantity > 0)
            {
                cartItem.Quantity--;
                await cartItemRepository.UpdateAsync(cartItem);
            }
            return RedirectToAction("CartItems");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCartItem(Guid cartItemId)
        {
            var cartItem = await cartItemRepository.GetAsync(cartItemId);
            if (cartItem != null)
            {
                await cartItemRepository.RemoveAsync(cartItem);
            }
            return RedirectToAction("CartItems");
        }
    }
}
