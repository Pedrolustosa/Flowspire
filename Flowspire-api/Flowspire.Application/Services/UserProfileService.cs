using Flowspire.Application.Common;
using Flowspire.Application.DTOs;
using Flowspire.Application.Factories;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Flowspire.Domain.Interfaces;
using Flowspire.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flowspire.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDtoFactory _dtoFactory;
        private readonly ILogger<UserProfileService> _logger;

        public UserProfileService(
            IUserRepository userRepository,
            IUserDtoFactory dtoFactory,
            ILogger<UserProfileService> logger)
        {
            _userRepository = userRepository;
            _dtoFactory = dtoFactory;
            _logger = logger;
        }

        public Task<UserDTO> RegisterUserAsync(
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
            => ServiceHelper.ExecuteAsync(async () =>
            {
                if (requestingUserId != null && role != UserRole.Customer)
                {
                    var requester = await _userRepository.FindByIdAsync(requestingUserId);
                    if (requester == null || !await _userRepository.IsInRoleAsync(requester, "Administrator"))
                        throw new UnauthorizedAccessException(ErrorMessages.OnlyAdminsCanAssignRoles);
                }

                if (await _userRepository.FindByEmailAsync(email) is not null)
                    throw new ArgumentException(ErrorMessages.EmailAlreadyRegistered);

                var nameVo = new FullName(firstName, lastName);
                var phoneVo = new PhoneNumber(phoneNumber);
                var addressVo = new Address(addressLine1, addressLine2, city, state, country, postalCode);

                var user = User.Create(email, password, nameVo, phoneVo, birthDate, gender, addressVo);

                var result = await _userRepository.CreateAsync(user, password);
                if (!result.Succeeded)
                    throw new Exception(ErrorMessages.ErrorRegisteringUser +
                        string.Join(", ", result.Errors.Select(e => e.Description)));

                await _userRepository.AddToRoleAsync(user, role.ToString());

                return _dtoFactory.Create(user, new List<UserRole> { role });
            }, _logger, nameof(RegisterUserAsync));

        public Task<UserDTO> GetCurrentUserAsync(string userId)
            => ServiceHelper.ExecuteAsync(async () =>
            {
                var user = await _userRepository.FindByIdAsync(userId)
                    ?? throw new KeyNotFoundException(ErrorMessages.UserNotFound);

                var roles = await _userRepository.GetRolesAsync(user);
                return _dtoFactory.Create(user, roles.Select(r => Enum.Parse<UserRole>(r)).ToList());
            }, _logger, nameof(GetCurrentUserAsync));

        public Task<UserDTO> UpdateUserAsync(
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
            => ServiceHelper.ExecuteAsync(async () =>
            {
                var user = await _userRepository.FindByIdAsync(userId)
                    ?? throw new KeyNotFoundException(ErrorMessages.UserNotFound);

                user.UpdateName(new FullName(firstName, lastName));
                user.UpdatePhoneNumber(new PhoneNumber(user.Phone.Value));
                user.UpdateAddress(new Address(addressLine1, addressLine2, city, state, country, postalCode));
                user.UpdateBirthDate(birthDate);
                user.UpdateGender(gender);

                if (roles != null && roles.Any())
                {
                    var current = await _userRepository.GetRolesAsync(user);
                    var toRemove = current.Except(roles.Select(r => r.ToString())).ToList();
                    var toAdd = roles.Select(r => r.ToString()).Except(current).ToList();

                    if (toRemove.Any())
                        await _userRepository.RemoveFromRolesAsync(user, toRemove);
                    if (toAdd.Any())
                        await _userRepository.AddToRolesAsync(user, toAdd);
                }

                var result = await _userRepository.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new Exception(ErrorMessages.ErrorUpdatingUser +
                        string.Join(", ", result.Errors.Select(e => e.Description)));

                var newRoles = await _userRepository.GetRolesAsync(user);
                return _dtoFactory.Create(user, newRoles.Select(r => Enum.Parse<UserRole>(r)).ToList());
            }, _logger, nameof(UpdateUserAsync));

        public Task AssignRoleAsync(string userId, UserRole role)
            => ServiceHelper.ExecuteAsync(async () =>
            {
                var user = await _userRepository.FindByIdAsync(userId)
                    ?? throw new KeyNotFoundException(ErrorMessages.UserNotFound);

                await _userRepository.AddToRoleAsync(user, role.ToString());
            }, _logger, nameof(AssignRoleAsync));

        public Task RemoveRoleAsync(string userId, UserRole role)
            => ServiceHelper.ExecuteAsync(async () =>
            {
                var user = await _userRepository.FindByIdAsync(userId)
                    ?? throw new KeyNotFoundException(ErrorMessages.UserNotFound);

                var roleName = role.ToString();
                if (!await _userRepository.IsInRoleAsync(user, roleName))
                    throw new InvalidOperationException(ErrorMessages.RoleNotAssigned);

                await _userRepository.RemoveFromRolesAsync(user, new[] { roleName });
            }, _logger, nameof(RemoveRoleAsync));

        public Task<List<UserDTO>> GetUsersByRoleAsync(UserRole role)
            => ServiceHelper.ExecuteAsync(async () =>
            {
                var users = await _userRepository.GetUsersInRoleAsync(role.ToString());
                return users.Select(u => _dtoFactory.Create(u, new List<UserRole> { role })).ToList();
            }, _logger, nameof(GetUsersByRoleAsync));
    }

}
