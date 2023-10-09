using Commerce.App.Services.Bff.Models.Response;
using Refit;

namespace Commerce.App.Services.Bff;

public interface IBffService
{
    [Get("/api/v1/tenants/{slug}")]
    Task<GetTenantServiceResponse> GetTenantBySlug(string slug);
    
    [Get("/api/v1/products")]
    Task<GetProductsServiceResponse> GetProducts([Header("X-Tenant-Id")] int tenantId, [Header("X-Connection-String")] string connectionString);
}