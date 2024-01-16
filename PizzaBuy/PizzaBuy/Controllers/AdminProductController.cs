using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaBuy.Models.Domain;
using PizzaBuy.Models.ViewModel;
using PizzaBuy.Repositories;

namespace PizzaBuy.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly IProductRepository productRepository;

        public AdminProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        
        public async Task<IActionResult> Add(AddProductRequest addProductRequest)
        {
            var product = new Product
            {
                ProductName = addProductRequest.ProductName,
                ProductDescription = addProductRequest.ProductDescription,
                ProductPrice = addProductRequest.ProductPrice,
                ProductImage = addProductRequest.ProductImage,
                ProductAvailable = addProductRequest.ProductAvailable,
                Type = addProductRequest.Type,
            };
            await productRepository.AddAsync(product);

            return RedirectToAction("Add");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var productList = await productRepository.GetAllAsync();
            return View(productList);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //get the data
            var product = await productRepository.GetAsync(id);

            if(product !=null)
            {
                //map the domain model to view model
                var model = new EditProductRequest
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    ProductPrice = product.ProductPrice,
                    ProductImage = product.ProductImage,
                    ProductAvailable = product.ProductAvailable,
                    Type = product.Type,
                };

                //pass data to view
                return View(model);

            }
            else {
                return View("Edit");

            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductRequest editProductRequest)
        {
            //map view model to domain model

            var updatedProduct = new Product
            {
                Id = editProductRequest.Id,
                ProductName = editProductRequest.ProductName,
                ProductDescription = editProductRequest.ProductDescription,
                ProductPrice = editProductRequest.ProductPrice,
                ProductImage = editProductRequest.ProductImage,
                ProductAvailable = editProductRequest.ProductAvailable,
                Type = editProductRequest.Type,

            };

            var updatedProduct1 = await productRepository.UpdateAsync(updatedProduct);

            if(updatedProduct1 != null)
            {
                //show success
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(EditProductRequest editProductRequest)
        {
            var deletedProduct = await productRepository.DeleteAsync(editProductRequest.Id);

            if(deletedProduct != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            else
            {
                //show error notification
                return RedirectToAction("Edit", new { id = editProductRequest.Id });
            }

        }
    }
}
