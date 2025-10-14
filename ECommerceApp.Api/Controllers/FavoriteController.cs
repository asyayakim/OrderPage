using System.Security.Claims;
using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace ECommerceApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FavoriteController : ControllerBase
{
    private readonly UserManager<UserData> _userManager;
    private readonly IFavoriteManager _favoriteManager;

    public FavoriteController(UserManager<UserData> userManager, IFavoriteManager favoriteManager)
    {
        _userManager = userManager;
        _favoriteManager = favoriteManager;
    }

    [HttpGet("all")]
    public async Task<IActionResult> Get()
    {
        var favorites = await _favoriteManager.GetAllFavorites();
        if (favorites == null)
           return NotFound(new {Message = "favorites not found"});
        return Ok(favorites);
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> AddFavorite(Guid productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        if (await _userManager.FindByIdAsync(userId) == null)
            return NotFound(new {Message = "user not found"});
        try
        {
            var favorite = await _favoriteManager.AddFavorite(userId, productId);
            return Ok(favorite);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveFavorite(Guid productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        if (await _userManager.FindByIdAsync(userId) == null)
            return NotFound(new {Message = "user not found"});
        try
        {
            var favorite = await _favoriteManager.DeleteFavorite(userId, productId);
            return Ok(favorite);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}