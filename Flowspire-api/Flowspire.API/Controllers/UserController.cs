using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Enums;
using System.Security.Claims;
using Flowspire.API.Models;

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
        try
        {
            var requestingUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userDto = await _userService.RegisterUserAsync(request.Email, request.FullName, request.Password, request.Role, requestingUserId);
            return Ok(new { Message = "Usuário registrado com sucesso", User = userDto });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request)
    {
        try
        {
            var userDto = await _userService.RegisterUserAsync(request.Email, request.FullName, request.Password, UserRole.Customer);
            return Ok(new { Message = "Usuário registrado com sucesso", User = userDto });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var (accessToken, refreshToken) = await _userService.LoginUserAsync(request.Email, request.Password);
            return Ok(new { Message = "Login bem-sucedido", AccessToken = accessToken, RefreshToken = refreshToken });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var (accessToken, refreshToken) = await _userService.RefreshTokenAsync(request.RefreshToken);
            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateRequestWrapper requestWrapper)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var updatedUserDto = await _userService.UpdateUserAsync(userId, requestWrapper.requestWrapper.FullName, requestWrapper.requestWrapper.Roles);
            return Ok(new { Message = "Usuário atualizado com sucesso", User = updatedUserDto });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var userDto = await _userService.GetCurrentUserAsync(userId);
            return Ok(userDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
    {
        try
        {
            await _userService.AssignRoleAsync(request.UserId, request.Role);
            return Ok(new { Message = $"Role {request.Role} atribuído ao usuário {request.UserId} com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleRequest request)
    {
        try
        {
            await _userService.RemoveRoleAsync(request.UserId, request.Role);
            return Ok(new { Message = $"Role {request.Role} removido do usuário {request.UserId} com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("users/{roleName}")]
    public async Task<IActionResult> GetUsersByRole(string roleName)
    {
        try
        {
            if (!Enum.TryParse<UserRole>(roleName, true, out var role))
                return BadRequest(new { Error = "Role inválido." });

            var users = await _userService.GetUsersByRoleAsync(role);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}