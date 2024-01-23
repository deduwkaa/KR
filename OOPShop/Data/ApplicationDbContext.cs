using Microsoft.EntityFrameworkCore;
using OOPShop.Data;

namespace OOPShop.Models
{
    public class ApplicationDbContext : AbstractApplicationDbContext
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=oopded;password=password;database=oopdb;",
                new MySqlServerVersion(new Version(8, 0, 28)));
        }
    }
}
