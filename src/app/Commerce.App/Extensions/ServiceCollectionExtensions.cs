using System.Text.Json;
using System.Text.Json.Serialization;
using Commerce.App.Business.Product;
using Commerce.App.Business.Product.Abstracts;
using Commerce.App.Context;
using Commerce.App.Services.Bff;
using Refit;

namespace Commerce.App.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddBffServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Http Context Accessor
        services.AddHttpContextAccessor();
        
        // Work Context
        services.AddScoped<IWorkContext, WorkContext>();
        
        // Business
        services.AddScoped<IProductBusiness, ProductBusiness>();
        
        // Services
        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(GetJsonSerializerOptions())
        };
        
        var bffServiceUrl = configuration.GetValue<string>("Services:Bff");
        services.AddRefitClient<IBffService>(refitSettings).ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(bffServiceUrl);
            c.Timeout = TimeSpan.FromSeconds(20.0);
        });
    }
    
    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true
        };

        return options;
    }
}
