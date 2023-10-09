namespace Commerce.App.Services.Bff.Models.Response;

public record GetProductsServiceResponse
{
    public List<GetProductServiceResponse> Products { get; init; }
}