using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Data;
using Flowspire.Infra.Common;
using Microsoft.Extensions.Logging;
using Flowspire.Domain.Entities;

namespace Flowspire.Infra.Repositories;

public class FinancialTransactionRepository(ApplicationDbContext context, ILogger<FinancialTransactionRepository> logger) : IFinancialTransactionRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<FinancialTransactionRepository> _logger = logger;

    public Task<FinancialTransaction?> GetByIdAsync(int id)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Transactions.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id),
            _logger,
            nameof(GetByIdAsync));

    public Task<List<FinancialTransaction>> GetAllAsync()
        => RepositoryHelper.ExecuteAsync(
            () => _context.Transactions.Include(t => t.Category).ToListAsync(),
            _logger,
            nameof(GetAllAsync));

    public Task<List<FinancialTransaction>> GetByUserIdAsync(string userId)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Transactions.Where(t => t.UserId == userId).ToListAsync(),
            _logger,
            nameof(GetByUserIdAsync));

    public Task<List<FinancialTransaction>> GetByUserIdAndDateRangeAsync(string userId, DateTime startDate, DateTime endDate)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Transactions
                .Where(t => t.UserId == userId && t.Date >= startDate && t.Date <= endDate)
                .ToListAsync(),
            _logger,
            nameof(GetByUserIdAndDateRangeAsync));

    public Task AddAsync(FinancialTransaction transaction)
        => RepositoryHelper.ExecuteAsync(
            async () => { await _context.Transactions.AddAsync(transaction); await _context.SaveChangesAsync(); },
            _logger,
            nameof(AddAsync));

    public Task UpdateAsync(FinancialTransaction transaction)
        => RepositoryHelper.ExecuteAsync(
            async () => { _context.Transactions.Update(transaction); await _context.SaveChangesAsync(); },
            _logger,
            nameof(UpdateAsync));

    public Task DeleteAsync(FinancialTransaction transaction)
        => RepositoryHelper.ExecuteAsync(
            async () => { _context.Transactions.Remove(transaction); await _context.SaveChangesAsync(); },
            _logger,
            nameof(DeleteAsync));

    public Task<IEnumerable<FinancialTransaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
    => RepositoryHelper.ExecuteAsync(
        async () =>
            (IEnumerable<FinancialTransaction>)await _context.Transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync(),
        _logger,
        nameof(GetTransactionsByDateRangeAsync)
    );
}
