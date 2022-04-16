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
                 .Include(x => x.Cart)
                 .Select(x => new UserListViewModel()
                 {
                     Id = x.Id,
                     Email = x.Email,
                     Cart = x.Cart
                 })
                 .ToListAsync();
        }

        public async Task<bool> CreateOrder(OrderCreateViewModel model)
        {
            var currUser = repo.All<ApplicationUser>()
               .Where(u => u.Id == userId)
               .Include(u => u.Cart)
               .FirstOrDefault();
            bool result = false;
            if(currUser.Cart == null)
            {
                return result;
            }
            var order = new Order()
            {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            AdditionalInformation = model.AdditionalInformation,
            Date = DateTime.Now,
            UserId = userId,
            Cart = currUser.Cart
        };
            try
            {
                await repo.AddAsync(order);
                await repo.SaveChangesAsync();
                currUser.Cart = null;
                currUser.Cart = new List<Item>();
                await repo.SaveChangesAsync();
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
