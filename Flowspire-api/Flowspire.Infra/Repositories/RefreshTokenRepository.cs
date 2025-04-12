using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;
using Microsoft.Data.Sqlite;

namespace Flowspire.Infra.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> AddAsync(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();
                return refreshToken;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error adding refresh token to the database.", ex);
            }
            catch (SqliteException ex)
            {
                throw new Exception("SQLite connection or operation error while adding refresh token.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while adding refresh token.", ex);
            }
        }

        public async Task<RefreshToken> GetByTokenAsync(string token)
        {
            try
            {
                return await _context.RefreshTokens
                    .FirstOrDefaultAsync(rt => rt.Token == token);
            }
            catch (SqliteException ex)
            {
                throw new Exception("SQLite connection or operation error while retrieving refresh token.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while retrieving refresh token.", ex);
            }
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshTokens.Update(refreshToken);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error updating refresh token in the database.", ex);
            }
            catch (SqliteException ex)
            {
                throw new Exception("SQLite connection or operation error while updating refresh token.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error while updating refresh token.", ex);
            }
        }
    }
}
