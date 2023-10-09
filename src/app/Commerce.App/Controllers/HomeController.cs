using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Commerce.App.Models;

namespace Commerce.App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult TenantNotFound()
    {
        return View();
    }
}