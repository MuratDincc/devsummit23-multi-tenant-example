using BFF.Api.Business.User.Dto;

namespace BFF.Api.Business.User.Abstracts;

public interface IUserBusiness
{
    Task<CreateUserResultDto> Create(string name, string surname, string email, string password);
    
    Task<GetUserDto> GetById(int id);
    
    Task PatchUserInformation(int userId, string name, string surname);
}