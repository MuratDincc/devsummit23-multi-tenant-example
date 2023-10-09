using Commerce.App.Business.Product.Abstracts;
using Commerce.App.Business.Product.Dto;
using Commerce.App.Context;
using Commerce.App.Services.Bff;

namespace Commerce.App.Business.Product;

public class ProductBusiness : IProductBusiness
{
    private readonly IBffService _bffService;
    private readonly IWorkContext _workContext;

    public ProductBusiness(IBffService bffService, IWorkContext workContext)
    {
        _bffService = bffService;
        _workContext = workContext;
    }
    
    public async Task<GetProductsDto> GetProducts()
    {
        var response = await _bffService.GetProducts(_workContext.TenantId, _workContext.ConnectionString);
        
        return new GetProductsDto
        {
            Products = response.Products.Select(x => new GetProductDto
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Image = x.Image
            }).ToList()
        };
    }
}