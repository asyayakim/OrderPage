using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ECommerceApp.ApplicationLayer;
using ECommerceApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Infrastructure.Repositories;

namespace ECommerceApp.Api.Controllers;
[ApiController]

[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly PlaceOrderHandler _handler;
    private readonly OrderRepository _repository;

    public OrderController(PlaceOrderHandler handler, OrderRepository repository)
    {
        _handler = handler;
        _repository = repository;
    }

    [Authorize]
    [HttpPost("place")]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) 
                          ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
    
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        command.CustomerId = userId; 
        var orderId = await _handler.Handle(command);
    
        return Ok(new { OrderId = orderId });
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid customerId)
    {
      var order = await _repository.GetByOrderAsync(customerId);
        return order != null ? Ok(order) : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _repository.GetAllAsync();
        return Ok(products);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveOrder(Guid id)
    {
        var order = await _repository.RemoveOrderAsync(id);
        return order != null ? Ok(order) : NotFound();
    }
}