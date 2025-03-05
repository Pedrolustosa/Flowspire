using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;

public class TransactionService(ITransactionRepository transactionRepository, 
                                IBudgetService budgetService) : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IBudgetService _budgetService = budgetService;

    public async Task<TransactionDTO> AddTransactionAsync(TransactionDTO transactionDto)
    {
        try
        {
            var transaction = Transaction.Create(
                transactionDto.Description,
                transactionDto.Amount,
                transactionDto.Date,
                transactionDto.CategoryId,
                transactionDto.UserId);

            var addedTransaction = await _transactionRepository.AddAsync(transaction);
            await _budgetService.CheckBudgetAndNotifyAsync(transactionDto.UserId, transactionDto.CategoryId, transactionDto.Amount);

            return new TransactionDTO
            {
                Id = addedTransaction.Id,
                Description = addedTransaction.Description,
                Amount = addedTransaction.Amount,
                Date = addedTransaction.Date,
                CategoryId = addedTransaction.CategoryId,
                UserId = addedTransaction.UserId
            };
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao adicionar a transação ao banco de dados.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar transação.", ex);
        }
    }

    public async Task<List<TransactionDTO>> GetTransactionsByUserIdAsync(string userId)
    {
        try
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            return transactions.Select(t => new TransactionDTO
            {
                Id = t.Id,
                Description = t.Description,
                Amount = t.Amount,
                Date = t.Date,
                CategoryId = t.CategoryId,
                UserId = t.UserId
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar transações.", ex);
        }
    }

    public async Task<FinancialReportDTO> GetFinancialReportAsync(string userId, DateTime startDate, DateTime endDate)
    {
        try
        {
            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            var filteredTransactions = transactions
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .ToList();

            var report = new FinancialReportDTO
            {
                TotalIncome = filteredTransactions.Where(t => t.Amount > 0).Sum(t => t.Amount),
                TotalExpenses = filteredTransactions.Where(t => t.Amount < 0).Sum(t => Math.Abs(t.Amount)),
                ExpensesByCategory = filteredTransactions
                    .Where(t => t.Amount < 0)
                    .GroupBy(t => t.Category.Name)
                    .ToDictionary(g => g.Key, g => Math.Abs(g.Sum(t => t.Amount)))
            };

            return report;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao gerar relatório financeiro.", ex);
        }
    }
}