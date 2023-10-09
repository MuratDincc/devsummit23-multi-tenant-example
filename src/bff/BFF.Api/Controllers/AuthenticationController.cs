using BFF.Api.Business.Authentication.Abstracts;
using BFF.Api.Business.User.Abstracts;
using BFF.Api.Models.Authentication.Request;
using BFF.Api.Models.Authentication.Response;
using BFF.Api.Models.User.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Api.Controllers;

[ApiController]
[Route("api/v1/authentication")]
public class AuthenticationController : BaseController
{
    #region Fields

    private readonly IAuthenticationBusiness _authenticationBusiness;
    private readonly IUserBusiness _userBusiness;

    #endregion

    #region Ctor

    public AuthenticationController(IAuthenticationBusiness authenticationBusiness, IUserBusiness userBusiness)
    {
        _authenticationBusiness = authenticationBusiness;
        _userBusiness = userBusiness;
    }

    #endregion
    
    #region Methods

    /// <summary>
    /// Authentication
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(AuthenticationResponse), 200)]
    public async Task<IActionResult> Post([FromBody] AuthenticationRequest request)
    {
        var response = await _authenticationBusiness.Authenticate(request.Email, request.Password);

        return Ok(new AuthenticationResponse
        {
            Token = response.Token,
            ExpiryDate = response.ExpiryDate
        });
    }
    
    /// <summary>
    /// Get Current Authentication
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(GetUserResponse), 200)]
    public async Task<IActionResult> Get()
    {
        var response = await _userBusiness.GetById(WorkContext.UserId);
        
        return Ok(new GetUserResponse
        {
            Id = response.Id,
            Name = response.Name,
            Surname = response.Surname,
            Email = response.Email,
            CreatedOnUtc = response.CreatedOnUtc,
            UpdatedOnUtc = response.UpdatedOnUtc,
            TenantIds = response.TenantIds
        });
    }

    /// <summary>
    /// Tenant Change
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("change-tenant")]
    [ProducesResponseType(typeof(ChangeTenantResponse), 200)]
    public async Task<IActionResult> PostChangeTenant([FromBody] ChangeTenantRequest request)
    {
        var response = await _authenticationBusiness.ChangeTenant(WorkContext.UserId, request.TenantId);

        return Ok(new ChangeTenantResponse
        {
            Token = response.Token,
            ExpiryDate = response.ExpiryDate
        });
    }
    
    #endregion
}
