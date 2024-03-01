using Microsoft.EntityFrameworkCore;
using PizzaBuy.Data;
using PizzaBuy.Models.Domain;

namespace PizzaBuy.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly PizzaBuyDbContext pizzaBuyDbContext;
        public CartItemRepository(PizzaBuyDbContext pizzaBuyDbContext)
        {
            this.pizzaBuyDbContext = pizzaBuyDbContext;
        }

        public async Task<CartItem> AddAsync(CartItem cartItem)
        {
            await pizzaBuyDbContext.AddAsync(cartItem);
            await pizzaBuyDbContext.SaveChangesAsync();
            return cartItem;
        }

        public Task<IEnumerable<CartItem>> GetAllByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }







        public async Task<CartItem?> GetAsync(Guid id)
        {
            return await pizzaBuyDbContext.CartItems.FirstOrDefaultAsync(x => x.CartItemId == id);

        }

        public async Task<CartItem> RemoveAsync(CartItem cartItem)
        {
            pizzaBuyDbContext.CartItems.Remove(cartItem);
            await pizzaBuyDbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem?> UpdateAsync(CartItem cartItem)
        {
            pizzaBuyDbContext.Entry(cartItem).State = EntityState.Modified; // Mark entity as modified
            await pizzaBuyDbContext.SaveChangesAsync(); // Save changes
            return cartItem;
        }

       



        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await pizzaBuyDbContext.CartItems.ToListAsync();
        }

        public async Task<CartItem> GetByIdAsync(Guid id, string userID)
        {
            return await pizzaBuyDbContext.CartItems.FirstOrDefaultAsync(x => x.CartItemId == id && x.UserId == userID);
        }


       
    }
}
