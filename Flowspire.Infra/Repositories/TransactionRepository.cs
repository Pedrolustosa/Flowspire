using Flowspire.Infra.Data;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flowspire.Infra.Repositories;

public class TransactionRepository(ApplicationDbContext context) : ITransactionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<List<Transaction>> GetByUserIdAsync(string userId)
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }
}