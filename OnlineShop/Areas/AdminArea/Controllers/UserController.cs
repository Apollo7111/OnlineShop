using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Contracts;

namespace OnlineShop.Areas.AdminArea.Controllers
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

        public async Task<IActionResult> ManageUsers()
        {
            var users = await userService.GetUsers();

            return View("Areas/AdminArea/Views/User/ManageUsers.cshtml", users);
        }

    }
}
