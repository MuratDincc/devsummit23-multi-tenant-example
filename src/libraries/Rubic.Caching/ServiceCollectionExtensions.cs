using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rubic.Caching.Configurations;

namespace Rubic.Caching;

public static class ServiceCollectionExtensions
{
    public static void AddRubicCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var rubicCacheConfiguration = configuration.GetSection("Rubic:Cache").Get<RubicCacheConfiguration>();
        var distributedCacheConfiguration = configuration.GetSection("Rubic:DistributedCache").Get<RubicDistributedCacheConfiguration>();
        
        if (rubicCacheConfiguration == null || distributedCacheConfiguration == null)
            throw new Exception("Cache config error!");

        services.AddSingleton(rubicCacheConfiguration);
        services.AddSingleton(distributedCacheConfiguration);
        
        if (distributedCacheConfiguration.Enabled)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = distributedCacheConfiguration.ConnectionString;
                options.InstanceName = distributedCacheConfiguration.InstanceName;
            });
            
            services.AddScoped<ILocker, DistributedCacheManager>();
            services.AddScoped<IStaticCacheManager, DistributedCacheManager>();
        }
        else
        {
            services.AddMemoryCache();
            services.AddSingleton<ILocker, MemoryCacheManager>();
            services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
        }
    }
}
