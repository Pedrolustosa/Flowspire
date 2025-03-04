using Flowspire.Domain.Entities;

namespace Flowspire.Domain.Interfaces;
public interface IRefreshTokenRepository
{
    Task<RefreshToken> AddAsync(RefreshToken refreshToken);
    Task<RefreshToken> GetByTokenAsync(string token);
    Task UpdateAsync(RefreshToken refreshToken);
}