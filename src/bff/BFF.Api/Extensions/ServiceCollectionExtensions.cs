using System.Text.Json;
using System.Text.Json.Serialization;
using BFF.Api.Business.Authentication;
using BFF.Api.Business.Authentication.Abstracts;
using BFF.Api.Business.Product;
using BFF.Api.Business.Product.Abstracts;
using BFF.Api.Business.Tenant;
using BFF.Api.Business.Tenant.Abstracts;
using BFF.Api.Business.User;
using BFF.Api.Business.User.Abstracts;
using BFF.Api.Services.Commerce;
using BFF.Api.Services.Tenant;
using Refit;

namespace BFF.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddBffServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Business
        services.AddTransient<IAuthenticationBusiness, AuthenticationBusiness>();
        services.AddTransient<IProductBusiness, ProductBusiness>();
        services.AddTransient<ITenantBusiness, TenantBusiness>();
        services.AddTransient<IUserBusiness, UserBusiness>();
        
        // Services
        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(GetJsonSerializerOptions())
        };
        
        var tenantServiceUrl = configuration.GetValue<string>("Services:Tenant");
        services.AddRefitClient<ITenantService>(refitSettings).ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(tenantServiceUrl);
            c.Timeout = TimeSpan.FromSeconds(20.0);
        });
        
        var commerceServiceUrl = configuration.GetValue<string>("Services:Commerce");
        services.AddRefitClient<ICommerceService>(refitSettings).ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(commerceServiceUrl);
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
