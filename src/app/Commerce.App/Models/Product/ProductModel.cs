namespace Commerce.App.Models.Product;

public record ProductModel
{
    public int Id { get; init; }
    public string Title { get; init; }
    public decimal Price { get; init; }
    public string Image { get; init; }
}