using PizzaBuy.Models.Domain;

namespace PizzaBuy.Repositories
{
    public interface ICartItemRepository
    {
        
        public Task<IEnumerable<CartItem>> GetAllByUserIdAsync(string userId);
        



        //used
        public Task<CartItem> AddAsync(CartItem cartItem);


        //used
        public Task<CartItem?> UpdateAsync(CartItem cartItem);
        

        

        public Task<IEnumerable<CartItem>> GetAllAsync();


        //used
        public Task<CartItem?> GetAsync(Guid id);


        public Task<CartItem> GetByIdAsync(Guid id, string userID);

        public Task<CartItem> RemoveAsync(CartItem cartItem);
    }
}
