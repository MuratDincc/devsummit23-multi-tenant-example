namespace BFF.Api.Models.Product.Request;

public record CreateProductRequest
{
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}
