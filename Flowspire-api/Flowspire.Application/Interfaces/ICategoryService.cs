using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryDTO> GetByIdAsync(int id);
    Task<IEnumerable<CategoryDTO>> GetAllAsync();
    Task<IEnumerable<CategoryDTO>> GetByUserIdAsync(string userId);
    Task CreateAsync(CategoryDTO categoryDTO);
    Task UpdateAsync(CategoryDTO categoryDTO);
    Task DeleteAsync(int id);

    Task<bool> ExistsByNameAsync(string userId, string name);
    Task<CategoryDTO> GetByNameAsync(string userId, string name);
    Task<IEnumerable<CategoryDTO>> GetDefaultCategoriesAsync(string userId);
    Task<IEnumerable<CategoryDTO>> GetCategoriesWithTransactionsAsync(string userId);
    Task<int> CountByUserAsync(string userId);
}
