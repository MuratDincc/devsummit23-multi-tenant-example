using BFF.Api.Services.Tenant.Models.Request;
using BFF.Api.Services.Tenant.Models.Response;
using Refit;

namespace BFF.Api.Services.Tenant;

public interface ITenantService
{
    #region Tenant

    [Post("/api/v1/tenants")]
    Task<CreateTenantServiceResponse> CreateTenant([Body] CreateTenantServiceRequest request);
    
    [Get("/api/v1/tenants/{id}")]
    Task<GetTenantServiceResponse> GetTenantById(int id);
    
    [Get("/api/v1/tenants/get-by-slug/{slug}")]
    Task<GetTenantServiceResponse> GetTenantBySlug(string slug);

    #endregion

    #region User

    [Post("/api/v1/users")]
    Task<CreateUserServiceResponse> CreateUser([Body] CreateUserServiceRequest request);
    
    [Get("/api/v1/users/{id}")]
    Task<GetUserServiceResponse> GetUserById(int id);
    
    [Post("/api/v1/users/{id}")]
    Task PatchUserInformation(int id, [Body] PatchUserInformationServiceRequest request);
    
    [Post("/api/v1/users/login")]
    Task<AuthenticationServiceResponse> GetUserByEmailAndPassword([Body] AuthenticationServiceRequest request);    

    #endregion
}