using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviors;
using System.Reflection;

namespace Shared.Extensions
{
    public static class MediatRExtensions
    {
        public static IServiceCollection AddMediatRWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            //Application use case services
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(assemblies);
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            //fluent validation
            services.AddValidatorsFromAssemblies(assemblies);

            return services;
        }
    }
}
