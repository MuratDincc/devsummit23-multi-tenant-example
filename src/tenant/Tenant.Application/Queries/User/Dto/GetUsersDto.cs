namespace Tenant.Application.Queries.User.Dto;

public record GetUsersDto
{
    public List<GetUserDto> Users { get; init; }
}
