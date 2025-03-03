using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;
public interface IUserService
{
    Task<UserDTO> RegisterUserAsync(string email, string fullName, string password, UserRole role);
    Task<string> LoginUserAsync(string email, string password);
    Task<UserDTO> UpdateUserAsync(string userId, string fullName);
}