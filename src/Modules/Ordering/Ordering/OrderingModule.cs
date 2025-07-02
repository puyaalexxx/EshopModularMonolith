using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering
{
    public static class OrderingModule
    {
        public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Register services related to the Catalog module here
            // Example: services.AddScoped<ICatalogService, CatalogService>();

            return services;
        }
    }
}
