using BFF.Api.Business.Product.Abstracts;
using BFF.Api.Business.Product.Dto;
using BFF.Api.Models.Product.Request;
using BFF.Api.Models.Product.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    #region Fields

    private readonly IProductBusiness _productBusiness;

    #endregion

    #region Ctor

    public ProductController(IProductBusiness productBusiness)
    {
        _productBusiness = productBusiness;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Get Product By Id
    /// </summary>
    /// <param name="id">Product Id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetProductResponse), 200)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _productBusiness.GetById(id);

        return Ok(new GetProductResponse
        {
            Id = response.Id,
            Title = response.Title,
            Image = response.Image,
            Price = response.Price
        });
    }

    /// <summary>
    /// Get Products
    /// </summary>
    /// <returns></returns>
    /// <param name="tenantId">Tenant Id</param>
    /// <param name="connectionString">Connection String</param>
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(GetProductsResponse), 200)]
    public async Task<IActionResult> Get([FromHeader(Name = "X-Tenant-Id")] int tenantId, [FromHeader(Name = "X-Connection-String")] string connectionString)
    {
        var response = await _productBusiness.GetProducts(tenantId, connectionString);

        return Ok(new GetProductsResponse
        {
            Products = response.Products.Select(x => new GetProductResponse
            {
                Id = x.Id,
                Title = x.Title,
                Image = x.Image,
                Price = x.Price
            }).ToList()
        });
    }

    /// <summary>
    /// Create Product
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateProductResponse), 200)]
    public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
    {
        var response = await _productBusiness.Create(new CreateProductDto
        {
            Title = request.Title,
            Price = request.Price,
            Image = request.Image
        });

        return Ok(new CreateProductResponse
        {
            Id = response.Id
        });
    }

    #endregion
}