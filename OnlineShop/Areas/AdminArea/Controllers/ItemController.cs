using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Contracts;
using OnlineShop.Core.Models;

namespace OnlineShop.Areas.AdminArea.Controllers
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
        public async Task<IActionResult> ManageItems(string SearchString)
        {
            var item = await itemService.GetItems();
            if (!String.IsNullOrEmpty(SearchString))
            {
                item = item.Where(s => s.Name.Contains(SearchString)).ToList();
            }

            return View("Areas/AdminArea/Views/Item/ManageItems.cshtml", item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await itemService.DeleteItem(id);
            return Redirect("/admin/item/manageitems");
        }

        public async Task<IActionResult> Create()
        {
           var category = await itemService.GetCategories();
            return View("/Areas/AdminArea/Views/Item/Create.cshtml", category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/admin/item/manageitems");
            }
            await itemService.CreateItem(model);

            return Redirect("/admin/item/manageitems");

        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await itemService.GetItemForEdit(id);

            return View("/Areas/AdminArea/Views/Item/Edit.cshtml",model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/admin/item/manageitems");
            }
            await itemService.UpdateItem(model);
            return Redirect("/admin/item/manageitems");
        }
    }
}
