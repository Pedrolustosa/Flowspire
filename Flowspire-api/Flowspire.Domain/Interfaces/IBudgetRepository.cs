namespace Flowspire.Domain.Interfaces;

public interface IBudgetRepository
{
    Task<Budget> GetByIdAsync(int id);
    Task<IEnumerable<Budget>> GetAllAsync();
    Task<IEnumerable<Budget>> GetByUserIdAsync(string userId);
    Task AddAsync(Budget budget);
    Task UpdateAsync(Budget budget);
    Task DeleteAsync(Budget budget);

    Task<IEnumerable<Budget>> GetActiveBudgetsAsync(string userId, DateTime date);
    Task<Budget> GetBudgetByCategoryIdAsync(string userId, int categoryId);
}