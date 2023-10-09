using Microsoft.AspNetCore.Mvc;
using Commerce.Api.Models.Product.Request;
using Commerce.Api.Models.Product.Response;
using Commerce.Application.Commands.Product;
using Commerce.Application.Queries.Product;
using Microsoft.AspNetCore.Authorization;

namespace Commerce.Api.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController : BaseController
{
    #region Methods

    /// <summary>
    /// Get Product By Id
    /// </summary>
    /// <param name="id">Product Id</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetProductResponse), 200)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var tenant = await Mediator.Send(new GetProductByIdQuery
        {
            Id = id,
            TenantId = WorkContext.TenantId
        });

        return Ok(new GetProductResponse
        {
            Id = id,
            Title = tenant.Title
        });
    }

    /// <summary>
    /// Get Products
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(GetProductsResponse), 200)]
    public async Task<IActionResult> Get()
    {
        var data = await Mediator.Send(new GetProductsQuery
        {
            TenantId = WorkContext.TenantId
        });

        return Ok(new GetProductsResponse
        {
            Products = data.Products.Select(x => new GetProductResponse
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Image = x.Image
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
        var result = await Mediator.Send(new CreateProductCommand
        {
            TenantId = WorkContext.TenantId,
            Title = request.Title,
            Price = request.Price,
            Image = request.Image
        });

        return Ok(new CreateProductResponse
        {
            Id = result.Id
        });
    }

    /// <summary>
    /// Update Product
    /// </summary>
    /// <param name="id">Product Id</param>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateProductRequest request)
    {
        await Mediator.Send(new UpdateProductCommand
        {
            Id = id,
            TenantId = WorkContext.TenantId,
            Title = request.Title,
            Price = request.Price,
            Image = request.Image
        });

        return Ok();
    }

    #endregion
}
