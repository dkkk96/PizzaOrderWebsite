using Microsoft.EntityFrameworkCore;
using PizzaBuy.Models.Domain;

namespace PizzaBuy.Data
{
    public class PizzaBuyDbContext : DbContext
    {
        public PizzaBuyDbContext(DbContextOptions<PizzaBuyDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<SubscribeEmail> SubscribeEmails { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
    }
}
