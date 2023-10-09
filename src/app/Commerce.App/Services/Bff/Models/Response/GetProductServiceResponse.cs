namespace Commerce.App.Services.Bff.Models.Response;

public record GetProductServiceResponse
{
    public int Id { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}