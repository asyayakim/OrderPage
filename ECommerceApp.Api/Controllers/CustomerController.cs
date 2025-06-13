using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repository;

    public CustomerController(ICustomerRepository repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _repository.GetAllCustomers();
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetCustomerById([FromQuery] Guid id)
    {
        var result = await _repository.GetByIdAsync(id);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> RegisterCustomer([FromBody] CustomerDto dto)
    {
        var customer = new Customer(dto.Name, dto.Email, new Address(dto.Street, dto.City, dto.ZipCode));
        await _repository.AddAsync(customer);
        return Ok(customer.Id);
    }
}