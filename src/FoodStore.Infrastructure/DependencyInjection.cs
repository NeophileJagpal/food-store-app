using FoodStore.Infrastructure.Persistence;
using FoodStore.SharedKernal.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoodStoreDbContext>(options =>
                     options.UseInMemoryDatabase("FoodStoreDB"));
            //services.AddDbContext()
            services.AddScoped(typeof(IReadRepository<>), typeof(EfReadRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            return services;
        }
    }
}
