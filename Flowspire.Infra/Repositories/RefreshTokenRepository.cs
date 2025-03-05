using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;
using Microsoft.Data.Sqlite;

namespace Flowspire.Infra.Repositories;
public class RefreshTokenRepository(ApplicationDbContext context) : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context = context;

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
            throw new Exception("Erro ao adicionar o refresh token ao banco de dados.", ex);
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao adicionar refresh token.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar refresh token.", ex);
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
            throw new Exception("Erro de conexão ou operação no SQLite ao recuperar refresh token.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar refresh token.", ex);
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
            throw new Exception("Erro ao atualizar o refresh token no banco de dados.", ex);
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao atualizar refresh token.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao atualizar refresh token.", ex);
        }
    }
}