using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PrismaCMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register AutoMapper with the assembly containing the mapping profiles
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Here you would add services like MediatR, FluentValidation, etc.
            // Example:
            // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}