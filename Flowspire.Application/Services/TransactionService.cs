using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;

public class TransactionService(ITransactionRepository transactionRepository) : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async Task<TransactionDTO> AddTransactionAsync(TransactionDTO transactionDto)
    {
        var transaction = Transaction.Create(
            transactionDto.Description,
            transactionDto.Amount,
            transactionDto.Date,
            transactionDto.Category,
            transactionDto.UserId);

        var addedTransaction = await _transactionRepository.AddAsync(transaction);
        return new TransactionDTO
        {
            Id = addedTransaction.Id,
            Description = addedTransaction.Description,
            Amount = addedTransaction.Amount,
            Date = addedTransaction.Date,
            Category = addedTransaction.Category,
            UserId = addedTransaction.UserId
        };
    }

    public async Task<List<TransactionDTO>> GetTransactionsByUserIdAsync(string userId)
    {
        var transactions = await _transactionRepository.GetByUserIdAsync(userId);
        return transactions.Select(t => new TransactionDTO
        {
            Id = t.Id,
            Description = t.Description,
            Amount = t.Amount,
            Date = t.Date,
            Category = t.Category,
            UserId = t.UserId
        }).ToList();
    }
}