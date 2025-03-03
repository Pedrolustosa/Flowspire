using Flowspire.Domain.Entities;

namespace Flowspire.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> AddAsync(Transaction transaction);
    Task<List<Transaction>> GetByUserIdAsync(string userId);
}