using ECommerceApp.ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Order.Infrastructure.Repositories;

namespace ECommerceApp.Api.Controllers;
[ApiController]

[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductImporter _productImporter;
    private readonly IProductService _productService;
    private readonly IStoreService _storeService;

    public ProductsController(ProductImporter productImporter, IProductService productService, IStoreService storeService)
    {
        _productImporter = productImporter;
        _productService = productService;
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
        var products = await _productService.GetProductsFromDb(pageNumber, pageSize);
        if (products.Count == 0)
            return NotFound("No products found.");
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _productService.GetProductById(id);
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
        var product = await _productService.GetOneAsync();
        return Ok(product);
    }
    
    
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _storeService.GetCategoriesAsync();
        if (categories == null)
            return NotFound("No products found.");
        return Ok(categories);
    }

    [HttpGet("products-by-category")]
    public async Task<IActionResult> GetProductsByCategory([FromQuery] string? category,int pageNumber = 1, int pageSize = 1, decimal? minPrice = null, decimal? maxPrice = null, string sortBy= null)
    {
        var products = await _storeService.GetProductsByCategory(category, pageNumber, pageSize, minPrice, maxPrice, sortBy);
        if (products ==  null)
            return NotFound("No products found.");
        return Ok(products);
    }

    [HttpGet("recent")]
    public async Task<IActionResult> GetFeaturedProducts([FromQuery]int limit)
    {
        var products = await _storeService.GetFeaturedProducts(limit);
        if (products ==  null)
            return NotFound("No products found .");
        return Ok(products);
    }

    [HttpGet("categories-with-total-products")]
    public async Task<IActionResult> GetCategoriesTotalProducts()
    {
        var products = await _storeService.GetCategoriesWithTotalProductsAsync();
        if (products ==  null)
            return NotFound("No products found .");
        return Ok(products);
    }
}