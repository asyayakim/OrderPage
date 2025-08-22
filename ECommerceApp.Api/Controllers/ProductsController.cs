using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Order.Infrastructure.Repositories;

namespace ECommerceApp.Api.Controllers;
[ApiController]

[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductImporter _productImporter;
    private readonly ProductServer _productServer;
    private readonly IStoreService _storeService;

    public ProductsController(ProductImporter productImporter, ProductServer productServer, IStoreService storeService)
    {
        _productImporter = productImporter;
        _productServer = productServer;
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery]int pageNumber = 1, int pageSize = 10)
    {
        
        var products = await _productImporter.ImportProducts(pageNumber, pageSize);
        if (products.Count == 0)
            return NotFound("No products found.");
        return Ok(products);
    } 
    
   
    [HttpGet("products-frontend")]
    public async Task<IActionResult> GetAllProductsForFrontend([FromQuery]int pageNumber = 1, int pageSize = 10)
    {
        var products = await _productServer.GetProductsFromDb(pageNumber, pageSize);
        if (products.Count == 0)
            return NotFound("No products found.");
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _productServer.GetProductById(id);
        if (product == null)
            return NotFound("No products found.");
        return Ok(product);
    }

    [HttpGet("top-sellers")]
    public async Task<IActionResult> GetTopSellers()
    {
        var topProducts = await _storeService.GetTopSellersAsync();
        if (topProducts == null)
            return NotFound("No products found.");
        return Ok(topProducts);
    }
    
    //final logic is not implemented
    [HttpGet("product-of-the-week")]
    public async Task<IActionResult> GetProductOfTheWeek()
    {
        var product = await _productServer.GetOneAsync();
        return Ok(product);
    }
}