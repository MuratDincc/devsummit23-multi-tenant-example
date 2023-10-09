namespace Commerce.Api.Models.Product.Response;

public record GetProductResponse
{
    public int Id { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}
