using Microsoft.AspNetCore.Mvc;
using OOPShop.Models;
using OOPShop.Services.Interfaces;

namespace OOPShop.Controllers
{
    public class ProductController : Controller
    {
        ILogger<ProductController> logger;
        IProductService productService;
        IOrderService orderService;
        AuthUser authUser;

        public ProductController(ILogger<ProductController> logger, IProductService productService, 
                                 IOrderService orderService, AuthUser authUser)
        {
            this.logger = logger;
            this.productService = productService;
            this.orderService = orderService;
            this.authUser = authUser;
        }

        [HttpGet]
        [Route("/product/all")]
        public IActionResult GetAllProducts()
        {
            ViewBag.products = productService.GetAll();
            return View();
        }

        [HttpGet]
        [Route("/product/get/info/{productId:int}")]
        public IActionResult GetProductById(int productId)
        {
            Product? product = productService.GetById(productId);

            if (userHasRights(product) is false)
            {
                return RedirectPermanent("~/");
            }

            ViewBag.product = product;
            return View();
        }

        [HttpPost]
        [Route("/product/one/order/")]
        public IActionResult OrderProduct(int productId, int quantity)
        {
            Product? product = productService.GetById(productId);

            if (userHasRights(product) is false || quantity < 1)
            {
                return RedirectPermanent("~/");
            }

            // ORDER PROCCESS
            bool isOrdered = orderService.Order(product, quantity);
            if (isOrdered is false)
            {
                TempData["AlertMessage"] = "You do not have enough money to buy it!";
            }
            else
            {
                TempData["AlertMessage"] = "You have successfully added the product to the cart!";
            }

            return RedirectPermanent(String.Format("/product/get/info/{0}", productId));
        }

        private bool userHasRights(Product? product)
        {
            return product != null && authUser.IsAuthenticated();
        }
        
    }
}
