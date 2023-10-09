using BFF.Api.Business.Authentication.Dto;

namespace BFF.Api.Business.Authentication.Abstracts;

public interface IAuthenticationBusiness
{
    Task<AuthenticationResultDto> Authenticate(string email, string password);
    
    Task<ChangeTenantResultDto> ChangeTenant(int userId, int tenantId);
}