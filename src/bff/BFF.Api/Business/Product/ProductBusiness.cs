using BFF.Api.Business.Product.Abstracts;
using BFF.Api.Business.Product.Dto;
using BFF.Api.Context;
using BFF.Api.Services.Commerce;
using BFF.Api.Services.Commerce.Models.Request;

namespace BFF.Api.Business.Product;

public class ProductBusiness : IProductBusiness
{
    private readonly ICommerceService _commerceService;
    private readonly IWorkContext _workContext;

    public ProductBusiness(ICommerceService commerceService, IWorkContext workContext)
    {
        _commerceService = commerceService;
        _workContext = workContext;
    }

    public async Task<CreateProductResultDto> Create(CreateProductDto request)
    {
        var response = await _commerceService.CreateProduct(new CreateProductServiceRequest
        {
            Title = request.Title,
            Price = request.Price,
            Image = request.Image
        }, _workContext.TenantId, _workContext.ConnectionString);
        
        return new CreateProductResultDto
        {
            Id = response.Id
        };
    }

    public async Task<GetProductDto> GetById(int id)
    {
        var response = await _commerceService.GetProductById(id, _workContext.TenantId, _workContext.ConnectionString);
        if (response == null)
            throw new Exception("Product not found!");
        
        return new GetProductDto
        {
            Id = response.Id,
            Title = response.Title,
            Price = response.Price,
            Image = response.Image
        };
    }
    
    public async Task<GetProductsDto> GetProducts(int tenantId, string connectionString)
    {
        var response = await _commerceService.GetProducts(tenantId, connectionString);
        if (response == null)
            throw new Exception("Products not found!");
        
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