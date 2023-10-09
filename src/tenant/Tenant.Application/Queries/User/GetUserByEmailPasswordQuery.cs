using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.AspNetCore.Exceptions;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Application.Queries.User.Dto;

namespace Tenant.Application.Queries.User;

public record GetUserByEmailPasswordQuery : IRequest<GetUserDto>
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public record GetUserByEmailPasswordQueryHandler : IRequestHandler<GetUserByEmailPasswordQuery, GetUserDto>
{
    private readonly IRepository<Entities.User> _userRepository;

    public GetUserByEmailPasswordQueryHandler(IRepository<Entities.User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserDto> Handle(GetUserByEmailPasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password && !x.Deleted);
        if (user == null)
            throw new StatusException(status: StatusCode.NotFound, "User Not Found!");

        return new GetUserDto
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            CreatedOnUtc = user.CreatedOnUtc,
            UpdatedOnUtc = user.UpdatedOnUtc,
            TenantIds = null
        };
    }
}
