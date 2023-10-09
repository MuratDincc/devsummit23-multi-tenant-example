namespace BFF.Api.Business.Product.Dto;

public record GetProductsDto
{
    public List<GetProductDto> Products { get; init; }
}
