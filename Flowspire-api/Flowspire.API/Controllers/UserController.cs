using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Enums;
using System.Security.Claims;
using Flowspire.API.Models;

namespace Flowspire.API.Controllers
{
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

                var userDto = await _userService.RegisterUserAsync(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.Password,
                    request.PhoneNumber,
                    request.BirthDate,
                    request.Gender,
                    request.AddressLine1,
                    request.AddressLine2,
                    request.City,
                    request.State,
                    request.Country,
                    request.PostalCode,
                    request.Role,
                    requestingUserId);

                return Ok(new { Message = "User registered successfully", User = userDto });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("register-customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request)
        {
            try
            {
                var userDto = await _userService.RegisterUserAsync(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.Password,
                    request.PhoneNumber,
                    request.BirthDate,
                    request.Gender,
                    request.AddressLine1,
                    request.AddressLine2,
                    request.City,
                    request.State,
                    request.Country,
                    request.PostalCode,
                    UserRole.Customer);

                return Ok(new { Message = "User registered successfully", User = userDto });
            }
            catch (System.Exception ex)
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
                return Ok(new { Message = "Login successful", AccessToken = accessToken, RefreshToken = refreshToken });
            }
            catch (System.Exception ex)
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
            catch (System.Exception ex)
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

                var updatedUserDto = await _userService.UpdateUserAsync(
                    userId,
                    requestWrapper.Request.FirstName,
                    requestWrapper.Request.LastName,
                    requestWrapper.Request.BirthDate,
                    requestWrapper.Request.Gender,
                    requestWrapper.Request.AddressLine1,
                    requestWrapper.Request.AddressLine2,
                    requestWrapper.Request.City,
                    requestWrapper.Request.State,
                    requestWrapper.Request.Country,
                    requestWrapper.Request.PostalCode,
                    requestWrapper.Request.Roles);

                return Ok(new { Message = "User updated successfully", User = updatedUserDto });
            }
            catch (System.Exception ex)
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
            catch (System.Exception ex)
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
                return Ok(new { Message = $"Role {request.Role} assigned to user {request.UserId} successfully." });
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
                return Ok(new { Message = $"Role {request.Role} removed from user {request.UserId} successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("users/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            try
            {
                if (!System.Enum.TryParse<UserRole>(roleName, true, out var role))
                    return BadRequest(new { Error = "Invalid role." });

                var users = await _userService.GetUsersByRoleAsync(role);
                return Ok(users);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
