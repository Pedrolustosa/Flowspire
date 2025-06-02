using Flowspire.Application.Common;
using Flowspire.Domain.Interfaces;
using Flowspire.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flowspire.Application.Interfaces;

namespace Flowspire.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IConfiguration configuration,
            ILogger<AuthenticationService> logger)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public Task<(string AccessToken, string RefreshToken)> LoginUserAsync(string email, string password)
            => ServiceHelper.ExecuteAsync(async () =>
            {
                var user = await _userRepository.FindByEmailAsync(email)
                    ?? throw new KeyNotFoundException(ErrorMessages.UserNotFound);

                if (!await _userRepository.CheckPasswordAsync(user, password))
                    throw new UnauthorizedAccessException(ErrorMessages.InvalidCredentials);

                var accessToken = await GenerateJwtAsync(user);
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

                var accessToken = await GenerateJwtAsync(user);
                return (accessToken, newRefresh.Token);
            }, _logger, nameof(RefreshTokenAsync));

        private async Task<string> GenerateJwtAsync(User user)
        {
            var roles = await _userRepository.GetRolesAsync(user);
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
    }
}
