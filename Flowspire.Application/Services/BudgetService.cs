using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;

public class BudgetService(IBudgetRepository budgetRepository, ITransactionRepository transactionRepository, INotificationService notificationService) : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly ITransactionRepository _transactionRepository = transactionRepository; 
    private readonly INotificationService _notificationService = notificationService;

    public async Task<BudgetDTO> AddBudgetAsync(BudgetDTO budgetDto)
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

    public async Task<List<BudgetDTO>> GetBudgetsByUserIdAsync(string userId)
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

    public async Task CheckBudgetAndNotifyAsync(string userId, int categoryId, decimal transactionAmount)
    {
        var budgets = await _budgetRepository.GetByUserIdAsync(userId);
        var budget = budgets.FirstOrDefault(b => b.CategoryId == categoryId && DateTime.UtcNow >= b.StartDate && DateTime.UtcNow <= b.EndDate);
        if (budget == null) return;

        var transactions = await _transactionRepository.GetByUserIdAsync(userId);
        var totalSpent = transactions
            .Where(t => t.CategoryId == categoryId && t.Date >= budget.StartDate && t.Date <= budget.EndDate)
            .Sum(t => t.Amount) + transactionAmount;

        if (totalSpent >= budget.Amount * 0.9m)
        {
            await _notificationService.SendNotificationAsync(userId,
                $"Atenção: Você atingiu 90% do orçamento de {budget.Category.Name} ({totalSpent}/{budget.Amount}).");
        }
    }
}
