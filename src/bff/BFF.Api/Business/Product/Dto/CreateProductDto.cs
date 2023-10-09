namespace BFF.Api.Business.Product.Dto;

public record CreateProductDto
{
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}