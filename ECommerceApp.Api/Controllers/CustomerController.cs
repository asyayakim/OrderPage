using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repository;
    private readonly UserManager<UserData> _userManager;

    public CustomerController(ICustomerRepository repository, UserManager<UserData> userManager)
    {
        _repository = repository;
        _userManager = userManager;
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
    [HttpPost("add-data")]
    public async Task<IActionResult> AddDataCustomer([FromBody] CustomerDto dto)
    {
        var customer = await _userManager.FindByIdAsync(dto.UserId.ToString());
        if (customer == null)
            return NotFound(new { Message = "Customer not found" });
        _ = await _repository.AddDataToUser(customer, dto);
    
        return Ok(new { Message = "Customer added successfully" });
    }
}