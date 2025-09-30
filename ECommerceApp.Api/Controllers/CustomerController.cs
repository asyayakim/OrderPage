using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
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
        var customer = new Customer(dto.Name, dto.Email, dto.Birthday);
        await _repository.AddAsync(customer);
        return Ok(customer.Id);
    }
}