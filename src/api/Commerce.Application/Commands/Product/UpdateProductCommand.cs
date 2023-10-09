using Commerce.Application.Events.Caching;
using Commerce.Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rubic.AspNetCore.Exceptions;
using Rubic.Caching;
using Rubic.EntityFramework.Repositories.Abstracts;

namespace Commerce.Application.Commands.Product;

public record UpdateProductCommand : IRequest
{
    public int Id { get; init; }
    public int TenantId { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}

public record UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IRepository<Entities.Product> _productRepository;
    private readonly IPublisher _publisher;

    public UpdateProductCommandHandler(IRepository<Entities.Product> productRepository, IPublisher publisher)
    {
        _productRepository = productRepository;
        _publisher = publisher;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Table.FirstOrDefaultAsync(x => x.Id == request.Id && !x.Deleted);
        if (product == null)
            throw new StatusException(status: StatusCode.NotFound, "Tenant Not Found!");

        product.Title = request.Title;
        product.Price = request.Price;
        product.Image = request.Image;

        await _productRepository.SaveAllAsync();
        
        // Cache Clear Event
        await _publisher.Publish(new DeleteCacheKeysEvent
        {
            Keys = new List<string>
            {
                new CacheKey(CacheKeyConstants.ProductById,  request.TenantId.ToString(), request.Id.ToString()).Key
            }
        });
    }
}
