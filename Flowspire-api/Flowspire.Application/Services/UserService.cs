using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Flowspire.Application.Services;

public class UserService(IUserRepository userRepository,
                   IConfiguration configuration,
                   IRefreshTokenRepository refreshTokenRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IConfiguration _configuration = configuration;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository ?? throw new ArgumentNullException(nameof(refreshTokenRepository));

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
        string requestingUserId = null)
    {
        try
        {
            if (requestingUserId != null)
            {
                var requestingUser = await _userRepository.FindByIdAsync(requestingUserId);
                if (requestingUser == null || !await _userRepository.IsInRoleAsync(requestingUser, "Administrator"))
                    throw new UnauthorizedAccessException("Only administrators can register users with specific roles.");
            }
            else if (role != UserRole.Customer)
            {
                throw new UnauthorizedAccessException("Only administrators can create users with roles other than Customer.");
            }

            var user = User.Create(
                email, password, firstName, lastName, phoneNumber,
                birthDate, gender, addressLine1, addressLine2, city, state, country, postalCode);

            var result = await _userRepository.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception("Error registering user: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userRepository.AddToRoleAsync(user, role.ToString());

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
                Roles = new List<UserRole> { role }
            };
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error saving user to the database.", ex);
        }
        catch (UnauthorizedAccessException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while registering user.", ex);
        }
    }

    public async Task<(string AccessToken, string RefreshToken)> LoginUserAsync(string email, string password)
    {
        try
        {
            var user = await _userRepository.FindByEmailAsync(email) ?? throw new Exception("User not found.");
            var signInResult = await _userRepository.CheckPasswordAsync(user, password);
            if (!signInResult) throw new Exception("Login failed.");

            var accessToken = GenerateJwtToken(user);
            var refreshToken = RefreshToken.Create(user.Id);
            await _refreshTokenRepository.AddAsync(refreshToken);

            return (accessToken, refreshToken.Token);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error saving refresh token to the database.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error during login.", ex);
        }
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (token == null || token.IsRevoked || token.IsExpired())
                throw new Exception("Invalid or expired refresh token.");

            var user = await _userRepository.FindByIdAsync(token.UserId);
            if (user == null)
                throw new Exception("User not found.");

            var newAccessToken = GenerateJwtToken(user);
            token.Revoke();
            await _refreshTokenRepository.UpdateAsync(token);

            var newRefreshToken = RefreshToken.Create(user.Id);
            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return (newAccessToken, newRefreshToken.Token);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error updating or saving refresh tokens to the database.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while refreshing token.", ex);
        }
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
        List<UserRole> roles = null)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID cannot be empty.");
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.");

            var user = await _userRepository.FindByIdAsync(userId) ?? throw new Exception("User not found.");

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
                Roles = roleEnums
            };
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error updating user in the database.", ex);
        }
        catch (ArgumentException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while updating user.", ex);
        }
    }

    public async Task<UserDTO> GetCurrentUserAsync(string userId)
    {
        try
        {
            var user = await _userRepository.FindByIdAsync(userId) ?? throw new Exception("User not found.");
            var roles = await _userRepository.GetRolesAsync(user);
            var roleEnums = roles.Select(r => Enum.Parse<UserRole>(r)).ToList();

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
                Roles = roleEnums
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error retrieving current user.", ex);
        }
    }

    public async Task AssignRoleAsync(string userId, UserRole role)
    {
        try
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            await _userRepository.AddToRoleAsync(user, role.ToString());
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error saving role assignment to the database.", ex);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while assigning role.", ex);
        }
    }

    public async Task RemoveRoleAsync(string userId, UserRole role)
    {
        try
        {
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            string roleName = role.ToString();
            if (!await _userRepository.IsInRoleAsync(user, roleName))
                throw new Exception("User does not have the specified role.");

            await _userRepository.RemoveFromRolesAsync(user, new List<string> { roleName });
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error saving role removal to the database.", ex);
        }
        catch (KeyNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while removing role.", ex);
        }
    }

    public async Task<List<UserDTO>> GetUsersByRoleAsync(UserRole role)
    {
        try
        {
            string roleName = role.ToString();
            var users = await _userRepository.GetUsersInRoleAsync(roleName);
            return users.Select(user => new UserDTO
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
                Roles = new List<UserRole> { role }
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Unexpected error while retrieving users by role.", ex);
        }
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
}
