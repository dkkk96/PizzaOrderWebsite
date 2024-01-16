using Microsoft.EntityFrameworkCore;
using PizzaBuy.Data;
using PizzaBuy.Models.Domain;

namespace PizzaBuy.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly PizzaBuyDbContext pizzaBuyDbContext;

        public ProductRepository(PizzaBuyDbContext pizzaBuyDbContext)
        {
            this.pizzaBuyDbContext = pizzaBuyDbContext;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await pizzaBuyDbContext.AddAsync(product);
            await pizzaBuyDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var existingProduct = await pizzaBuyDbContext.Products.FindAsync(id);

            if (existingProduct != null)
            {
                pizzaBuyDbContext.Products.Remove(existingProduct);
                await pizzaBuyDbContext.SaveChangesAsync();
                return existingProduct;
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await pizzaBuyDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetAsync(Guid id)
        {
            return await pizzaBuyDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
                  
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var existingProduct = await pizzaBuyDbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

            if(existingProduct !=null)
            {
                existingProduct.Id = product.Id;
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductDescription = product.ProductDescription;
                existingProduct.ProductPrice = product.ProductPrice;
                existingProduct.ProductImage = product.ProductImage;
                existingProduct.ProductAvailable = product.ProductAvailable;
                existingProduct.Type = product.Type;


                await pizzaBuyDbContext.SaveChangesAsync();
                return existingProduct;
            }
            return null;
        }
    }
}
