using Microsoft.AspNetCore.Mvc;
using PizzaBuy.Repositories;

namespace PizzaBuy.Controllers
{
    public class MenuController : Controller
    {
        private readonly IProductRepository productRepository;

        public MenuController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> VegMenu()
        {
            var productList = await productRepository.GetAllAsync().ConfigureAwait(false); ;
            return View(productList);
        }

        [HttpGet]
        public async Task<IActionResult> NonVegMenu() 
        {
            var productList = await productRepository.GetAllAsync().ConfigureAwait(false); ;
            return View(productList);
        
        }
    }
}


