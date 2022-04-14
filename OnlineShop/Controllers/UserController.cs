using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Constants;
using OnlineShop.Core.Contracts;
using OnlineShop.Core.Models;

namespace OnlineShop.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService userService;
        private readonly IOrderService orderService;

        public UserController(RoleManager<IdentityRole> _roleManager, IUserService _userService, IOrderService _orderService)
        {
            roleManager = _roleManager;
            userService = _userService;
            orderService = _orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Cart()
        {
            var cart = await userService.GetUserCart();
            return View("Views/User/Cart.cshtml",cart);
        }
         public async Task<IActionResult> Order()
        {
            return View("Views/User/Order.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //return View("/Views/User/Order.cshtml", model);
                return Ok(model);
            }
            await userService.CreateOrder(model);

            return View("/Views/User/Order.cshtml", model);
        }


        /*  public async Task<IActionResult> Add(int id)
          {
              await userService.Add(id);
              return Ok(id);
          }*/
    }
}
