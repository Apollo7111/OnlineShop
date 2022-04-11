using OnlineShop.Core.Models;
using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListViewModel>> GetCategories();
        Task<bool> CreateCategory(CategoryListViewModel model);
        Task<Category> DeleteCategory(int id);
        Task<CategoryListViewModel> GetCategoryForEdit(int id);
        Task<bool> UpdateCategory(CategoryListViewModel model);
    }
}
