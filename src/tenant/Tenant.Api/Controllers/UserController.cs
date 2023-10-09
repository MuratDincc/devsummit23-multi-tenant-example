using Microsoft.AspNetCore.Mvc;
using Tenant.Api.Models.User.Request;
using Tenant.Api.Models.User.Response;
using Tenant.Application.Commands.User;
using Tenant.Application.Queries.User;

namespace Tenant.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : BaseController
{
    #region Methods

    /// <summary>
    /// Get User By Id
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserResponse), 200)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var user = await Mediator.Send(new GetUserByIdQuery
        {
            Id = id
        });

        return Ok(new GetUserResponse
        {
            Id = id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            CreatedOnUtc = user.CreatedOnUtc,
            UpdatedOnUtc = user.UpdatedOnUtc,
            TenantIds = user.TenantIds
        });
    }

    /// <summary>
    /// Get Users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetUsersResponse), 200)]
    public async Task<IActionResult> Get()
    {
        var data = await Mediator.Send(new GetUsersQuery());

        return Ok(new GetUsersResponse
        {
            Users = data.Users.Select(x => new GetUserResponse
            {
                Name = x.Name,
                Surname = x.Surname,
                CreatedOnUtc = x.CreatedOnUtc,
                UpdatedOnUtc = x.UpdatedOnUtc
            }).ToList()
        });
    }

    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResponse), 200)]
    public async Task<IActionResult> Post([FromBody] CreateUserRequest request)
    {
        var result = await Mediator.Send(new CreateUserCommand
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            Password = request.Password
        });

        return Ok(new CreateUserResponse
        {
            Id = result.Id
        });
    }

    /// <summary>
    /// Patch User Information
    /// </summary>
    /// <param name="id">User Id</param>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [HttpPut("user-information/{id}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> PatchUserInformarion([FromRoute] int id, [FromBody] PatchUserInformationRequest request)
    {
        await Mediator.Send(new PatchUserInformationCommand
        {
            Id = id,
            Name = request.Name,
            Surname = request.Surname
        });

        return Ok();
    }
    
    /// <summary>
    /// Get User By Email and Password
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(GetUserByEmailAndPasswordResponse), 200)]
    public async Task<IActionResult> Post([FromBody] GetUserByEmailAndPasswordRequest request)
    {
        var result = await Mediator.Send(new GetUserByEmailPasswordQuery
        {
            Email = request.Email,
            Password = request.Password
        });

        return Ok(new GetUserByEmailAndPasswordResponse
        {
            Id = result.Id,
            Name = result.Name,
            Surname = result.Surname,
            Email = result.Email,
            TenantIds = result.TenantIds,
            CreatedOnUtc = result.CreatedOnUtc,
            UpdatedOnUtc = result.UpdatedOnUtc
        });
    }

    #endregion
}
