using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Contracts;
using OnlineShop.Core.Models;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Data.Identity;
using OnlineShop.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Services
{
    public class ItemService : IItemService
    {
        private readonly IApplicatioDbRepository repo;
        public ItemService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> CreateItem(ItemCreateViewModel model)
        {
            bool result = false;
            decimal price = 0;
            var item = new Item()
            {
               /* Name = model.Name,
                Price = price,
                Description = "20 incha",
                CategoryId = 1*/
            };
            item.Name = model.Name;
            item.CategoryId = model.CategoryId;
            item.Price = model.Price;
            item.Description = model.Description;
            try
            {
               await repo.AddAsync(item);
                repo.SaveChanges();
                result = true;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<Item> DeleteItem(int id)
        {
            await repo.DeleteAsync<Item>(id);
            repo.SaveChanges();
            return null;
        }

        public async Task<ItemEditViewModel> GetItemForEdit(int id)
        {
            var item = await repo.GetByIdAsync<Item>(id);

            return new ItemEditViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                CategoryId = item.CategoryId,
                Price = item.Price,
                Description = item.Description,
            };
        }
        public async Task<IEnumerable<ItemListViewModel>> GetItems()
        {
            return await repo.All<Item>()
                .Select(x => new ItemListViewModel()
                {
                    Id = x.Id,
                    Category = x.Category,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateItem(ItemEditViewModel model)
        {
            bool result = false;
            var item = await repo.GetByIdAsync<Item>(model.Id);

            if (item != null)
            {
                item.Name = model.Name;
                item.CategoryId = model.CategoryId;
                item.Price = model.Price;
                item.Description = model.Description;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
    }
