// ... (usings existentes)
using Flowspire.API.Models;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Flowspire.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var userDto = await _userService.RegisterUserAsync(request.Email, request.FullName, request.Password, request.Role);
        return Ok(new { Message = "Usuário registrado com sucesso", User = userDto });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _userService.LoginUserAsync(request.Email, request.Password);
        return Ok(new { Message = "Login bem-sucedido", Token = token });
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var updatedUserDto = await _userService.UpdateUserAsync(userId, request.FullName);
        return Ok(new { Message = "Usuário atualizado com sucesso", User = updatedUserDto });
    }
}