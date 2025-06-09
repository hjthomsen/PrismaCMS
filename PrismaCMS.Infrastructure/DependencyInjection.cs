using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrismaCMS.Application.Common.Interfaces;
using PrismaCMS.Domain.Entities;
using PrismaCMS.Infrastructure.Persistence;
using PrismaCMS.Infrastructure.Persistence.Repositories;
using System;
using System.Threading.Tasks;

namespace PrismaCMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

            // Register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var logger = services.GetRequiredService<ILogger<ApplicationDbContextSeed>>();

                await ApplicationDbContextSeed.SeedSampleDataAsync(context, logger);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<ApplicationDbContextSeed>>();
                logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }
    }
}