using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Contracts;

namespace OnlineShop.Areas.AdminArea.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageOrders()
        {
            var order = await orderService.GetOrders();

            return View("Areas/AdminArea/Views/Order/ManageOrders.cshtml", order);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await orderService.DeleteOrder(id);
            return Redirect("/admin/order/manageorders");
        }

        public async Task<IActionResult> View(int id)
        {
            var order = await orderService.ViewOrder(id);
            return View("Areas/AdminArea/Views/Order/View.cshtml", order);
        }
    }
}
