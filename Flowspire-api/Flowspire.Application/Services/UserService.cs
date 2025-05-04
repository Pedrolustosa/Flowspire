using Flowspire.Application.Common;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Flowspire.Domain.Interfaces;
using Flowspire.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Flowspire.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IConfiguration configuration,
            IRefreshTokenRepository refreshTokenRepository,
            ILogger<UserService> logger)
        {
            _userRepository           = userRepository           ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration            = configuration            ?? throw new ArgumentNullException(nameof(configuration));
            _refreshTokenRepository   = refreshTokenRepository   ?? throw new ArgumentNullException(nameof(refreshTokenRepository));
            _logger                   = logger                   ?? throw new ArgumentNullException(nameof(logger));
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
                // Só admins podem criar com papel diferente de Customer
                if (requestingUserId != null && role != UserRole.Customer)
                {
                    var requester = await _userRepository.FindByIdAsync(requestingUserId);
                    if (requester == null || !await _userRepository.IsInRoleAsync(requester, "Administrator"))
                        throw new UnauthorizedAccessException(ErrorMessages.OnlyAdminsCanAssignRoles);
                }

                // Verifica e-mail duplicado
                if (await _userRepository.FindByEmailAsync(email) is not null)
                    throw new ArgumentException(ErrorMessages.EmailAlreadyRegistered);

                // Cria VO e domínio
                var nameVo = new FullName(firstName, lastName);
                var phoneVo = new PhoneNumber(phoneNumber);
                var addressVo = new Address(addressLine1, addressLine2, city, state, country, postalCode);

                var user = User.Create(
                    email,
                    password,
                    nameVo,
                    phoneVo,
                    birthDate,
                    gender,
                    addressVo
                );

                var result = await _userRepository.CreateAsync(user, password);
                if (!result.Succeeded)
                    throw new Exception(
                        ErrorMessages.ErrorRegisteringUser +
                        string.Join(", ", result.Errors.Select(e => e.Description))
                    );

                await _userRepository.AddToRoleAsync(user, role.ToString());

                return MapToDTO(user, new List<UserRole> { role });
            }, _logger, nameof(RegisterUserAsync));

        public Task<(string AccessToken, string RefreshToken)> LoginUserAsync(string email, string password)
            => ServiceHelper.ExecuteAsync(async () =>
            {
                var user = await _userRepository.FindByEmailAsync(email)
                    ?? throw new KeyNotFoundException(ErrorMessages.UserNotFound);

                if (!await _userRepository.CheckPasswordAsync(user, password))
                    throw new UnauthorizedAccessException(ErrorMessages.InvalidCredentials);

                var accessToken = GenerateJwt(user);
                var refresh = RefreshToken.Create(user.Id);
                await _refreshTokenRepository.AddAsync(refresh);

                return (accessToken, refresh.Token);
            }, _logger, nameof(LoginUserAsync));

        public Task<(string AccessToken, string RefreshToken)> RefreshTokenAsync(string token)
            => ServiceHelper.ExecuteAsync(async () =>
            {
                var stored = await _refreshTokenRepository.GetByTokenAsync(token)
                    ?? throw new KeyNotFoundException(ErrorMessages.InvalidRefreshToken);
                if (stored.IsRevoked || stored.IsExpired())
                    throw new UnauthorizedAccessException(ErrorMessages.ExpiredRefreshToken);

                var user = await _userRepository.FindByIdAsync(stored.UserId)
                    ?? throw new KeyNotFoundException(ErrorMessages.UserNotFound);

                stored.Revoke();
                await _refreshTokenRepository.UpdateAsync(stored);

                var newRefresh = RefreshToken.Create(user.Id);
                await _refreshTokenRepository.AddAsync(newRefresh);

                return (GenerateJwt(user), newRefresh.Token);
            }, _logger, nameof(RefreshTokenAsync));

        public Task<UserDTO> GetCurrentUserAsync(string userId)
            => ServiceHelper.ExecuteAsync(async () =>
            {
                var user = await _userRepository.FindByIdAsync(userId)
                    ?? throw new KeyNotFoundException(ErrorMessages.UserNotFound);

                var roles = await _userRepository.GetRolesAsync(user);
                return MapToDTO(user, roles.Select(r => Enum.Parse<UserRole>(r)).ToList());
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

                // Atualiza VO
                user.UpdateName(new FullName(firstName, lastName));
                user.UpdatePhoneNumber(new PhoneNumber(user.Phone.Value)); // ou receber novo phoneNumber
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

                var res = await _userRepository.UpdateAsync(user);
                if (!res.Succeeded)
                    throw new Exception(
                        ErrorMessages.ErrorUpdatingUser +
                        string.Join(", ", res.Errors.Select(e => e.Description))
                    );

                var newRoles = await _userRepository.GetRolesAsync(user);
                return MapToDTO(user, newRoles.Select(r => Enum.Parse<UserRole>(r)).ToList());
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
                var list = await _userRepository.GetUsersInRoleAsync(role.ToString());
                return list.Select(u => MapToDTO(u, new List<UserRole> { role })).ToList();
            }, _logger, nameof(GetUsersByRoleAsync));

        private string GenerateJwt(User user)
        {
            var roles = _userRepository.GetRolesAsync(user).Result;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,   user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static UserDTO MapToDTO(User u, List<UserRole> roles) => new()
        {
            Id           = u.Id,
            Email        = u.Email,
            FirstName    = u.Name.FirstName,
            LastName     = u.Name.LastName, 
            PhoneNumber  = u.Phone.Value,
            BirthDate    = u.BirthDate,
            Gender       = u.Gender,
            AddressLine1 = u.Address.Line1,
            AddressLine2 = u.Address.Line2,
            City         = u.Address.City,
            State        = u.Address.State,
            Country      = u.Address.Country,
            PostalCode   = u.Address.PostalCode,
            Roles        = roles
        };
    }
}
