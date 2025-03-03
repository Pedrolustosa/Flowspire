using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;
public interface ITransactionService
{
    Task<TransactionDTO> AddTransactionAsync(TransactionDTO transactionDto);
    Task<List<TransactionDTO>> GetTransactionsByUserIdAsync(string userId);
}