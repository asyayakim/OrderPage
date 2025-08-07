using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.ApplicationLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet("store-names")]
    public async Task<IActionResult> GetStoreNames()
    {
        var stores = await _storeService.GetAllStoreNamesAsync();
        if (stores.Count == 0)
            return NotFound("No stores found.");
        return Ok(stores);
    }
}
