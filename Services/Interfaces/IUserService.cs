using OOPShop.Models;

namespace OOPShop.Services.Interfaces
{
    public interface IUserService
    {
        User? SignUp(User user);
        bool LogIn(string name, string password);
        void LogOut();
        void Add(User user);
        bool Delete(int id);
        List<User> GetAll();
        User? GetById(int id);
        bool AddToBalance(User user, double amount);
        bool WithdrawFromBalance(User user, double amount);
    }
}
