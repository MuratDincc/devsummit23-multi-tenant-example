using Commerce.Application.Queries.Product.Dto;
using Commerce.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.AspNetCore.Exceptions;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;

namespace Commerce.Application.Queries.Product;

public record GetProductByIdQuery : IRequest<GetProductDto>
{
    public int Id { get; init; }
    public int TenantId { get; init; }
}

public record GetTenantByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductDto>
{
    private readonly IRepository<Entities.Product> _productRepository;
    private readonly IStaticCacheManager _staticCacheManager;

    public GetTenantByIdQueryHandler(IRepository<Entities.Product> productRepository, IStaticCacheManager staticCacheManager)
    {
        _productRepository = productRepository;
        _staticCacheManager = staticCacheManager;
    }

    public async Task<GetProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = _staticCacheManager.PrepareKey(new CacheKey(CacheKeyConstants.ProductById), request.TenantId, request.Id);

        return await _staticCacheManager.GetAsync(cacheKey, async () =>
        {
            var product = await _productRepository.Table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted);
            if (product == null)
                throw new StatusException(status: StatusCode.NotFound, "Product Not Found!");

            return new GetProductDto
            {
                Title = product.Title
            };
        });
    }
}
