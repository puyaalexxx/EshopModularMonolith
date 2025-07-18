﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Shared.Data;
using Shared.Data.Interceptors;

namespace Basket
{
    public static class BasketModule
    {
        public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration configuration)
        {
            //Application Use Case services
            services.AddScoped<IBasketRepository, BasketRepository>();
            /*services.AddScoped<IBasketRepository>(provider =>
            {
                var basketRepository = provider.GetRequiredService<BasketRepository>();

                return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());
            });*/
            //Scrutor
            services.Decorate<IBasketRepository, CachedBasketRepository>();

            //Data - Infrastructure services
            var connectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<BasketDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString);
            });

            // services.AddScoped<IDataSeeder, CatalogDataSeeder>();

            return services;
        }

        public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
        {

            //Data - Infrastructure services
            app.UseMigration<BasketDbContext>();

            return app;
        }
    }
}
