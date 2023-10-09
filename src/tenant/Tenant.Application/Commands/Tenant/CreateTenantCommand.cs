using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.AspNetCore.Exceptions;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Application.Commands.Tenant.Dto;
using Tenant.Application.Events.Caching;
using Tenant.Domain.Constants;

namespace Tenant.Application.Commands.Tenant;

public record CreateTenantCommand : IRequest<CreateTenantResultDto>
{
    public int UserId  { get; init; }
    public string Slug { get; init; }
    public string Title { get; init; }
}

public record CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, CreateTenantResultDto>
{
    private readonly IRepository<Entities.Pool> _poolRepository;
    private readonly IRepository<Entities.Tenant> _tenantRepository;
    private readonly IRepository<Entities.TenantUser> _tenantUserRepository;
    private readonly IRepository<Entities.User> _userRepository;
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IPublisher _publisher;

    public CreateTenantCommandHandler(IRepository<Entities.Pool> poolRepository,
        IRepository<Entities.Tenant> tenantRepository,
        IRepository<Entities.TenantUser> tenantUserRepository,
        IRepository<Entities.User> userRepository,
        IStaticCacheManager staticCacheManager,
        IPublisher publisher)
    {
        _poolRepository = poolRepository;
        _tenantRepository = tenantRepository;
        _tenantUserRepository = tenantUserRepository;
        _userRepository = userRepository;
        _staticCacheManager = staticCacheManager;
        _publisher = publisher;
    }

    public async Task<CreateTenantResultDto> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.Slug.ToLower() == request.Slug.ToLower());
        if (tenant != null)
            throw new StatusException(status: StatusCode.BadRequest, "There is an app with the same name!");

        var user = await _userRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (user == null)
            throw new StatusException(status: StatusCode.BadRequest, "User Not Found!");

        var pool = await _poolRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => !x.Deleted);
        if (pool == null)
            throw new StatusException(status: StatusCode.BadRequest, "Pool Not Found!");
        
        var tenantEntity = new Entities.Tenant
        {
            AliasId = Guid.NewGuid(),
            Slug = request.Slug,
            Title = request.Title,
            CreatedOnUtc = DateTimeOffset.UtcNow,
            PoolDatabase = new Entities.PoolDatabase
            {
                PoolId = pool.Id,
                Name = $"app_db_{request.Slug}"
            }
        };

        await _tenantRepository.InsertAsync(tenantEntity);
        await _tenantRepository.SaveAllAsync();

        var tenantUserEntity = new Entities.TenantUser
        {
            TenantId = tenantEntity.Id,
            UserId = user.Id
        };
        
        await _tenantUserRepository.InsertAsync(tenantUserEntity);
        await _tenantUserRepository.SaveAllAsync();

        // Cache Clear Event
        await _publisher.Publish(new DeleteCacheKeysEvent
        {
            Keys = new List<string>
            {
                _staticCacheManager.PrepareKey(new CacheKey(CacheKeyConstants.UserById), request.UserId).Key
            }
        });
        
        return new CreateTenantResultDto
        {
            Id = tenantEntity.AliasId,
            Slug = request.Slug,
            DatabaseName = $"app_db_{request.Slug}",
            ConnectionString = $"Host={pool.Host}; Port={pool.Port}; Database={tenantEntity.PoolDatabase.Name}; Username={pool.Username}; Password={pool.Password}"
        };
    }
}