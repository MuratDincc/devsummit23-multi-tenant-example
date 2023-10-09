using BFF.Api.Business.User.Abstracts;
using BFF.Api.Business.User.Dto;
using BFF.Api.Services.Tenant;
using BFF.Api.Services.Tenant.Models.Request;
using Rubic.AspNetCore.Security;

namespace BFF.Api.Business.User;

public class UserBusiness : IUserBusiness
{
    private readonly ICryptographyProvider _cryptographyProvider;
    private readonly ITenantService _tenantService;

    public UserBusiness(ICryptographyProvider cryptographyProvider, ITenantService tenantService)
    {
        _cryptographyProvider = cryptographyProvider;
        _tenantService = tenantService;
    }

    public async Task<CreateUserResultDto> Create(string name, string surname, string email, string password)
    {
        password = _cryptographyProvider.Encrypt(password);
        
        var response = await _tenantService.CreateUser(new CreateUserServiceRequest
        {
           Name = name,
           Surname = surname,
           Email = email,
           Password = password
        });
        
        return new CreateUserResultDto
        {
            Id = response.Id
        };
    }

    public async Task<GetUserDto> GetById(int id)
    {
        var response = await _tenantService.GetUserById(id);
        
        return new GetUserDto
        {
            Id = response.Id,
            Name = response.Name,
            Surname = response.Surname,
            Email = response.Email,
            CreatedOnUtc = response.CreatedOnUtc,
            UpdatedOnUtc = response.UpdatedOnUtc,
            TenantIds = response.TenantIds
        };
    }
    
    public async Task PatchUserInformation(int userId, string name, string surname)
    {
        await _tenantService.PatchUserInformation(userId, new PatchUserInformationServiceRequest
        {
            Name = name,
            Surname = surname
        });
    }
}