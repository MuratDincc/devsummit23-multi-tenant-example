namespace Commerce.App.Models.Product;

public record ProductListModel
{
    public List<ProductModel> Products { get; init; }
}