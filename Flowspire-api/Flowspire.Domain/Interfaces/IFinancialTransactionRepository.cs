namespace Flowspire.Domain.Interfaces;

public interface IFinancialTransactionRepository
{
    Task<FinancialTransaction> GetByIdAsync(int id);

    Task<IEnumerable<FinancialTransaction>> GetAllAsync();

    Task<IEnumerable<FinancialTransaction>> GetByUserIdAsync(string userId);

    Task<IEnumerable<FinancialTransaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);

    Task AddAsync(FinancialTransaction transaction);

    Task UpdateAsync(FinancialTransaction transaction);

    Task DeleteAsync(FinancialTransaction transaction);
}