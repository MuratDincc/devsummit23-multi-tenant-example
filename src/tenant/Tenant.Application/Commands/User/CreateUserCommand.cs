using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.AspNetCore.Exceptions;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Application.Commands.User.Dto;

namespace Tenant.Application.Commands.User;

public record CreateUserCommand : IRequest<CreateUserResultDto>
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}

public record CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResultDto>
{
    private readonly IRepository<Entities.User> _userRepository;
    private readonly IPublisher _publisher;

    public CreateUserCommandHandler(IRepository<Entities.User> userRepository, 
        IPublisher publisher)
    {
        _userRepository = userRepository;
        _publisher = publisher;
    }

    public async Task<CreateUserResultDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user != null)
            throw new StatusException(status: StatusCode.BadRequest, "There is an account registered to the email address you entered!");

        var entity = new Entities.User
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            Password = request.Password,
            CreatedOnUtc = DateTimeOffset.Now
        };

        await _userRepository.InsertAsync(entity);
        await _userRepository.SaveAllAsync();

        return new CreateUserResultDto
        {
            Id = entity.Id
        };
    }
}
