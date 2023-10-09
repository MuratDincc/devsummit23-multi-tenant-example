using Commerce.Infrastructure.Repositories;

namespace Commerce.Api.Middleware;

public class DatabaseInstallerMiddleware
{
    private readonly RequestDelegate _next;
    
    public DatabaseInstallerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
 
    public async Task Invoke(HttpContext httpContext, CommerceDbContext dbContext)
    {
        var connectionString = httpContext.Request.Headers["X-Connection-String"].ToString();
        if (!string.IsNullOrWhiteSpace(connectionString))
            await dbContext.Database.EnsureCreatedAsync();
        
        await _next.Invoke(httpContext);
    }
}