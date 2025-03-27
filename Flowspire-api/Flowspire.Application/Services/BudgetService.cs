using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;

public class BudgetService(IBudgetRepository budgetRepository, 
                           ITransactionRepository transactionRepository) : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async Task<BudgetDTO> AddBudgetAsync(BudgetDTO budgetDto)
    {
        try
        {
            var budget = Budget.Create(budgetDto.CategoryId, budgetDto.Amount, budgetDto.StartDate, budgetDto.EndDate, budgetDto.UserId);
            var addedBudget = await _budgetRepository.AddAsync(budget);
            return new BudgetDTO
            {
                Id = addedBudget.Id,
                CategoryId = addedBudget.CategoryId,
                Amount = addedBudget.Amount,
                StartDate = addedBudget.StartDate,
                EndDate = addedBudget.EndDate,
                UserId = addedBudget.UserId
            };
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao adicionar o orçamento ao banco de dados.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar orçamento.", ex);
        }
    }

    public async Task<List<BudgetDTO>> GetBudgetsByUserIdAsync(string userId)
    {
        try
        {
            var budgets = await _budgetRepository.GetByUserIdAsync(userId);
            return budgets.Select(b => new BudgetDTO
            {
                Id = b.Id,
                CategoryId = b.CategoryId,
                Amount = b.Amount,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                UserId = b.UserId
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar orçamentos.", ex);
        }
    }

    public async Task CheckBudgetAndNotifyAsync(string userId, int categoryId, decimal transactionAmount)
    {
        try
        {
            var budgets = await _budgetRepository.GetByUserIdAsync(userId);
            var budget = budgets.FirstOrDefault(b => b.CategoryId == categoryId && DateTime.UtcNow >= b.StartDate && DateTime.UtcNow <= b.EndDate);
            if (budget == null) return;

            var transactions = await _transactionRepository.GetByUserIdAsync(userId);
            var totalSpent = transactions
                .Where(t => t.CategoryId == categoryId && t.Date >= budget.StartDate && t.Date <= budget.EndDate)
                .Sum(t => t.Amount) + transactionAmount;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao verificar orçamento e enviar notificação.", ex);
        }
    }
}