using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;

public interface IBudgetService
{
    Task<BudgetDTO> GetByIdAsync(int id);
    Task<IEnumerable<BudgetDTO>> GetAllAsync();
    Task<IEnumerable<BudgetDTO>> GetByUserIdAsync(string userId);
    Task CreateAsync(BudgetDTO budgetDTO);
    Task UpdateAsync(BudgetDTO budgetDTO);
    Task DeleteAsync(int id);
    Task<IEnumerable<BudgetDTO>> GetActiveBudgetsAsync(string userId, DateTime date);
    Task<BudgetDTO> GetBudgetByCategoryIdAsync(string userId, int categoryId);
}