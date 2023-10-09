namespace Commerce.Domain.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public bool Deleted { get; set; }
}
