using System.Security.Claims;

namespace BFF.Api.Context;

public class WorkContext : IWorkContext
{
    private readonly IHttpContextAccessor _context;

    public WorkContext(IHttpContextAccessor context)
    {
        _context = context;
    }

    public int UserId
    {
        get
        {
            if (_context.HttpContext?.User?.HasClaim(x => x.Type == ClaimTypes.Sid) ?? false)
                return Convert.ToInt32(_context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            
            return 0;
        }
    }

    public int TenantId
    {
        get
        {
            if (_context.HttpContext?.User?.HasClaim(x => x.Type == "TenantId") ?? false)
                return Convert.ToInt32(_context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "TenantId").Value);
            
            return 0;
        }
    }

    public string ConnectionString
    {
        get
        {
            if (_context.HttpContext?.User?.HasClaim(x => x.Type == "ConnectionString") ?? false)
                return _context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "ConnectionString").Value;
            
            return string.Empty;
        }
    }
}