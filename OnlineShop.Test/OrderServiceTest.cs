using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OnlineShop.Core.Contracts;
using OnlineShop.Core.Models;
using OnlineShop.Core.Services;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Data.Identity;
using OnlineShop.Infrastructure.Data.Repositories;
using System.Threading.Tasks;

namespace OnlineShop.Test
{
    public class OrderServiceTest
    {
        private ServiceProvider serviceProvider;
        private InMemoryDbContext dbContext;

        [SetUp]
        public async Task Setup()
        {
            dbContext = new InMemoryDbContext();
            var serviceCollection = new ServiceCollection();
            serviceProvider = serviceCollection
                .AddSingleton(sp => dbContext.CreateContext())
                .AddSingleton<IApplicatioDbRepository, ApplicatioDbRepository>()
                .AddSingleton<IOrderService, OrderService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            var user = new ApplicationUser()
            {
                Id = "123"
            };
            await repo.AddAsync(user);
            await repo.SaveChangesAsync();
            var order = new Order()
            {
                Id = 1,
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "TestPhoneNumber",
                Address = "TestAddress",
                AdditionalInformation = "TestAddInfo",
                Date = System.DateTime.Now,
                UserId = "123"
            };
            await repo.AddAsync<Order>(order);
            await repo.SaveChangesAsync();
        }

        [Test]
        public void DeleteOrderShouldNotThrow()
        {
            
            var service = serviceProvider.GetService<IOrderService>();
            Assert.DoesNotThrowAsync(async () => await service.DeleteOrder(1));
        }
        [Test]
        public void DeleteOrderShouldThrow()
        {
            
            var service = serviceProvider.GetService<IOrderService>();
            Assert.CatchAsync(async () => await service.DeleteOrder(2));
        }

        [Test]
        public void GetOrdersIsNotNull()
        {

            var service = serviceProvider.GetService<IOrderService>();
            Assert.NotNull(async () => await service.GetOrders());
        }

        [Test]
        public void ViewOrderNotNull()
        {

            var service = serviceProvider.GetService<IOrderService>();
            Assert.NotNull(async () => await service.ViewOrder(1));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}