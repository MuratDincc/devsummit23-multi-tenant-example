using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Application.Queries.User.Dto;

namespace Tenant.Application.Queries.User;

public record GetUsersQuery : IRequest<GetUsersDto>;

public record GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersDto>
{
    private readonly IRepository<Entities.User> _userRepository;

    public GetUsersQueryHandler(IRepository<Entities.User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUsersDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var data = await _userRepository.Table.AsNoTracking()
            .Where(x => !x.Deleted)
            .Select(x => new GetUserDto
            {
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                CreatedOnUtc = x.CreatedOnUtc,
                UpdatedOnUtc = x.UpdatedOnUtc
            })
            .ToListAsync();

        return new GetUsersDto
        {
            Users = data
        };
    }
}