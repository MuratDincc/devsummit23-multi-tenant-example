using BFF.Api.Business.Tenant.Abstracts;
using BFF.Api.Business.Tenant.Dto;
using BFF.Api.Services.Tenant;
using BFF.Api.Services.Tenant.Models.Request;

namespace BFF.Api.Business.Tenant;

public class TenantBusiness : ITenantBusiness
{
    private readonly ITenantService _tenantService;

    public TenantBusiness(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    public async Task<CreateTenantResultDto> Create(int userId, string slug, string title)
    {
        var response = await _tenantService.CreateTenant(new CreateTenantServiceRequest
        {
            UserId = userId,
            Title = title,
            Slug = slug,
        });

        if (response == null)
            throw new Exception("Tenant create error!");
        
        return new CreateTenantResultDto
        {
            Id = response.Id,
            Slug = response.Slug,
            DatabaseName = response.DatabaseName,
            ConnectionString = response.ConnectionString
        };
    }
    
    public async Task<GetTenantDto> GetById(int id)
    {
        var response = await _tenantService.GetTenantById(id);
        if (response == null)
            throw new Exception("Tenant not found!");
        
        return new GetTenantDto
        {
            Id = response.Id,
            AliasId = response.AliasId,
            Title = response.Title,
            Slug = response.Slug,
            ConnectionString = response.ConnectionString
        };
    }
    
    public async Task<GetTenantDto> GetBySlug(string slug)
    {
        var response = await _tenantService.GetTenantBySlug(slug);
        if (response == null)
            throw new Exception("Tenant not found!");
        
        return new GetTenantDto
        {
            Id = response.Id,
            AliasId = response.AliasId,
            Title = response.Title,
            Slug = response.Slug,
            ConnectionString = response.ConnectionString
        };
    }
}