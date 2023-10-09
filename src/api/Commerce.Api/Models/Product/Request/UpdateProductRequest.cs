namespace Commerce.Api.Models.Product.Request;

public record UpdateProductRequest
{
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}
