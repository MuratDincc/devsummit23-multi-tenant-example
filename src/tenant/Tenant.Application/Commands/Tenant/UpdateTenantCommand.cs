using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.AspNetCore.Exceptions;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Application.Events.Caching;
using Tenant.Domain.Constants;

namespace Tenant.Application.Commands.Tenant;

public record UpdateTenantCommand : IRequest
{
    public Guid AliasId { get; init; }
    public string Slug { get; init; }
    public string Title { get; init; }
}

public record UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand>
{
    private readonly IRepository<Entities.Tenant> _tenantRepository;
    private readonly IPublisher _publisher;

    public UpdateTenantCommandHandler(IRepository<Entities.Tenant> tenantRepository, IPublisher publisher)
    {
        _tenantRepository = tenantRepository;
        _publisher = publisher;
    }

    public async Task Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.Table.FirstOrDefaultAsync(x => x.AliasId == request.AliasId && !x.Deleted);
        if (tenant == null)
            throw new StatusException(status: StatusCode.NotFound, "Tenant Not Found!");

        tenant.Title = request.Title;

        await _tenantRepository.SaveAllAsync();
        
        // Cache Clear Event
        await _publisher.Publish(new DeleteCacheKeysEvent
        {
            Keys = new List<string>
            {
                new CacheKey(CacheKeyConstants.TenantById, tenant.AliasId.ToString()).Key
            }
        });
    }
}
