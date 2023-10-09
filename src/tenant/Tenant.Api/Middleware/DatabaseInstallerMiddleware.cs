using Microsoft.EntityFrameworkCore;
using Tenant.Infrastructure.Repositories;

namespace Tenant.Api.Middleware;

public class DatabaseInstallerMiddleware
{
    private readonly RequestDelegate _next;
    
    public DatabaseInstallerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
 
    public async Task Invoke(HttpContext httpContext, TenantDbContext dbContext)
    {
        await dbContext.Database.EnsureCreatedAsync();
        
        await _next.Invoke(httpContext);
    }
}