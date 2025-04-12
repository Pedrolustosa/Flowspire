using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;

public interface IFinancialTransactionService
{
    Task<FinancialTransactionDTO> GetByIdAsync(int id);

    Task<IEnumerable<FinancialTransactionDTO>> GetAllAsync();

    Task<IEnumerable<FinancialTransactionDTO>> GetByUserIdAsync(string userId);

    Task<IEnumerable<FinancialTransactionDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

    Task CreateAsync(FinancialTransactionDTO transactionDTO);

    Task UpdateAsync(FinancialTransactionDTO transactionDTO);

    Task DeleteAsync(int id);
}
