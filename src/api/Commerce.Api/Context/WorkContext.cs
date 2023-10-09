using Commerce.Infrastructure.Abstracts;

namespace Commerce.Api.Context;

public class WorkContext : IWorkContext
{
    private readonly IHttpContextAccessor _context;

    public WorkContext(IHttpContextAccessor context)
    {
        _context = context;
    }

    public int TenantId
    {
        get
        {
            if (_context?.HttpContext?.Request.Headers.ContainsKey("X-TenantId") ?? false)
                return Convert.ToInt32(_context.HttpContext.Request.Headers["X-TenantId"]);

            return 0;
        }
    }

    public string ConnectionString
    {
        get
        {
            if (_context?.HttpContext?.Request.Headers.ContainsKey("X-Connection-String") ?? false)
                return _context.HttpContext.Request.Headers["X-Connection-String"].ToString();

            return string.Empty;
        }
    }
}