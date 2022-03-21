using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

    }
}
