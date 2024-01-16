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
    }
}
