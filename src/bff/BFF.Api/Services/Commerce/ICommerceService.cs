using BFF.Api.Services.Commerce.Models.Request;
using BFF.Api.Services.Commerce.Models.Response;
using Refit;

namespace BFF.Api.Services.Commerce;

public interface ICommerceService
{
    [Get("/api/v1/products")]
    Task<GetProductsServiceResponse> GetProducts([Header("X-TenantId")] int tenantId, [Header("X-Connection-String")] string connectionString);
    
    [Get("/api/v1/products/{id}")]
    Task<GetProductServiceResponse> GetProductById(int id, [Header("X-TenantId")] int tenantId, [Header("X-Connection-String")] string connectionString);
    
    [Post("/api/v1/products")]
    Task<CreateProductServiceResponse> CreateProduct([Body] CreateProductServiceRequest request, [Header("X-TenantId")] int tenantId, [Header("X-Connection-String")] string connectionString);
}