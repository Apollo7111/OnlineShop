using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Contracts;
using OnlineShop.Models;
using System.Diagnostics;

namespace OnlineShop.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;

        private readonly IItemService itemService;

        public HomeController(ILogger<HomeController> _logger, IItemService _itemService)
        {
            logger = _logger;
            itemService = _itemService;
        }

        /*public  IActionResult Index()
        {
            return Redirect("home/home");
        }*/
        public async Task<IActionResult> Index()
        {
            var item = await itemService.GetItems();
            return View(item);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       /* public async Task<IActionResult> Home()
        {
            var item = await itemService.GetItems();
            return View(item);
        }*/

    }
}