using System.Security.Claims;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly UserManager<UserData> _userManager;
    private readonly IFavoriteManager _favoriteManager;

    public BasketController(UserManager<UserData> userManager, IFavoriteManager favoriteManager)
    {
        _userManager = userManager;
        _favoriteManager = favoriteManager;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var basket = await _favoriteManager.GetProductsFromBasketAsync();
        if (basket == null)
            return NotFound(new { Message = "Products not found" });
        return Ok(basket);
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> Post(Guid productId)
    {
        var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (await _userManager.FindByIdAsync(user) == null)
            return NotFound(new { Message = "user not found" });
        var product = await _favoriteManager.AddProductToBasketAsync(user, productId);
        if (product == null)
            return NotFound(new { Message = "product not found" });
        return Ok(product);
    }
}