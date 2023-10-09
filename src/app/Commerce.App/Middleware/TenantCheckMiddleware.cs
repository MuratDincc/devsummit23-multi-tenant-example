using System.Net;
using Commerce.App.Services.Bff;
using Microsoft.Extensions.Primitives;

namespace Commerce.App.Middleware;

public class TenantCheckMiddleware
{
    private readonly RequestDelegate _next;
    
    public TenantCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext httpContext)
    {
        if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.Contains("/Home/TenantNotFound"))
            await _next.Invoke(httpContext);

        var fullAddress = httpContext?.Request?.Headers?["Host"].ToString()?.Split('.');

        if (fullAddress.Length < 2)
        {
            httpContext.Response.Redirect("/Home/TenantNotFound");
            
            await _next.Invoke(httpContext);
        }
        
        var tenantSlug = fullAddress[0];

        try
        {
            var bffService = httpContext.RequestServices.GetService<IBffService>();
            var tenantServiceResponse = await bffService.GetTenantBySlug(tenantSlug);
            if (tenantServiceResponse == null)
            {
                httpContext.Response.Redirect("/Home/TenantNotFound");
                
                await _next.Invoke(httpContext);
            }
        
            httpContext.Items.Add("Tenant-Title", tenantServiceResponse.Title);
        
            httpContext.Request.Headers.Add(new KeyValuePair<string, StringValues>("X-TenantId", tenantServiceResponse.Id.ToString()));
            httpContext.Request.Headers.Add(new KeyValuePair<string, StringValues>("X-Connection-String", tenantServiceResponse.ConnectionString));
        }
        catch (Refit.ApiException e)
        {
            httpContext.Response.Redirect("/Home/TenantNotFound");
                
            await _next.Invoke(httpContext);
        }
        
        await _next.Invoke(httpContext);
    }
}