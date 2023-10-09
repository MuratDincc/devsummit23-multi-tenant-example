using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BFF.Api.Business.Authentication.Abstracts;
using BFF.Api.Business.Authentication.Dto;
using BFF.Api.Services.Tenant;
using BFF.Api.Services.Tenant.Models.Request;
using Microsoft.IdentityModel.Tokens;
using Rubic.AspNetCore.Configurations;
using Rubic.AspNetCore.Security;

namespace BFF.Api.Business.Authentication;

public class AuthenticationBusiness : IAuthenticationBusiness
{
    private readonly ICryptographyProvider _cryptographyProvider;
    private readonly ITenantService _tenantService;
    private readonly RubicSecurityConfiguration _rubicSecurityConfiguration;

    public AuthenticationBusiness(ICryptographyProvider cryptographyProvider,
        ITenantService tenantService,
        RubicSecurityConfiguration rubicSecurityConfiguration)
    {
        _cryptographyProvider = cryptographyProvider;
        _tenantService = tenantService;
        _rubicSecurityConfiguration = rubicSecurityConfiguration;
    }

    private async Task<SecurityTokenDescriptor> CreateClaims(int id, string name, string surname, string email, int? tenantId, string connectionString)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, id.ToString()),
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Surname, surname),
            new Claim(ClaimTypes.Email, email)
        };

        if (tenantId != null)
            claims.Add(new Claim("TenantId", tenantId.ToString()));
        
        if (!string.IsNullOrWhiteSpace(connectionString))
            claims.Add(new Claim("ConnectionString", connectionString));
        
        var key = Encoding.ASCII.GetBytes(_rubicSecurityConfiguration.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenDescriptor;
    }
    
    public async Task<AuthenticationResultDto> Authenticate(string email, string password)
    {
        password = _cryptographyProvider.Encrypt(password);
        
        var response = await _tenantService.GetUserByEmailAndPassword(new AuthenticationServiceRequest
        {
            Email = email,
            Password = password
        });

        if (response == null)
            throw new Exception("User not found!");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = await CreateClaims(response.Id, response.Name, response.Surname, response.Email, null, string.Empty);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var createdToken = tokenHandler.WriteToken(token);
        
        return new AuthenticationResultDto
        {
            Token = createdToken,
            ExpiryDate = tokenDescriptor.Expires.Value
        };
    }

    public async Task<ChangeTenantResultDto> ChangeTenant(int userId, int tenantId)
    {
        var response = await _tenantService.GetUserById(userId);
        if (response == null)
            throw new Exception("User not found!");

        if (!response.TenantIds.Exists(x => x == tenantId))
            throw new Exception("You do not have authority over the tenant!");

        var tenantServiceResponse = await _tenantService.GetTenantById(tenantId);
        if (tenantServiceResponse == null)
            throw new Exception("Tenant not found!");
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = await CreateClaims(response.Id, response.Name, response.Surname, response.Email, tenantId, tenantServiceResponse.ConnectionString);
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var createdToken = tokenHandler.WriteToken(token);
        
        return new ChangeTenantResultDto
        {
            Token = createdToken,
            ExpiryDate = tokenDescriptor.Expires.Value
        };
    }
}