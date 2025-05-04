using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Data;
using Flowspire.Infra.Common;
using Microsoft.Extensions.Logging;
using System;

namespace Flowspire.Infra.Repositories;

public class RefreshTokenRepository(ApplicationDbContext context, ILogger<RefreshTokenRepository> logger) : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<RefreshTokenRepository> _logger = logger;

    public Task<RefreshToken> AddAsync(RefreshToken token)
        => RepositoryHelper.ExecuteAsync(
            async () => { await _context.RefreshTokens.AddAsync(token); await _context.SaveChangesAsync(); return token; },
            _logger,
            nameof(AddAsync));

    public Task<RefreshToken?> GetByTokenAsync(string token)
        => RepositoryHelper.ExecuteAsync(
            () => _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token),
            _logger,
            nameof(GetByTokenAsync));

    public Task UpdateAsync(RefreshToken token)
        => RepositoryHelper.ExecuteAsync(
            async () => { _context.RefreshTokens.Update(token); await _context.SaveChangesAsync(); },
            _logger,
            nameof(UpdateAsync));
}
