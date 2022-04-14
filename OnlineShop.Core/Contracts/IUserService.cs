using OnlineShop.Core.Models;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<IEnumerable<UserListViewModel>> GetUserCart();
        Task<bool> CreateOrder(OrderListViewModel model);
    }
}
