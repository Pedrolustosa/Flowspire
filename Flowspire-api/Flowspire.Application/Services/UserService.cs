using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;
using Flowspire.Application.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Flowspire.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IConfiguration configuration,
    IRefreshTokenRepository refreshTokenRepository,
    ILogger<UserService> logger
) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IConfiguration _configuration = configuration;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly ILogger<UserService> _logger = logger;

    public async Task<UserDTO> RegisterUserAsync(
    string email,
    string firstName,
    string lastName,
    string password,
    string phoneNumber,
    DateTime? birthDate,
    Gender gender,
    string? addressLine1,
    string? addressLine2,
    string? city,
    string? state,
    string? country,
    string? postalCode,
    UserRole role,
    string? requestingUserId = null)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            if (requestingUserId != null)
            {
                var requestingUser = await _userRepository.FindByIdAsync(requestingUserId)
                    .ThrowIfNull("Requesting user not found.");

                if (!await _userRepository.IsInRoleAsync(requestingUser, "Administrator"))
                    throw new UnauthorizedAccessException("Only administrators can register users with specific roles.");
            }
            else if (role != UserRole.Customer)
            {
                throw new UnauthorizedAccessException("Only administrators can create users with roles other than Customer.");
            }

            var existingUser = await _userRepository.FindByEmailAsync(email);
            if (existingUser != null)
                throw new ArgumentException("There is already a registration with this email.");

            var user = User.Create(
                email, password, firstName, lastName, phoneNumber,
                birthDate, gender, addressLine1, addressLine2, city, state, country, postalCode);

            var result = await _userRepository.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception("Error registering user: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userRepository.AddToRoleAsync(user, role.ToString());

            return MapToUserDTO(user, new List<UserRole> { role });
        }, _logger, nameof(RegisterUserAsync));
    }

    public async Task<(string AccessToken, string RefreshToken)> LoginUserAsync(string email, string password)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var user = await _userRepository.FindByEmailAsync(email)
                .ThrowIfNull("User not found.");

            var signInResult = await _userRepository.CheckPasswordAsync(user, password);
            if (!signInResult) throw new Exception("Login failed.");

            var accessToken = GenerateJwtToken(user);
            var refreshToken = RefreshToken.Create(user.Id);
            await _refreshTokenRepository.AddAsync(refreshToken);

            return (accessToken, refreshToken.Token);
        }, _logger, nameof(LoginUserAsync));
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (token == null || token.IsRevoked || token.IsExpired())
                throw new Exception("Invalid or expired refresh token.");

            var user = await _userRepository.FindByIdAsync(token.UserId)
                .ThrowIfNull("User not found.");

            var newAccessToken = GenerateJwtToken(user);

            token.Revoke();
            await _refreshTokenRepository.UpdateAsync(token);

            var newRefreshToken = RefreshToken.Create(user.Id);
            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return (newAccessToken, newRefreshToken.Token);
        }, _logger, nameof(RefreshTokenAsync));
    }

    public async Task<UserDTO> UpdateUserAsync(
        string userId,
        string firstName,
        string lastName,
        DateTime? birthDate,
        Gender gender,
        string? addressLine1,
        string? addressLine2,
        string? city,
        string? state,
        string? country,
        string? postalCode,
        List<UserRole>? roles = null)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID cannot be empty.");
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.");

            var user = await _userRepository.FindByIdAsync(userId)
                .ThrowIfNull("User not found.");

            user.UpdatePersonalInfo(firstName, lastName, birthDate, gender);
            user.UpdateAddress(addressLine1, addressLine2, city, state, country, postalCode);

            if (roles != null && roles.Any())
            {
                var currentRoles = await _userRepository.GetRolesAsync(user);
                var rolesToRemove = currentRoles.Except(roles.Select(r => r.ToString())).ToList();
                var rolesToAdd = roles.Select(r => r.ToString()).Except(currentRoles).ToList();

                if (rolesToRemove.Any())
                    await _userRepository.RemoveFromRolesAsync(user, rolesToRemove);
                if (rolesToAdd.Any())
                    await _userRepository.AddToRolesAsync(user, rolesToAdd);
            }

            var result = await _userRepository.UpdateAsync(user);
            if (!result.Succeeded)
                throw new Exception("Error updating user: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            var userRoles = await _userRepository.GetRolesAsync(user);
            var roleEnums = userRoles.Select(r => Enum.Parse<UserRole>(r)).ToList();

            return MapToUserDTO(user, roleEnums);
        }, _logger, nameof(UpdateUserAsync));
    }

    public async Task<UserDTO> GetCurrentUserAsync(string userId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var user = await _userRepository.FindByIdAsync(userId)
                .ThrowIfNull("User not found.");

            var roles = await _userRepository.GetRolesAsync(user);
            var roleEnums = roles.Select(r => Enum.Parse<UserRole>(r)).ToList();

            return MapToUserDTO(user, roleEnums);
        }, _logger, nameof(GetCurrentUserAsync));
    }

    public async Task AssignRoleAsync(string userId, UserRole role)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var user = await _userRepository.FindByIdAsync(userId)
                .ThrowIfNull("User not found.");

            await _userRepository.AddToRoleAsync(user, role.ToString());
        }, _logger, nameof(AssignRoleAsync));
    }

    public async Task RemoveRoleAsync(string userId, UserRole role)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var user = await _userRepository.FindByIdAsync(userId)
                .ThrowIfNull("User not found.");

            string roleName = role.ToString();
            if (!await _userRepository.IsInRoleAsync(user, roleName))
                throw new Exception("User does not have the specified role.");

            await _userRepository.RemoveFromRolesAsync(user, new List<string> { roleName });
        }, _logger, nameof(RemoveRoleAsync));
    }

    public async Task<List<UserDTO>> GetUsersByRoleAsync(UserRole role)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            string roleName = role.ToString();
            var users = await _userRepository.GetUsersInRoleAsync(roleName);

            return users.Select(user => MapToUserDTO(user, new List<UserRole> { role })).ToList();
        }, _logger, nameof(GetUsersByRoleAsync));
    }

    private string GenerateJwtToken(User user)
    {
        var roles = _userRepository.GetRolesAsync(user).Result;
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static UserDTO MapToUserDTO(User user, List<UserRole> roles)
    {
        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = user.BirthDate,
            Gender = user.Gender,
            AddressLine1 = user.AddressLine1,
            AddressLine2 = user.AddressLine2,
            City = user.City,
            State = user.State,
            Country = user.Country,
            PostalCode = user.PostalCode,
            Roles = roles
        };
    }
}
