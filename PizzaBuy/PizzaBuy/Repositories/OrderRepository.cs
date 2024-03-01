using Microsoft.EntityFrameworkCore;
using PizzaBuy.Data;
using PizzaBuy.Models.Domain;

namespace PizzaBuy.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PizzaBuyDbContext pizzaBuyDbContext;

        public OrderRepository(PizzaBuyDbContext pizzaBuyDbContext)
        {
            this.pizzaBuyDbContext = pizzaBuyDbContext;
        }

        public async Task<Order> AddAsync(Order order)
        {
            await pizzaBuyDbContext.Orders.AddAsync(order);
            await pizzaBuyDbContext.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllByUserIdAsync(string userId)
        {
            return await pizzaBuyDbContext.Orders
                .Include(o => o.CartItems)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order?> GetAsync(Guid orderId)
        {
            return await pizzaBuyDbContext.Orders.FindAsync(orderId);
        }

        public async Task RemoveAsync(Order order)
        {
            pizzaBuyDbContext.Orders.Remove(order);
            await pizzaBuyDbContext.SaveChangesAsync();
        }
    }
}
