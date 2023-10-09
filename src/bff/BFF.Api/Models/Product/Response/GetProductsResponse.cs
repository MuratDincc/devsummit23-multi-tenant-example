namespace BFF.Api.Models.Product.Response;

public record GetProductsResponse
{
    public List<GetProductResponse> Products { get; init; }
}
