using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Areas.AdminArea.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
           return View("Areas/AdminArea/Views/Home/Index.cshtml");
        }
    }
}
