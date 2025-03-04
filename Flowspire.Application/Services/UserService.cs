using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;
public class UserService(UserManager<User> userManager, 
    SignInManager<User> signInManager, 
    IConfiguration configuration, 
    RoleManager<IdentityRole> roleManager,
    IRefreshTokenRepository refreshTokenRepository) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public async Task<UserDTO> RegisterUserAsync(string email, string fullName, string password, UserRole role, string requestingUserId = null)
    {
        if (requestingUserId != null)
        {
            var requestingUser = await _userManager.FindByIdAsync(requestingUserId);
            if (requestingUser == null || !await _userManager.IsInRoleAsync(requestingUser, "Administrator"))
                throw new UnauthorizedAccessException("Apenas administradores podem registrar usuários com roles específicos.");
        }
        else if (role != UserRole.Customer)
        {
            throw new UnauthorizedAccessException("Somente administradores podem criar usuários com roles diferentes de Customer.");
        }

        var user = User.Create(email, fullName, password);
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new Exception("Erro ao registrar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));

        string roleName = role.ToString();
        if (!await _roleManager.RoleExistsAsync(roleName))
            await _roleManager.CreateAsync(new IdentityRole(roleName));

        await _userManager.AddToRoleAsync(user, roleName);

        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = role
        };
    }

    public async Task<(string AccessToken, string RefreshToken)> LoginUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new Exception("Usuário não encontrado.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
            throw new Exception("Login falhou.");

        var accessToken = GenerateJwtToken(user);
        var refreshToken = RefreshToken.Create(user.Id);
        await _refreshTokenRepository.AddAsync(refreshToken);

        return (accessToken, refreshToken.Token);
    }

    public async Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if (token == null || token.IsRevoked || token.Expires < DateTime.UtcNow)
            throw new Exception("Refresh token inválido ou expirado.");

        var user = await _userManager.FindByIdAsync(token.UserId);
        if (user == null)
            throw new Exception("Usuário não encontrado.");

        var newAccessToken = GenerateJwtToken(user);
        token.Revoke();
        await _refreshTokenRepository.UpdateAsync(token);

        var newRefreshToken = RefreshToken.Create(user.Id);
        await _refreshTokenRepository.AddAsync(newRefreshToken);

        return (newAccessToken, newRefreshToken.Token);
    }

    public async Task<UserDTO> UpdateUserAsync(string userId, string fullName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new Exception("Usuário não encontrado.");

        user.UpdateFullName(fullName);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            throw new Exception("Erro ao atualizar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));

        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = Enum.Parse<UserRole>(_userManager.GetRolesAsync(user).Result.First())
        };
    }

    public async Task<UserDTO> GetCurrentUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new Exception("Usuário não encontrado.");

        var roles = await _userManager.GetRolesAsync(user);
        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = Enum.Parse<UserRole>(roles.First())
        };
    }

    private string GenerateJwtToken(User user)
    {
        var roles = _userManager.GetRolesAsync(user).Result;
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