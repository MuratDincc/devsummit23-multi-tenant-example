namespace BFF.Api.Context;

public interface IWorkContext
{
    int UserId { get; }
    int TenantId { get; }
    string ConnectionString { get; }
}