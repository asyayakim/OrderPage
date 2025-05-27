using ApplicationLayer;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly PlaceOrderHandler _handler;

    public OrderController(PlaceOrderHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderCommand command)
    {
        var orderId = await _handler.Handle(command);
        return Ok(new { OrderId = orderId });
    }
}