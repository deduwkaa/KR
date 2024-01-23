using OOPShop.Models;

namespace OOPShop.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User? GetByName(string name);
    }
}
