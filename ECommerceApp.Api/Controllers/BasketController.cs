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
}