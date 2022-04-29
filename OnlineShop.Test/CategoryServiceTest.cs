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
    public class CategoryServiceTest
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
                .AddSingleton<ICategoryService, CategoryService>()
                .BuildServiceProvider();

            var repo = serviceProvider.GetService<IApplicatioDbRepository>();
            var category = new Category()
            {
                Id = 1,
                Name = "TestCategory"
            };
            await repo.AddAsync(category);
            await repo.SaveChangesAsync();
        }

        [Test]
        public void DeleteCategoryShouldNotThrow()
        {

            var service = serviceProvider.GetService<ICategoryService>();
            Assert.DoesNotThrowAsync(async () => await service.DeleteCategory(1));
        }
        [Test]
        public void DeleteCategoryShouldThrow()
        {

            var service = serviceProvider.GetService<ICategoryService>();
            Assert.CatchAsync(async () => await service.DeleteCategory(2));
        }

        [Test]
        public void CreateCategoryShouldNotThrow()
        {
            var model = new CategoryListViewModel
            {
                Id = 2,
                Name = "TestCategoryCreate"
            };

            var service = serviceProvider.GetService<ICategoryService>();
            Assert.DoesNotThrowAsync(async () => await service.CreateCategory(model));
        }

        [Test]
        public void CreateCategoryShouldThrow()
        {
            var model = new CategoryListViewModel
            {
                Id = 2,
                Name = null
            };

            var service = serviceProvider.GetService<ICategoryService>();
            Assert.CatchAsync(async () => await service.CreateCategory(model));
        }

        [Test]
        public void GetCategoriesIsNotNull()
        {

            var service = serviceProvider.GetService<ICategoryService>();
            Assert.NotNull(async () => await service.GetCategories());
        }

        [Test]
        public void GetCategoryForEditIsValid()
        {

            var service = serviceProvider.GetService<ICategoryService>();
            Assert.IsTrue(service.GetCategoryForEdit(1).IsCompletedSuccessfully);
        }

        [Test]
        public void GetCategoryForEditIsNotValid()
        {

            var service = serviceProvider.GetService<ICategoryService>();
            Assert.IsFalse(service.GetCategoryForEdit(2).IsCompletedSuccessfully);
        }

        [Test]
        public void UpdateCategoryIsValid()
        {
            var model = new CategoryListViewModel
            {
                Id = 2,
                Name = "456"
            };
            var service = serviceProvider.GetService<ICategoryService>();
            Assert.IsTrue(service.UpdateCategory(model).IsCompletedSuccessfully);
        }

        [Test]
        public void UpdateCategoryIsNotValid()
        {
            var model = new CategoryListViewModel
            {
                Id = 3,
                Name = null
            };
            var service = serviceProvider.GetService<ICategoryService>();
            Assert.IsFalse(service.UpdateCategory(model).Result);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Dispose();
        }
    }
}