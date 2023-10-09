namespace BFF.Api.Services.Commerce.Models.Request;

public record CreateProductServiceRequest
{
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}