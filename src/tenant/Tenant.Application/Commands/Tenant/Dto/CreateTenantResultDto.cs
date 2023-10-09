namespace Tenant.Application.Commands.Tenant.Dto;

public record CreateTenantResultDto
{
    public Guid Id { get; init; }
    public string Slug { get; init; }
    public string DatabaseName { get; init; }
    public string ConnectionString { get; init; }
}
