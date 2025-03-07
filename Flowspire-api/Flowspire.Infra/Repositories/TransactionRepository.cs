using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;
using Microsoft.Data.Sqlite;

namespace Flowspire.Infra.Repositories;
public class TransactionRepository(ApplicationDbContext context) : ITransactionRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        try
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao adicionar a transação ao banco de dados.", ex);
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao adicionar transação.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar transação.", ex);
        }
    }

    public async Task<List<Transaction>> GetByUserIdAsync(string userId)
    {
        try
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao recuperar transações.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar transações.", ex);
        }
    }
}