using Microsoft.AspNetCore.Mvc;
using Tenant.Api.Models.Tenant.Request;
using Tenant.Api.Models.Tenant.Response;
using Tenant.Application.Commands.Tenant;
using Tenant.Application.Queries.Tenant;

namespace Tenant.Api.Controllers;

[ApiController]
[Route("api/v1/tenants")]
public class TenantController : BaseController
{
    #region Methods

    /// <summary>
    /// Get Tenant By Id
    /// </summary>
    /// <param name="id">Tenant Id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetTenantResponse), 200)]
    public async Task<IActionResult> GetById(int id)
    {
        var tenant = await Mediator.Send(new GetTenantByIdQuery
        {
            Id = id
        });

        return Ok(new GetTenantResponse
        {
            Id = id,
            AliasId = tenant.AliasId,
            Title = tenant.Title,
            Slug = tenant.Slug,
            ConnectionString = tenant.ConnectionString
        });
    }
    
    /// <summary>
    /// Get Tenant By Slug
    /// </summary>
    /// <param name="slug">Slug</param>
    /// <returns></returns>
    [HttpGet("get-by-slug/{slug}")]
    [ProducesResponseType(typeof(GetTenantResponse), 200)]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var tenant = await Mediator.Send(new GetTenantBySlugQuery
        {
            Slug = slug
        });

        return Ok(new GetTenantResponse
        {
            Id = tenant.Id,
            AliasId = tenant.AliasId,
            Title = tenant.Title,
            Slug = tenant.Slug,
            ConnectionString = tenant.ConnectionString
        });
    }

    /// <summary>
    /// Get Tenants
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetTenantsResponse), 200)]
    public async Task<IActionResult> Get()
    {
        var data = await Mediator.Send(new GetTenantsQuery());

        return Ok(new GetTenantsResponse
        {
            Tenants = data.Tenants.Select(x => new GetTenantResponse
            {
                Id = x.Id,
                AliasId = x.AliasId,
                Slug = x.Slug
            }).ToList()
        });
    }

    /// <summary>
    /// Create Tenant
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateTenantResponse), 200)]
    public async Task<IActionResult> Post([FromBody] CreateTenantRequest request)
    {
        var result = await Mediator.Send(new CreateTenantCommand
        {
            UserId = request.UserId,
            Title = request.Title,
            Slug = request.Slug
        });

        return Ok(new CreateTenantResponse
        {
            Id = result.Id,
            Slug = result.Slug,
            DatabaseName = result.DatabaseName,
            ConnectionString = result.ConnectionString
        });
    }

    /// <summary>
    /// Update Tenant
    /// </summary>
    /// <param name="id">Tenant Id</param>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateTenantRequest request)
    {
        await Mediator.Send(new UpdateTenantCommand
        {
            AliasId = id,
            Title = request.Title,
            Slug = request.Slug
        });

        return Ok();
    }

    #endregion
}
