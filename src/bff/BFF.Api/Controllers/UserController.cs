using BFF.Api.Business.User.Abstracts;
using BFF.Api.Models.User.Request;
using BFF.Api.Models.User.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    #region Fields

    private readonly IUserBusiness _userBusiness;

    #endregion

    #region Ctor

    public UserController(IUserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    #endregion
    
    #region Methods

    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [AllowAnonymous] 
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResponse), 200)]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
    {
        var response = await _userBusiness.Create(request.Name, request.Surname, request.Email, request.Password);

        return Ok(new CreateUserResponse
        {
            Id = response.Id
        });
    }

    /// <summary>
    /// Patch User Information
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [Authorize]
    [HttpPatch("{id}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Patch(int id, [FromBody] PatchUserInformationRequest request)
    {
        await _userBusiness.PatchUserInformation(id, request.Name, request.Surname);

        return Ok();
    }
    
    #endregion
}
