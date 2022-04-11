using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Contracts;
using OnlineShop.Core.Models;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicatioDbRepository repo;

        public CategoryService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task<bool> CreateCategory(CategoryListViewModel model)
        {
            bool result = false;
            decimal price = 0;
            var category = new Category()
            {
                /* Name = model.Name,
                 Price = price,
                 Description = "20 incha",
                 CategoryId = 1*/
            };
            category.Name = model.Name;
            try
            {
                await repo.AddAsync(category);
                repo.SaveChanges();
                result = true;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<Category> DeleteCategory(int id)
        {
            await repo.DeleteAsync<Category>(id);
            repo.SaveChanges();
            return null;
        }

        public async Task<IEnumerable<CategoryListViewModel>> GetCategories()
        {
            return await repo.All<Category>()
                .Select(x => new CategoryListViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();
        }

        public async Task<CategoryListViewModel> GetCategoryForEdit(int id)
        {
            var item = await repo.GetByIdAsync<Category>(id);

            return new CategoryListViewModel()
            {
                Id = item.Id,
                Name = item.Name,
            };
        }

        public async Task<bool> UpdateCategory(CategoryListViewModel model)
        {
            bool result = false;
            var item = await repo.GetByIdAsync<Category>(model.Id);

            if (item != null)
            {
                item.Name = model.Name;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
