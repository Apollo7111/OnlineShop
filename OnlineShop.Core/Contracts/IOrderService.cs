using OnlineShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderListViewModel>> GetOrders();
    }
}
