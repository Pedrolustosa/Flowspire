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

namespace Flowspire.Application.Services;
public class UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager) : IUserService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<UserDTO> RegisterUserAsync(string email, string fullName, string password, UserRole role)
    {
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

    public async Task<string> LoginUserAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email)??throw new Exception("Usuário não encontrado.");
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
            throw new Exception("Login falhou.");

        var token = GenerateJwtToken(user);
        return token;
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

    private string GenerateJwtToken(User user)
    {
        var roles = _userManager.GetRolesAsync(user).Result;
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}