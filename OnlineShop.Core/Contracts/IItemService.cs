using OnlineShop.Core.Models;
using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Contracts
{
    public interface IItemService
    {
        Task<IEnumerable<ItemListViewModel>> GetItems();
        Task<bool> CreateItem(ItemCreateViewModel model);
        Task<Item> DeleteItem(int id);
        Task<ItemEditViewModel> GetItemForEdit(int id);
        Task<bool> UpdateItem(ItemEditViewModel model);
        Task<bool> AddToCart(int id);
        Task<bool> RemoveItemFromCart(int id);
    }
}
