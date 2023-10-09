using Microsoft.AspNetCore.Mvc;

namespace Tenant.Api.Controllers;

[ApiController]
[Route("api/v1/cache")]
public class CacheController : ControllerBase
{
    private readonly ILogger<CacheController> _logger;

    public CacheController(ILogger<CacheController> logger)
    {
        _logger = logger;
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return Ok();
    }
}
