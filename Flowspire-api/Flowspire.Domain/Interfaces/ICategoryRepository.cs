using Flowspire.Domain.Entities;

namespace Flowspire.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(int id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<IEnumerable<Category>> GetByUserIdAsync(string userId);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);

    Task<bool> ExistsByNameAsync(string userId, string name);
    Task<Category> GetCategoryByNameAsync(string userId, string name);
    Task<IEnumerable<Category>> GetDefaultCategoriesAsync(string userId);
    Task<IEnumerable<Category>> GetCategoriesWithTransactionsAsync(string userId);
    Task<int> CountByUserAsync(string userId);
}
