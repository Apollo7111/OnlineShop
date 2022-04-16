using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Contracts;
using OnlineShop.Core.Models;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Data.Identity;
using OnlineShop.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Services
{
    public class ItemService : IItemService
    {
        private readonly IApplicatioDbRepository repo;
        string userId = null;
        public ItemService(IApplicatioDbRepository _repo, IHttpContextAccessor httpContextAccessor)
        {
            repo = _repo;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<ItemCreateViewModel> GetCategories()
        {
            var categories = repo.All<Category>()
                .ToList();
            return new ItemCreateViewModel()
            {
                Categories = categories
            };

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
            item.ImageUrl = model.ImageUrl;
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
            var categories = repo.All<Category>()
                .ToList();
            categories.ToList();
            return new ItemEditViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                ImageUrl = item.ImageUrl,
                CategoryId = item.CategoryId,
                Price = item.Price,
                Description = item.Description,
                Categories = categories
            };
        }
        public async Task<IEnumerable<ItemListViewModel>> GetItems()
        {
            return await repo.All<Item>()
                .Select(x => new ItemListViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Category = x.Category,
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
                item.ImageUrl = model.ImageUrl;
                item.CategoryId = model.CategoryId;
                item.Price = model.Price;
                item.Description = model.Description;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
        public async Task<bool> AddToCart(int id)
        {
            var result = false;
            var user = repo.All<ApplicationUser>()
               .Where(u => u.Id == userId)
             //  .Include(u => u.Cart)
               .FirstOrDefault();
            var item = repo.All<Item>()
               .Where(u => u.Id == id)
               .FirstOrDefault();
            if (item != null && user != null)
            {
                user.Cart.Add(item);
             //   repo.Update(user);
                 await repo.SaveChangesAsync();
                result=true;
            }
            return result;
        }
        
        public async Task<bool> RemoveItemFromCart(int id)
        {
            var result = false;
            /* var item = await repo.GetByIdAsync<Item>(id);
             var user = await repo.GetByIdAsync<ApplicationUser>(userId);*/
            var user = repo.All<ApplicationUser>()
                .Where(u => u.Id == userId)
            //    .Include(u => u.Cart)
                .FirstOrDefault();
            var item = repo.All<Item>()
               .Where(u => u.Id == id)
               .FirstOrDefault();
            if (item != null && user != null)
            {
                user.Cart.Remove(item);
            //    repo.Update(user);
                 await repo.SaveChangesAsync();
            //    user.Cart.Count();
        //        user.Email.ToString();
                result = true;
            }
            return result;
        }

    }
    }
