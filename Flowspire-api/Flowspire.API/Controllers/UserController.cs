using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Enums;
using Flowspire.API.Models;
using Flowspire.API.Common;
using Flowspire.Application.Common;

namespace Flowspire.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly ILogger<UserController> _logger = logger;

        [HttpPost("register")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
            => await ControllerHelper.ExecuteAsync(async () =>
            {
                var requestingUserId = User.GetUserId();
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

                return userDto;
            }, _logger, this, SuccessMessages.UserRegistered);

        [HttpPost("register-customer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerRequest request)
            => await ControllerHelper.ExecuteAsync(async () =>
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

                return userDto;
            }, _logger, this, SuccessMessages.CustomerRegistered);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
            => await ControllerHelper.ExecuteAsync(async () =>
            {
                var (accessToken, refreshToken) = await _userService.LoginUserAsync(request.Email, request.Password);

                Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(8)
                });

                var tokenResponse = new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                return tokenResponse;
            }, _logger, this, SuccessMessages.LoginSuccessful);

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
            => await ControllerHelper.ExecuteAsync(async () =>
            {
                var (accessToken, refreshToken) = await _userService.RefreshTokenAsync(request.RefreshToken);

                var tokenResponse = new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                return tokenResponse;
            }, _logger, this, SuccessMessages.TokenRefreshed);

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateRequestWrapper requestWrapper)
            => await ControllerHelper.ExecuteAsync(async () =>
            {
                var userId = User.GetUserId();
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

                return updatedUserDto;
            }, _logger, this, SuccessMessages.UserUpdated);

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
            => await ControllerHelper.ExecuteAsync(async () =>
            {
                var userId = User.GetUserId();
                var userDto = await _userService.GetCurrentUserAsync(userId);
                return userDto;
            }, _logger, this, SuccessMessages.CurrentUserRetrieved);

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
            => await ControllerHelper.ExecuteAsync(async () =>
            {
                await _userService.AssignRoleAsync(request.UserId, request.Role);
            }, _logger, this, SuccessMessages.RoleAssigned);

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRole([FromBody] RemoveRoleRequest request)
            => await ControllerHelper.ExecuteAsync(async () =>
            {
                await _userService.RemoveRoleAsync(request.UserId, request.Role);
            }, _logger, this, SuccessMessages.RoleRemoved);

        [HttpGet("users/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
            => await ControllerHelper.ExecuteAsync(async () =>
            {
                if (!Enum.TryParse<UserRole>(roleName, true, out var role))
                    throw new ArgumentException(ErrorMessages.RoleNotFound);

                var users = await _userService.GetUsersByRoleAsync(role);
                return users;
            }, _logger, this, SuccessMessages.UsersByRoleRetrieved);
    }
}
