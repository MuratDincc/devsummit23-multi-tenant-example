namespace Commerce.App.Context;

public interface IWorkContext
{
    int TenantId { get; }
    string ConnectionString { get; }
}