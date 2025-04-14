using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;
using Flowspire.Application.Interfaces;

namespace Flowspire.Application.Services;

public class BudgetService(IBudgetRepository budgetRepository) : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;

    public async Task<BudgetDTO> GetByIdAsync(int id)
    {
        var budget = await _budgetRepository.GetByIdAsync(id);
        return MapToDTO(budget);
    }

    public async Task<IEnumerable<BudgetDTO>> GetAllAsync()
    {
        var budgets = await _budgetRepository.GetAllAsync();
        return budgets.Select(b => MapToDTO(b)).ToList();
    }

    public async Task<IEnumerable<BudgetDTO>> GetByUserIdAsync(string userId)
    {
        var budgets = await _budgetRepository.GetByUserIdAsync(userId);
        return budgets.Select(b => MapToDTO(b)).ToList();
    }

    public async Task CreateAsync(BudgetDTO budgetDTO)
    {
        var budget = Budget.Create(budgetDTO.Amount, budgetDTO.StartDate, budgetDTO.EndDate, budgetDTO.CategoryId, budgetDTO.UserId);
        await _budgetRepository.AddAsync(budget);
        budgetDTO.Id = budget.Id;
    }

    public async Task UpdateAsync(BudgetDTO budgetDTO)
    {
        var budget = await _budgetRepository.GetByIdAsync(budgetDTO.Id);
        if (budget == null)
            throw new Exception("Budget not found.");
;
        budget.Update(budgetDTO.Amount, budgetDTO.StartDate, budgetDTO.EndDate, budgetDTO.CategoryId);
        await _budgetRepository.UpdateAsync(budget);
    }

    public async Task DeleteAsync(int id)
    {
        var budget = await _budgetRepository.GetByIdAsync(id);
        if (budget == null)
            throw new Exception("Budget not found.");
        await _budgetRepository.DeleteAsync(budget);
    }

    public async Task<IEnumerable<BudgetDTO>> GetActiveBudgetsAsync(string userId, DateTime date)
    {
        var budgets = await _budgetRepository.GetActiveBudgetsAsync(userId, date);
        return budgets.Select(b => MapToDTO(b)).ToList();
    }

    public async Task<BudgetDTO> GetBudgetByCategoryIdAsync(string userId, int categoryId)
    {
        var budget = await _budgetRepository.GetBudgetByCategoryIdAsync(userId, categoryId);
        return MapToDTO(budget);
    }

    private BudgetDTO MapToDTO(Budget budget)
    {
        if (budget == null)
            return null;

        return new BudgetDTO
        {
            Id = budget.Id,
            Amount = budget.Amount,
            StartDate = budget.StartDate,
            EndDate = budget.EndDate,
            CategoryId = budget.CategoryId,
            UserId = budget.UserId,
            CategoryName = budget.Category != null ? budget.Category.Name : string.Empty
        };
    }
}
