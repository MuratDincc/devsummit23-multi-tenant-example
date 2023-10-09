namespace Commerce.Infrastructure.Abstracts;

public interface IWorkContext
{
    int TenantId { get; }
    string ConnectionString { get; }
}