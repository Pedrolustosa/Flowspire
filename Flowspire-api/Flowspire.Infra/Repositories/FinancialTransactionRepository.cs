using Microsoft.EntityFrameworkCore;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Infra.Repositories;

public class FinancialTransactionRepository(ApplicationDbContext context) : IFinancialTransactionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<FinancialTransaction> GetByIdAsync(int id)
    {
        return await _context.FinancialTransactions
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<FinancialTransaction>> GetAllAsync()
    {
        return await _context.FinancialTransactions
            .ToListAsync();
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByUserIdAsync(string userId)
    {
        return await _context.FinancialTransactions
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<FinancialTransaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.FinancialTransactions
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .ToListAsync();
    }

    public async Task AddAsync(FinancialTransaction transaction)
    {
        await _context.FinancialTransactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FinancialTransaction transaction)
    {
        _context.FinancialTransactions.Update(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(FinancialTransaction transaction)
    {
        _context.FinancialTransactions.Remove(transaction);
        await _context.SaveChangesAsync();
    }
}
