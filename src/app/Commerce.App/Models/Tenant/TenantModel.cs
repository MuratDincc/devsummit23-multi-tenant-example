namespace Commerce.App.Models.Tenant;

public record TenantModel
{
    public int Id { get; init; }
    public string Title { get; init; }
    public decimal ConnectionString { get; init; }
}