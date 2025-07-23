using Microsoft.AspNetCore.Mvc;
using Order.Infrastructure.Repositories;

namespace ECommerceApp.Api.Controllers;
[ApiController]

[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductImporter _productImporter;

    public ProductsController(ProductImporter productImporter)
    {
        _productImporter = productImporter;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts([FromQuery]int pageNumber = 1, int pageSize = 10)
    {
        
        var products = await _productImporter.ImportProducts(pageNumber, pageSize);
        if (products.Count == 0)
            return NotFound("No products found.");
        return Ok(products);
    } 
}