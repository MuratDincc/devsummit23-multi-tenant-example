using System.Diagnostics;
using System.Security.AccessControl;
using Commerce.App.Business.Product.Abstracts;
using Commerce.App.Context;
using Microsoft.AspNetCore.Mvc;
using Commerce.App.Models;
using Commerce.App.Models.Product;

namespace Commerce.App.Controllers;

public class ProductController : Controller
{
    private readonly IProductBusiness _productBusiness;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductBusiness productBusiness, ILogger<ProductController> logger)
    {
        _productBusiness = productBusiness;
        _logger = logger;
    }
    
    public async Task<IActionResult> List()
    {
        var products = await _productBusiness.GetProducts();
        var model = products?.Products?.Select(x => new ProductModel
        {
            Id = x.Id,
            Title = x.Title,
            Price = x.Price,
            Image = x.Image
        }).ToList();
        
        return View(model);
    }
}