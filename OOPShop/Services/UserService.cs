using OOPShop.Models;
using OOPShop.Repositories.Interfaces;
using OOPShop.Services.Interfaces;

namespace OOPShop.Services
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        AuthUser authUser;

        public UserService(IUserRepository userRepository, AuthUser authUser)
        {
            this.userRepository = userRepository;
            this.authUser = authUser;
        }

        public void Add(User user)
        {
            userRepository.Add(user);
        }

        public bool Delete(int id)
        {
            return userRepository.Delete(id);
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User? GetById(int id)
        {
            return userRepository.GetById(id);
        }

        public bool LogIn(string name, string password)
        {
            bool isValidPassword = false;
            var user = userRepository.GetByName(name);

            if (user != null)
            {
                 isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
            }
            if (isValidPassword)
            {
                authUser.User = user;
                return true;
            }
            return false;
        }

        public void LogOut()
        {
            authUser.User = null;
        }

        public User? SignUp(User user)
        {
            // if user with the same name exists - return null
            if (user is null || userRepository.GetByName(user.Name) != null)
            {
                return null;
            }
            else
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                userRepository.Add(user);
            }
            return user;
        }

        public bool AddToBalance(User user, double amount)
        {
            if (user is null)
                return false;

            user.Balance += amount;
            userRepository.Save();
            return true;
        }
        public bool WithdrawFromBalance(User user, double amount)
        {
            return AddToBalance(user, -amount);
        }
    }
}
