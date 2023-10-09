using BFF.Api.Business.Tenant.Abstracts;
using BFF.Api.Models.Tenant.Request;
using BFF.Api.Models.Tenant.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/tenants")]
public class TenantController : BaseController
{
    #region Fields

    private readonly ITenantBusiness _tenantBusiness;

    #endregion

    #region Ctor

    public TenantController(ITenantBusiness tenantBusiness)
    {
        _tenantBusiness = tenantBusiness;
    }

    #endregion
    
    #region Methods

    /// <summary>
    /// Get Tenant By Slug
    /// </summary>
    /// <param name="slug">Slug</param>
    /// <returns></returns>
    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(GetTenantResponse), 200)]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var response = await _tenantBusiness.GetBySlug(slug);
        if (response == null)
            throw new Exception("Tenant not found!");

        return Ok(new GetTenantResponse
        {
            Id = response.Id,
            AliasId = response.AliasId,
            Title = response.Title,
            Slug = response.Slug,
            ConnectionString = response.ConnectionString
        });
    }
    
    /// <summary>
    /// Create Tenant
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(CreateTenantResponse), 200)]
    public async Task<IActionResult> Post([FromBody] CreateTenantRequest request)
    {
        var response = await _tenantBusiness.Create(WorkContext.UserId, request.Slug, request.Title);

        if (response == null)
            throw new Exception("Tenant not created!");

        return Ok(new CreateTenantResponse
        {
            Id = response.Id,
            Slug = response.Slug,
            DatabaseName = response.DatabaseName,
            ConnectionString = response.ConnectionString
        });
    }

    #endregion
}
