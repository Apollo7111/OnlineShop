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
        public async Task<IActionResult> ManageItems()
        {
            var item = await itemService.GetItems();

            return View("Areas/AdminArea/Views/Item/ManageItems.cshtml", item);
        }

       /* [HttpPost]
        public async Task<IActionResult> Create(ItemCreateViewModel model)
        {
             *//*await itemService.CreateItem(model);*//*
            return View("/Areas/AdminArea/Views/Item/Create.cshtml",model);
        }*/

        public async Task<IActionResult> Delete(int id)
        {
            await itemService.DeleteItem(id);
            return Redirect("/admin/item/manageitems");
        }

        public async Task<IActionResult> Create()
        {
            return View("/Areas/AdminArea/Views/Item/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("/Areas/AdminArea/Views/Item/Create.cshtml", model);
            }
            await itemService.CreateItem(model);

            return View("/Areas/AdminArea/Views/Item/Create.cshtml",model);

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
                return View("/Areas/AdminArea/Views/Item/Edit.cshtml",model);
            }

            if (await itemService.UpdateItem(model))
            {
                ViewBag.Message =string.Format("Successfully edited item with Id: {0}", model.Id);
            }
            else
            {
                ViewBag.Message = string.Format("There was an error trying to eddit item with Id: {0}", model.Id);
            }

            return View("/Areas/AdminArea/Views/Item/Edit.cshtml",model);
        }
    }
}
