using BFF.Api.Context;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Api.Controllers;

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
}
