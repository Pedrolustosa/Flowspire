using Flowspire.Domain.Entities;

namespace Flowspire.Domain.Interfaces;

public interface IFinancialTransactionRepository
{
    Task<FinancialTransaction?> GetByIdAsync(int id);
    Task<List<FinancialTransaction>> GetAllAsync();
    Task<List<FinancialTransaction>> GetByUserIdAsync(string userId);
    Task<List<FinancialTransaction>> GetByUserIdAndDateRangeAsync(string userId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<FinancialTransaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task AddAsync(FinancialTransaction transaction);
    Task UpdateAsync(FinancialTransaction transaction);
    Task DeleteAsync(FinancialTransaction transaction);
}