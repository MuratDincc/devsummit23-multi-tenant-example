using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Infrastructure.Repositories;
using Tenant.Infrastructure.Repositories.Concretes;

namespace Tenant.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Entity Framework & Repository
        services.AddDbContext<TenantDbContext>(
            (serviceProvider, dbContextBuilder) =>
            {
                dbContextBuilder.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"),
                                optionsBuilder =>
                                {
                                    optionsBuilder.MigrationsAssembly("Tenant.Infrastructure");
                                    optionsBuilder.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                                }).UseSnakeCaseNamingConvention();
            });

        services.AddScoped(typeof(IRepository<>), typeof(LinqToSqlRepository<>));
    }
}
