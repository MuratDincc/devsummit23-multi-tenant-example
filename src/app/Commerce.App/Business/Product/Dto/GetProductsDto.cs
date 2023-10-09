namespace Commerce.App.Business.Product.Dto;

public record GetProductsDto
{
    public List<GetProductDto> Products { get; init; }
}
