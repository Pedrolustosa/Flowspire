using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Enums;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;

public class FinancialTransactionService(IFinancialTransactionRepository transactionRepository) : IFinancialTransactionService
{
    private readonly IFinancialTransactionRepository _transactionRepository = transactionRepository;

    public async Task<FinancialTransactionDTO> GetByIdAsync(int id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            return null;
        return MapToDTO(transaction);
    }

    public async Task<IEnumerable<FinancialTransactionDTO>> GetAllAsync()
    {
        var transactions = await _transactionRepository.GetAllAsync();
        return transactions.Select(t => MapToDTO(t));
    }

    public async Task<IEnumerable<FinancialTransactionDTO>> GetByUserIdAsync(string userId)
    {
        var transactions = await _transactionRepository.GetByUserIdAsync(userId);
        return transactions.Select(t => MapToDTO(t));
    }

    public async Task<IEnumerable<FinancialTransactionDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);
        return transactions.Select(t => MapToDTO(t));
    }

    public async Task CreateAsync(FinancialTransactionDTO transactionDTO)
    {
        if (!Enum.TryParse<TransactionType>(transactionDTO.TransactionType, true, out var type))
            throw new ArgumentException("Invalid Transaction Type value.");

        var transaction = FinancialTransaction.Create(
            transactionDTO.Description,
            transactionDTO.Amount,
            transactionDTO.Date,
            type,
            transactionDTO.CategoryId,
            transactionDTO.UserId,
            fee: transactionDTO.Fee,
            discount: transactionDTO.Discount,
            notes: transactionDTO.Notes,
            paymentMethod: transactionDTO.PaymentMethod,
            isRecurring: transactionDTO.IsRecurring,
            nextOccurrence: transactionDTO.NextOccurrence
        );

        await _transactionRepository.AddAsync(transaction);
    }

    public async Task UpdateAsync(FinancialTransactionDTO transactionDTO)
    {
        var transaction = await _transactionRepository.GetByIdAsync(transactionDTO.Id);
        if (transaction == null)
            throw new Exception("Transaction not found.");

        if (!Enum.TryParse<TransactionType>(transactionDTO.TransactionType, true, out var type))
            throw new ArgumentException("Invalid Transaction Type value.");

        transaction.Update(
            transactionDTO.Description,
            transactionDTO.Amount,
            type,
            transactionDTO.CategoryId,
            transactionDTO.Fee,
            transactionDTO.Discount,
            transactionDTO.Notes,
            transactionDTO.PaymentMethod
        );

        await _transactionRepository.UpdateAsync(transaction);
    }

    public async Task DeleteAsync(int id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        if (transaction == null)
            throw new Exception("Transaction not found.");

        await _transactionRepository.DeleteAsync(transaction);
    }

    private FinancialTransactionDTO MapToDTO(FinancialTransaction transaction)
    {
        return new FinancialTransactionDTO
        {
            Id = transaction.Id,
            Description = transaction.Description,
            Amount = transaction.Amount,
            OriginalAmount = transaction.OriginalAmount,
            Fee = transaction.Fee,
            Discount = transaction.Discount,
            Date = transaction.Date,
            TransactionType = transaction.Type.ToString(),
            CategoryId = transaction.CategoryId,
            CategoryName = transaction.Category != null ? transaction.Category.Name : string.Empty,
            UserId = transaction.UserId,
            CreatedAt = transaction.CreatedAt,
            UpdatedAt = transaction.UpdatedAt,
            Notes = transaction.Notes,
            PaymentMethod = transaction.PaymentMethod,
            IsRecurring = transaction.IsRecurring,
            NextOccurrence = transaction.NextOccurrence
        };
    }
}
