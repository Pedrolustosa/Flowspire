using Flowspire.Application.DTOs;
using Flowspire.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowspire.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserDTO> RegisterUserAsync(
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
            string? requestingUserId = null);

        Task<UserDTO> GetCurrentUserAsync(string userId);

        Task<UserDTO> UpdateUserAsync(
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
            List<UserRole>? roles = null);

        Task AssignRoleAsync(string userId, UserRole role);
        Task RemoveRoleAsync(string userId, UserRole role);
        Task<List<UserDTO>> GetUsersByRoleAsync(UserRole role);
    }
}
