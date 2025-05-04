namespace Flowspire.Domain.Interfaces;

public interface IBudgetRepository
{
    Task<Budget?> GetByIdAsync(int id);
    Task<List<Budget>> GetAllAsync();
    Task<List<Budget>> GetByUserIdAsync(string userId);
    Task<List<Budget>> GetActiveBudgetsAsync(string userId, DateTime reference);
    Task<Budget?> GetBudgetByCategoryIdAsync(string userId, int categoryId);
    Task AddAsync(Budget budget);
    Task UpdateAsync(Budget budget);
    Task DeleteAsync(Budget budget);
}