using ECommerceApp.ApplicationLayer.DTO;
using ECommerceApp.ApplicationLayer.Interfaces;
using ECommerceApp.ApplicationLayer.Services;
using ECommerceApp.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order.Infrastructure.Repositories;

namespace ECommerceApp.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<UserData> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly UserRepository _userRepository;

    public UserController(UserManager<UserData> userManager, IJwtTokenGenerator jwtTokenGenerator, UserRepository userRepository)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreateUserDto userDto)
    {
        var user = userDto.ToUserData();
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        var saveUser = await _userRepository.SaveToDbAsync(userDto);

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var user = await _userManager.FindByEmailAsync(email: login.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            return Unauthorized();
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);
        return Ok(new { Token = token });
    }
}