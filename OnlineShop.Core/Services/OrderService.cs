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
    public class OrderService : IOrderService
    {
        private readonly IApplicatioDbRepository repo;
        public OrderService(IApplicatioDbRepository _repo)
        {
            repo = _repo;
        }
        public async Task<IEnumerable<OrderListViewModel>> GetOrders()
        {
            return await repo.All<Order>()
                .Select(x => new OrderListViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    AdditionalInformation = x.AdditionalInformation,
                })
                .ToListAsync();
        }
    }
}
