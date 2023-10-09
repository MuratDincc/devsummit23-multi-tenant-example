namespace BFF.Api.Models.User.Response;

public record GetUsersResponse
{
    public List<GetUserResponse> Users { get; init; }
}
