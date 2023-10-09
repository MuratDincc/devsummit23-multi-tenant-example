using Commerce.Infrastructure.Repositories;
using Commerce.Infrastructure.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rubic.EntityFramework.Repositories.Abstracts;

namespace Commerce.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Entity Framework & Repository
        services.AddDbContext<CommerceDbContext>((dbContextBuilder) =>
        {
            dbContextBuilder.UseNpgsql(optionsBuilder =>
                {
                    optionsBuilder.MigrationsAssembly("Commerce.Infrastructure");
                    optionsBuilder.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                }).UseSnakeCaseNamingConvention();
        });

        services.AddScoped(typeof(IRepository<>), typeof(LinqToSqlRepository<>));
    }
}
