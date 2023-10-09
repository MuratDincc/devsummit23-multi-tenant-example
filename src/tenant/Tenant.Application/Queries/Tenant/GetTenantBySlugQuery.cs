using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.AspNetCore.Exceptions;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Application.Queries.Tenant.Dto;
using Tenant.Domain.Constants;

namespace Tenant.Application.Queries.Tenant;

public record GetTenantBySlugQuery : IRequest<GetTenantDto>
{
    public string Slug { get; init; }
}

public record GetTenantBySlugQueryHandler : IRequestHandler<GetTenantBySlugQuery, GetTenantDto>
{
    private readonly IRepository<Entities.Tenant> _tenantRepository;
    private readonly IStaticCacheManager _staticCacheManager;

    public GetTenantBySlugQueryHandler(IRepository<Entities.Tenant> tenantRepository, IStaticCacheManager staticCacheManager)
    {
        _tenantRepository = tenantRepository;
        _staticCacheManager = staticCacheManager;
    }

    public async Task<GetTenantDto> Handle(GetTenantBySlugQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = _staticCacheManager.PrepareKey(new CacheKey(CacheKeyConstants.TenantBySlug), request.Slug);

        return await _staticCacheManager.GetAsync(cacheKey, async () =>
        {
            var tenant = await _tenantRepository.Table.AsNoTracking()
                .Include(x => x.PoolDatabase)
                .ThenInclude(x => x.Pool)
                .FirstOrDefaultAsync(x => x.Slug == request.Slug && !x.Deleted);
            
            if (tenant == null)
                throw new StatusException(status: StatusCode.NotFound, "Tenant Not Found!");

            return new GetTenantDto
            {
                Id = tenant.Id,
                AliasId = tenant.AliasId,
                Title = tenant.Title,
                Slug = tenant.Slug,
                ConnectionString = $"Host={tenant.PoolDatabase.Pool.Host}; Port={tenant.PoolDatabase.Pool.Port}; Database={tenant.PoolDatabase.Name}; Username={tenant.PoolDatabase.Pool.Username}; Password={tenant.PoolDatabase.Pool.Password}"
            };
        });
    }
}
