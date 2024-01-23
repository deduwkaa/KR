using Microsoft.EntityFrameworkCore;
using OOPShop.Models;

namespace OOPShop.Data
{
    public abstract class AbstractApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
