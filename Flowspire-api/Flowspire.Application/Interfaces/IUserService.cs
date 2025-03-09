using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;
public interface IUserService
{
    Task<UserDTO> RegisterUserAsync(string email, string fullName, string password, UserRole role, string requestingUserId = null);
    Task<(string AccessToken, string RefreshToken)> LoginUserAsync(string email, string password);
    Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string refreshToken);
    Task<UserDTO> GetCurrentUserAsync(string userId);
    Task<UserDTO> UpdateUserAsync(string userId, string fullName, List<UserRole> roles = null);
    Task AssignRoleAsync(string userId, UserRole role);
    Task RemoveRoleAsync(string userId, UserRole role);
    Task<List<UserDTO>> GetUsersByRoleAsync(UserRole role);
}