using ECommerceApp.ApplicationLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Order.Infrastructure.Repositories;

namespace ECommerceApp.Api.Controllers;
[ApiController]

[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductImporter _productImporter;
    private readonly ProductServer _productServer;

    public ProductsController(ProductImporter productImporter, ProductServer productServer)
    {
        _productImporter = productImporter;
        _productServer = productServer;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery]int pageNumber = 1, int pageSize = 10)
    {
        
        var products = await _productImporter.ImportProducts(pageNumber, pageSize);
        if (products.Count == 0)
            return NotFound("No products found.");
        return Ok(products);
    } 
    
    //[HttpGet("products-from-store/{storeId}")]
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
   
}