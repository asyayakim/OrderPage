using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ECommerceApp.ApplicationLayer;
using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Infrastructure.Repositories;

namespace ECommerceApp.Api.Controllers;
[ApiController]

[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderRepository _repository;
    private readonly IPlaceOrderHandler _handler;

    public OrderController(PlaceOrderHandler handler, OrderRepository repository)
    {
        _handler = handler;
        _repository = repository;
    }

    [Authorize]
    [HttpPost("place")]
    public async Task<IActionResult> PlaceOrder([FromBody] CreateProductOrderDto command)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) 
                          ?? User.FindFirst(JwtRegisteredClaimNames.Sub);
    
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }
        var ageClaim = User.FindFirst("Age");
        if (ageClaim == null || !int.TryParse(ageClaim.Value, out int age))
        {
            return Unauthorized("Age claim missing or invalid.");
        }
        command.Age = age;

        command.CustomerId = userId; 
        var orderId = await _handler.Handle(command);
    
        return Ok(new { OrderId = orderId });
    }
    [HttpGet("customer")]
    public async Task<IActionResult> GetOrdersByCustomerId(Guid customerId)
    {
      var order = await _repository.GetOrdersByCustomerIdAsync(customerId);
        return order != null ? Ok(order) : NotFound();
    }
    [HttpGet("orderId")]
    public async Task<IActionResult> GetOrdersByOrderId(Guid productOrderId)
    {
        var order = await _repository.GetOrderByProductOrderIdAsync(productOrderId);
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