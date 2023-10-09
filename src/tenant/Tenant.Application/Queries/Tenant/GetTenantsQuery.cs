using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;
using Tenant.Application.Queries.Tenant.Dto;
using Tenant.Domain.Constants;

namespace Tenant.Application.Queries.Tenant;

public record GetTenantsQuery : IRequest<GetTenantsDto>;

public record GetTenantsQueryHandler : IRequestHandler<GetTenantsQuery, GetTenantsDto>
{
    private readonly IRepository<Entities.Tenant> _tenantRepository;
    private readonly IStaticCacheManager _staticCacheManager;

    public GetTenantsQueryHandler(IRepository<Entities.Tenant> tenantRepository, IStaticCacheManager staticCacheManager)
    {
        _tenantRepository = tenantRepository;
        _staticCacheManager = staticCacheManager;
    }

    public async Task<GetTenantsDto> Handle(GetTenantsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = _staticCacheManager.PrepareKey(new CacheKey(CacheKeyConstants.Tenants));

        return await _staticCacheManager.GetAsync(cacheKey, async () =>
        {
            var data = await _tenantRepository.Table.AsNoTracking()
                                                              .Where(x => !x.Deleted)
                                                              .Select(x => new GetTenantDto
                                                              {
                                                                  AliasId = x.AliasId,
                                                                  Title = x.Title,
                                                                  Slug = x.Slug
                                                              })
                                                              .ToListAsync();

            return new GetTenantsDto
            {
                Tenants = data
            };
        });
    }
}