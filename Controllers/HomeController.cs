using Microsoft.AspNetCore.Mvc;
using OOPShop.Services.Interfaces;
using OOPShop.Models;
using OOPShop.Data;

namespace OOPShop.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService orderService;
        private ILogger<HomeController> logger;
        private AuthUser authUser;

        // DI
        public HomeController(IOrderService orderService, AuthUser authUser, ILogger<HomeController> logger)
        {
            this.orderService = orderService;
            this.authUser = authUser;
            this.logger = logger;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            if (authUser.IsAuthenticated())
            {
                ViewBag.orders = orderService.GetAllOrders(authUser.User);
                ViewBag.user = authUser.User;
                return View();
            }
            return RedirectPermanent("~/user/login");
        }
    }
}
