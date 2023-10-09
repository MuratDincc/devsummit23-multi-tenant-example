namespace Commerce.App.Business.Product.Dto;

public record GetProductDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}
