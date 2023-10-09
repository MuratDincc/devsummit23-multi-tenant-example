using Commerce.App.Middleware;

namespace Commerce.App.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseTenantCheckMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TenantCheckMiddleware>();
    }
}