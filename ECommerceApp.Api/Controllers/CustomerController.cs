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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> RegisterCustomer([FromBody] CustomerDto dto)
    {
        var address = new Address(dto.Street, dto.ZipCode);
        var customer = new Customer(dto.Name, dto.Email, address);
        await _repository.AddAsync(customer);
        return Ok(customer.Id);
    }
}