using PizzaBuy.Models.Domain;

namespace PizzaBuy.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task<IEnumerable<Order>> GetAllByUserIdAsync(string userId);
        Task<Order?> GetAsync(Guid orderId);
        Task RemoveAsync(Order order);
    }
}
