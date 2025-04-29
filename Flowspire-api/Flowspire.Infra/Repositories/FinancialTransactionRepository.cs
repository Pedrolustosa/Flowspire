using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Common;
using Flowspire.Infra.Data;

namespace Flowspire.Infra.Repositories;

public class FinancialTransactionRepository(ApplicationDbContext context, ILogger<FinancialTransactionRepository> logger) : IFinancialTransactionRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<FinancialTransactionRepository> _logger = logger;

    public async Task<FinancialTransaction> GetByIdAsync(int id)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.FinancialTransactions
                .FirstOrDefaultAsync(t => t.Id == id);
        }, _logger, nameof(GetByIdAsync));
    }

    public async Task<IEnumerable<FinancialTransaction>> GetAllAsync()
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.FinancialTransactions
                .ToListAsync();
        }, _logger, nameof(GetAllAsync));
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByUserIdAsync(string userId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.FinancialTransactions
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }, _logger, nameof(GetByUserIdAsync));
    }

    public async Task<IEnumerable<FinancialTransaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.FinancialTransactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToListAsync();
        }, _logger, nameof(GetTransactionsByDateRangeAsync));
    }

    public async Task AddAsync(FinancialTransaction transaction)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            await _context.FinancialTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }, _logger, nameof(AddAsync));
    }

    public async Task UpdateAsync(FinancialTransaction transaction)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.FinancialTransactions.Update(transaction);
            await _context.SaveChangesAsync();
        }, _logger, nameof(UpdateAsync));
    }

    public async Task DeleteAsync(FinancialTransaction transaction)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.FinancialTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }, _logger, nameof(DeleteAsync));
    }
}
