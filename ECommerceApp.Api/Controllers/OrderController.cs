using ApplicationLayer;
using ECommerceApp.Domain;
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

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
    {
        var orderId = await _handler.Handle(command);
        return Ok(new { OrderId = orderId });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
      var order = await _repository.GetByOrderAsync(id);
        return order != null ? Ok(order) : NotFound();
    }
}