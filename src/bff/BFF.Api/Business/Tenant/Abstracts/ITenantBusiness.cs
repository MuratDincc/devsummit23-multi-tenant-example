using BFF.Api.Business.Tenant.Dto;

namespace BFF.Api.Business.Tenant.Abstracts;

public interface ITenantBusiness
{
    Task<CreateTenantResultDto> Create(int userId, string slug, string title);
    Task<GetTenantDto> GetById(int id);
    Task<GetTenantDto> GetBySlug(string slug);
}