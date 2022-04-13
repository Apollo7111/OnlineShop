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

        public UserController(RoleManager<IdentityRole> _roleManager, IUserService _userService)
        {
            roleManager = _roleManager;
            userService = _userService;
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

      /*  public async Task<IActionResult> Add(int id)
        {
            await userService.Add(id);
            return Ok(id);
        }*/
    }
}
