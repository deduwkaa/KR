using Microsoft.AspNetCore.Mvc;
using OOPShop.Services.Interfaces;
using OOPShop.Models;

namespace OOPShop.Controllers
{
    public class OrderController : Controller
    {
        ILogger<OrderController> logger;
        IOrderService orderService;
        IUserService userService;
        AuthUser authUser;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IUserService userService, AuthUser authUser)
        {
            this.logger = logger;
            this.orderService = orderService;
            this.userService = userService;
            this.authUser = authUser;
        }

        [HttpGet]
        [Route("order/info/orderId={orderId:int}")]
        public IActionResult Info(int orderId)
        {
            Order? order = orderService.GetById(orderId);

            if (userHasRights(order) is false)
            {
                return RedirectPermanent("~/");
            }

            List<OrderItem> items = orderService.GetAllItems(order);
            ViewBag.order = order;
            ViewBag.items = items;

            return View();
        }

        [HttpPost]
        [Route("/order/cancel/orderId={orderId:int}")]
        public IActionResult Cancel(int orderId)
        {
            Order? order = orderService.GetById(orderId);

            if (userHasRights(order) is false || order.Status != OrderStatus.Open)
            {
                return RedirectPermanent("~/");
            }

            orderService.Cancel(order);

            return RedirectPermanent(String.Format("~/order/info/orderId={0}", order.Id));
        }

        [HttpPost]
        [Route("/order/complete/orderId={orderId:int}")]
        public IActionResult Complete(int orderId)
        {
            Order? order = orderService.GetById(orderId);

            if (userHasRights(order) is false || order.Status != OrderStatus.Open)
            {
                return RedirectPermanent("~/");
            }

            orderService.Complete(order);
            userService.WithdrawFromBalance(authUser.User, order.TotalSum);

            return RedirectPermanent(String.Format("~/order/info/orderId={0}", order.Id));
        }

        private bool userHasRights(Order? order)
        {
            // if order doesn't exist or user is not authenticated or user doesn't have this order
            // then the user has no rights to see the page
            return order != null && authUser.IsAuthenticated() && order.UserId == authUser.User.Id;
        }
    }
}
