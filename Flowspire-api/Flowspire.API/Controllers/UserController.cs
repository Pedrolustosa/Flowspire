using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Enums;
using Flowspire.API.Common;
using Flowspire.Application.Common;
using Flowspire.API.Models.Users;

namespace Flowspire.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(
    IUserProfileService userProfileService,
    IAuthenticationService authenticationService,
    ILogger<UserController> logger) : ControllerBase
{
    private readonly IUserProfileService _userProfileService = userProfileService;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly ILogger<UserController> _logger = logger;

    [HttpPost("register")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var requestingUserId = User.GetUserId();
            var userDto = await _userProfileService.RegisterUserAsync(
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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var (accessToken, refreshToken) = await _authenticationService.LoginUserAsync(request.Email, request.Password);

            Response.Cookies.Append("X-Access-Token", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(8)
            });

            return new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }, _logger, this, SuccessMessages.LoginSuccessful);

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenRefreshRequest request)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var (accessToken, refreshToken) = await _authenticationService.RefreshTokenAsync(request.RefreshToken);

            return new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }, _logger, this, SuccessMessages.TokenRefreshed);

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UserUpdateRequest requestWrapper)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.GetUserId();
            var updatedUserDto = await _userProfileService.UpdateUserAsync(
                userId,
                requestWrapper.FirstName,
                requestWrapper.LastName,
                requestWrapper.BirthDate,
                requestWrapper.Gender,
                requestWrapper.AddressLine1,
                requestWrapper.AddressLine2,
                requestWrapper.City,
                requestWrapper.State,
                requestWrapper.Country,
                requestWrapper.PostalCode,
                requestWrapper.Roles);

            return updatedUserDto;
        }, _logger, this, SuccessMessages.UserUpdated);

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            var userId = User.GetUserId();
            var userDto = await _userProfileService.GetCurrentUserAsync(userId);
            return userDto;
        }, _logger, this, SuccessMessages.CurrentUserRetrieved);

    [HttpGet("users/{roleName}")]
    public async Task<IActionResult> GetUsersByRole(string roleName)
        => await ControllerHelper.ExecuteAsync(async () =>
        {
            if (!Enum.TryParse<UserRole>(roleName, true, out var role))
                throw new ArgumentException(ErrorMessages.RoleNotFound);

            var users = await _userProfileService.GetUsersByRoleAsync(role);
            return users;
        }, _logger, this, SuccessMessages.UsersByRoleRetrieved);
}