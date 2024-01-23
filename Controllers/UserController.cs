using Microsoft.AspNetCore.Mvc;
using OOPShop.Services.Interfaces;
using OOPShop.Models;

namespace OOPShop.Controllers
{
    public class UserController : Controller
    {
        AuthUser authUser;
        IUserService userService;
        ILogger<UserController> logger;

        public UserController(AuthUser authUser, IUserService userService, ILogger<UserController> logger)
        {
            this.authUser = authUser;
            this.userService = userService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("/user/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/user/register")]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User? registeredUser = userService.SignUp(user);

            if (registeredUser == null)
            {
                TempData["AlertMessage"] = "User with the same name exists!";
            }
            else
            {
                TempData["AlertMessage"] = "You have successfully signed up!";
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("/user/login")]
        public IActionResult Login()
        {
            if (authUser.IsAuthenticated())
            {
                return RedirectPermanent("~/");
            }
            return View();
        }

        [HttpPost]
        [Route("/user/login")]
        public IActionResult Login(string name, string password)
        {
            if (name is null || password is null)
            {
                return BadRequest();
            }
           
            bool isLoggedIn = userService.LogIn(name, password);

            if (isLoggedIn)
            {
                return RedirectPermanent("~/");
            }

            TempData["AlertMessage"] = "Name or password is not valid";
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("/user/logout")]
        public IActionResult Logout()
        {
            if (authUser.IsAuthenticated())
            {
                TempData["AlertMessage"] = "You successfully logged out!";
                userService.LogOut();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [Route("user/addToBalance")]
        public IActionResult AddToBalance(double amount)
        {
            if (authUser.IsNotAuthenticated())
            {
                TempData["AlertMessage"] = "You must log in!";
                return RedirectToAction("Login");
            }

            if (amount > 0)
            {
                userService.AddToBalance(authUser.User, amount);
            }

            return RedirectPermanent("~/");
        }
    }
}
