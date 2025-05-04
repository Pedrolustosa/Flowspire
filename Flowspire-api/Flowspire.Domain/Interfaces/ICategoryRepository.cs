using Flowspire.Domain.Entities;

namespace Flowspire.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id);
    Task<List<Category>> GetAllAsync();
    Task<List<Category>> GetByUserIdAsync(string userId);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);

    Task<bool> ExistsByNameAsync(string userId, string name);
    Task<Category?> GetByNameAsync(string userId, string name);
    Task<List<Category>> GetDefaultCategoriesAsync(string userId);
    Task<List<Category>> GetCategoriesWithTransactionsAsync(string userId);
    Task<int> CountByUserAsync(string userId);
}
