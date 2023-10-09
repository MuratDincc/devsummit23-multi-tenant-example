using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.AspNetCore.Exceptions;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Application.Events.Caching;
using Tenant.Domain.Constants;

namespace Tenant.Application.Commands.User;

public record PatchUserInformationCommand : IRequest
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
}

public record PatchUserInformationCommandHandler : IRequestHandler<PatchUserInformationCommand>
{
    private readonly IRepository<Entities.User> _userRepository;
    private readonly IPublisher _publisher;

    public PatchUserInformationCommandHandler(IRepository<Entities.User> userRepository, IPublisher publisher)
    {
        _userRepository = userRepository;
        _publisher = publisher;
    }

    public async Task Handle(PatchUserInformationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Table.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted);
        if (user == null)
            throw new StatusException(status: StatusCode.NotFound, "User Not Found!");

        user.Name = request.Name;
        user.Surname = request.Surname;
        user.UpdatedOnUtc = DateTimeOffset.UtcNow;

        await _userRepository.SaveAllAsync();
        
        // Cache Clear Event
        await _publisher.Publish(new DeleteCacheKeysEvent
        {
            Keys = new List<string>
            {
                new CacheKey(CacheKeyConstants.TenantById, user.Id.ToString()).Key
            }
        });
    }
}