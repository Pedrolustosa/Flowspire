using Flowspire.Application.Common;
using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Enums;
using Flowspire.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Flowspire.Application.Services;

public class FinancialTransactionService(
    IFinancialTransactionRepository transactionRepository,
    ILogger<FinancialTransactionService> logger
) : IFinancialTransactionService
{
    private readonly IFinancialTransactionRepository _transactionRepository = transactionRepository;
    private readonly ILogger<FinancialTransactionService> _logger = logger;

    public async Task<FinancialTransactionDTO> GetByIdAsync(int id)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            return transaction == null ? null : MapToDTO(transaction);
        }, _logger, nameof(GetByIdAsync));
    }

    public async Task<IEnumerable<FinancialTransactionDTO>> GetAllAsync()
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var transactions = await _transactionRepository.GetAllAsync();
            return transactions.Select(MapToDTO).ToList();
        }, _logger, nameof(GetAllAsync));
    }

    public async Task<IEnumerable<FinancialTransactionDTO>> GetByUserIdAsync(string userId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            return transactions.Select(MapToDTO).ToList();
        }, _logger, nameof(GetByUserIdAsync));
    }

    public async Task<IEnumerable<FinancialTransactionDTO>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var transactions = await _transactionRepository.GetTransactionsByDateRangeAsync(startDate, endDate);
            return transactions.Select(MapToDTO).ToList();
        }, _logger, nameof(GetByDateRangeAsync));
    }

    public async Task CreateAsync(FinancialTransactionDTO transactionDTO)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            if (!Enum.TryParse<TransactionType>(transactionDTO.TransactionType, true, out var type))
                throw new ArgumentException(ErrorMessages.InvalidTransactionType);

            var transaction = FinancialTransaction.Create(
                transactionDTO.Description,
                transactionDTO.Amount,
                transactionDTO.Date,
                type,
                transactionDTO.CategoryId,
                transactionDTO.UserId,
                transactionDTO.Fee,
                transactionDTO.Discount,
                transactionDTO.Notes,
                transactionDTO.PaymentMethod,
                transactionDTO.IsRecurring,
                transactionDTO.NextOccurrence
            );

            await _transactionRepository.AddAsync(transaction);
        }, _logger, nameof(CreateAsync));
    }

    public async Task UpdateAsync(FinancialTransactionDTO transactionDTO)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionDTO.Id);
            if (transaction == null)
                throw new KeyNotFoundException(ErrorMessages.TransactionNotFound);

            if (!Enum.TryParse<TransactionType>(transactionDTO.TransactionType, true, out var type))
                throw new ArgumentException(ErrorMessages.InvalidTransactionType);

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
        }, _logger, nameof(UpdateAsync));
    }

    public async Task DeleteAsync(int id)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
                throw new KeyNotFoundException(ErrorMessages.TransactionNotFound);

            await _transactionRepository.DeleteAsync(transaction);
        }, _logger, nameof(DeleteAsync));
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
            CategoryName = transaction.Category?.Name ?? string.Empty,
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
