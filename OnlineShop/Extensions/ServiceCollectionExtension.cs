using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Contracts;
using OnlineShop.Core.Services;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Data.Repositories;

namespace OnlineShop.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicatioDbRepository, ApplicatioDbRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
        public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
            return services;
        }
    }
}
