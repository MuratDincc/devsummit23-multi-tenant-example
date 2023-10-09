using Commerce.Application.Commands.Product.Dto;
using Commerce.Application.Events.Caching;
using Commerce.Domain.Constants;
using MediatR;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;

namespace Commerce.Application.Commands.Product;

public record CreateProductCommand : IRequest<CreateProductResultDto>
{
    public int TenantId { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}

public record CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResultDto>
{
    private readonly IRepository<Entities.Product> _productRepository;
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(IRepository<Entities.Product> productRepository, IStaticCacheManager staticCacheManager, IPublisher publisher)
    {
        _productRepository = productRepository;
        _staticCacheManager = staticCacheManager;
        _publisher = publisher;
    }

    public async Task<CreateProductResultDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Entities.Product
        {
            Title = request.Title,
            Price = request.Price,
            Image = request.Image
        };

        await _productRepository.InsertAsync(entity);
        await _productRepository.SaveAllAsync();
        
        // Cache Clear Event
        await _publisher.Publish(new DeleteCacheKeysEvent
        {
            Keys = new List<string>
            {
                _staticCacheManager.PrepareKey(new CacheKey(CacheKeyConstants.Products), request.TenantId.ToString()).Key
            }
        });
        
        return new CreateProductResultDto
        {
            Id = entity.Id
        };
    }
}