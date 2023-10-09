namespace Commerce.Application.Queries.Product.Dto;

public record GetProductsDto
{
    public List<GetProductDto> Products { get; init; }
}
