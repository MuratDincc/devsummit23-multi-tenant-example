using Commerce.Application.Queries.Product.Dto;
using Commerce.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;

namespace Commerce.Application.Queries.Product;

public record GetProductsQuery : IRequest<GetProductsDto>
{
    public int TenantId { get; init; }   
}

public record GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsDto>
{
    private readonly IRepository<Entities.Product> _productRepository;
    private readonly IStaticCacheManager _staticCacheManager;

    public GetProductsQueryHandler(IRepository<Entities.Product> productRepository, IStaticCacheManager staticCacheManager)
    {
        _productRepository = productRepository;
        _staticCacheManager = staticCacheManager;
    }

    public async Task<GetProductsDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = _staticCacheManager.PrepareKey(new CacheKey(CacheKeyConstants.Products), request.TenantId);

        return await _staticCacheManager.GetAsync(cacheKey, async () =>
        {
            var data = await _productRepository.Table.AsNoTracking()
                                                              .Where(x => !x.Deleted)
                                                              .Select(x => new GetProductDto
                                                              {
                                                                  Id = x.Id,
                                                                  Title = x.Title,
                                                                  Price = x.Price,
                                                                  Image = x.Image
                                                              })
                                                              .ToListAsync();

            return new GetProductsDto
            {
                Products = data
            };
        });
    }
}