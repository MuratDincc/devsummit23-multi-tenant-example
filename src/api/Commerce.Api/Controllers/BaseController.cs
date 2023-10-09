using Commerce.Infrastructure.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Api.Controllers;

/// <summary>
/// BaseController
/// </summary>
[ApiController]
public class BaseController : ControllerBase
{
    private IWorkContext _workContext;

    /// <summary>
    /// Work Context
    /// </summary>
    protected IWorkContext WorkContext
    {
        get
        {
            if (_workContext == null)
                _workContext = HttpContext.RequestServices.GetRequiredService<IWorkContext>();

            return _workContext;
        }
    }
    
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
