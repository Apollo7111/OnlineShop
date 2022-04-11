using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Contracts;
using OnlineShop.Core.Models;

namespace OnlineShop.Areas.AdminArea.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }
        public IActionResult Index()
        {
            return Redirect("/admin/category/managecategories");
        }
        public async Task<IActionResult> ManageCategories()
        {
            var category = await categoryService.GetCategories();
            
            return View("Areas/AdminArea/Views/Category/ManageCategories.cshtml", category);
        }

        /* [HttpPost]
         public async Task<IActionResult> Create(ItemCreateViewModel model)
         {
              *//*await itemService.CreateItem(model);*//*
             return View("/Areas/AdminArea/Views/Item/Create.cshtml",model);
         }*/

        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteCategory(id);
            return Redirect("/admin/category/managecategories");
        }

        public async Task<IActionResult> Create()
        {
            return View("/Areas/AdminArea/Views/Category/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("/Areas/AdminArea/Views/Category/Create.cshtml", model);
            }
            await categoryService.CreateCategory(model);
            return View("/Areas/AdminArea/Views/Category/Create.cshtml", model);

        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await categoryService.GetCategoryForEdit(id);
            return View("/Areas/AdminArea/Views/Category/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryListViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("/Areas/AdminArea/Views/Category/Edit.cshtml", model);
            }

            if (await categoryService.UpdateCategory(model))
            {
                ViewBag.Message = string.Format("Successfully edited category with Id: {0}", model.Id);
            }
            else
            {
                ViewBag.Message = string.Format("There was an error trying to eddit category with Id: {0}", model.Id);
            }
            return View("/Areas/AdminArea/Views/Category/Edit.cshtml", model);
        }
    }
}
