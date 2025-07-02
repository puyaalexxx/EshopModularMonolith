using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Register services related to the Catalog module here
            // Example: services.AddScoped<ICatalogService, CatalogService>();

            return services;
        }
    }
}
