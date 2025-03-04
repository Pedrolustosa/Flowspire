using Flowspire.API.Models;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
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
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var requestingUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userDto = await _userService.RegisterUserAsync(request.Email, request.FullName, request.Password, request.Role, requestingUserId);
        return Ok(new { Message = "Usuário registrado com sucesso", User = userDto });
    }

    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request)
    {
        var userDto = await _userService.RegisterUserAsync(request.Email, request.FullName, request.Password, UserRole.Customer);
        return Ok(new { Message = "Usuário registrado com sucesso", User = userDto });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var (accessToken, refreshToken) = await _userService.LoginUserAsync(request.Email, request.Password);
        return Ok(new { Message = "Login bem-sucedido", AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var (accessToken, refreshToken) = await _userService.RefreshTokenAsync(request.RefreshToken);
        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
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

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var userDto = await _userService.GetCurrentUserAsync(userId);
        return Ok(userDto);
    }
}