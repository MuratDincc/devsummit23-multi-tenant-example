namespace BFF.Api.Services.Commerce.Models.Response;

public record GetProductsServiceResponse
{
    public List<GetProductServiceResponse> Products { get; init; }
}