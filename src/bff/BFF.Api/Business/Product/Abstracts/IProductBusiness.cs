using BFF.Api.Business.Product.Dto;
using BFF.Api.Business.User.Dto;

namespace BFF.Api.Business.Product.Abstracts;

public interface IProductBusiness
{
    Task<CreateProductResultDto> Create(CreateProductDto request);
    Task<GetProductDto> GetById(int id);
    Task<GetProductsDto> GetProducts(int tenantId, string connectionString);
}