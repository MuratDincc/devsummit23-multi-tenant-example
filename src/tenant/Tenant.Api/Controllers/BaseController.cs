using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Tenant.Api.Controllers;

/// <summary>
/// BaseController
/// </summary>
[ApiController]
public class BaseController : ControllerBase
{
    private ISender _mediator;

    /// <summary>
    /// Mediator
    /// </summary>
    protected ISender Mediator
    {
        get
        {
            if (_mediator == null)
                _mediator = HttpContext.RequestServices.GetRequiredService<ISender>();

            return _mediator;
        }
    }
}
