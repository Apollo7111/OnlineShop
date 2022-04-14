using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Contracts;

namespace OnlineShop.Controllers
{
    public class ItemController : BaseController
    {
        private readonly IItemService itemService;

        public ItemController(IItemService _itemService)
        {
            itemService = _itemService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add(int id)
        {
            await itemService.AddToCart(id);

            return Redirect("/");
        }
        public async Task<IActionResult> Remove(int id)
        {
            await itemService.RemoveItemFromCart(id);

            return Redirect("/user/cart");
        }

    }
}
