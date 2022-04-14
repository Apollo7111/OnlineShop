using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class UserService : IUserService
    {
        private readonly IApplicatioDbRepository repo;
        private readonly UserManager<ApplicationUser> userManager;
        string userId = null;

        public UserService(IApplicatioDbRepository _repo, UserManager<ApplicationUser> _userManager, IHttpContextAccessor httpContextAccessor)
        {
            repo = _repo;
            userManager = _userManager;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(x => new UserListViewModel()
                {
                    Id = x.Id,
                    Email = x.Email,
                    Cart = x.Cart
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserListViewModel>> GetUserCart()
        {
             return await repo.All<ApplicationUser>()
                 .Where(x => x.Id == userId)
                 .Select(x => new UserListViewModel()
                 {
                     Id = x.Id,
                     Email = x.Email,
                     Cart = x.Cart
                 })
                 .ToListAsync();
        }

        public async Task<bool> CreateOrder(OrderListViewModel model)
        {
            var currUser = await repo.GetByIdAsync<ApplicationUser>(userId);
            bool result = false;
            var order = new Order()
            {
            Id = model.Id,
            FirstName = currUser.Email,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            AdditionalInformation = model.AdditionalInformation,
            Date = DateTime.Now,
            UserId = userId,
            Cart = currUser.Cart
        };
            /*order.Id = model.Id;
            order.FirstName = currUser.Email;
            order.LastName = model.LastName;
            order.PhoneNumber = model.PhoneNumber;
            order.Address = model.Address;
            order.AdditionalInformation = model.AdditionalInformation;
            order.Date = DateTime.Now;
            order.UserId = userId;
            currUser.Cart.DefaultIfEmpty();*/
            try
            {
                await repo.AddAsync(order);
                repo.SaveChanges();
                result = true;
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
