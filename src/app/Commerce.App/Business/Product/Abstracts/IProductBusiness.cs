using Commerce.App.Business.Product.Dto;

namespace Commerce.App.Business.Product.Abstracts;

public interface IProductBusiness
{
    Task<GetProductsDto> GetProducts();
}