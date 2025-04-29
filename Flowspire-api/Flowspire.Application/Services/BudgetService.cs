using Flowspire.Application.DTOs;
using Flowspire.Application.Interfaces;
using Flowspire.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Flowspire.Application.Common;

namespace Flowspire.Application.Services;

public class BudgetService(IBudgetRepository budgetRepository, ILogger<BudgetService> logger) : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository = budgetRepository;
    private readonly ILogger<BudgetService> _logger = logger;

    public async Task<BudgetDTO> GetByIdAsync(int id)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var budget = await _budgetRepository.GetByIdAsync(id);
            return MapToDTO(budget);
        }, _logger, nameof(GetByIdAsync));
    }

    public async Task<IEnumerable<BudgetDTO>> GetAllAsync()
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var budgets = await _budgetRepository.GetAllAsync();
            return budgets.Select(MapToDTO).ToList();
        }, _logger, nameof(GetAllAsync));
    }

    public async Task<IEnumerable<BudgetDTO>> GetByUserIdAsync(string userId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var budgets = await _budgetRepository.GetByUserIdAsync(userId);
            return budgets.Select(MapToDTO).ToList();
        }, _logger, nameof(GetByUserIdAsync));
    }

    public async Task CreateAsync(BudgetDTO budgetDTO)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var budget = Budget.Create(
                budgetDTO.Amount,
                budgetDTO.StartDate,
                budgetDTO.EndDate,
                budgetDTO.CategoryId,
                budgetDTO.UserId
            );

            await _budgetRepository.AddAsync(budget);
            budgetDTO.Id = budget.Id;
        }, _logger, nameof(CreateAsync));
    }

    public async Task UpdateAsync(BudgetDTO budgetDTO)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var budget = await _budgetRepository.GetByIdAsync(budgetDTO.Id);
            if (budget == null)
                throw new KeyNotFoundException("Budget not found.");

            budget.Update(
                budgetDTO.Amount,
                budgetDTO.StartDate,
                budgetDTO.EndDate,
                budgetDTO.CategoryId
            );

            await _budgetRepository.UpdateAsync(budget);
        }, _logger, nameof(UpdateAsync));
    }

    public async Task DeleteAsync(int id)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var budget = await _budgetRepository.GetByIdAsync(id);
            if (budget == null)
                throw new KeyNotFoundException("Budget not found.");

            await _budgetRepository.DeleteAsync(budget);
        }, _logger, nameof(DeleteAsync));
    }

    public async Task<IEnumerable<BudgetDTO>> GetActiveBudgetsAsync(string userId, DateTime date)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var budgets = await _budgetRepository.GetActiveBudgetsAsync(userId, date);
            return budgets.Select(MapToDTO).ToList();
        }, _logger, nameof(GetActiveBudgetsAsync));
    }

    public async Task<BudgetDTO> GetBudgetByCategoryIdAsync(string userId, int categoryId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var budget = await _budgetRepository.GetBudgetByCategoryIdAsync(userId, categoryId);
            return MapToDTO(budget);
        }, _logger, nameof(GetBudgetByCategoryIdAsync));
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
            CategoryName = budget.Category?.Name ?? string.Empty
        };
    }
}
