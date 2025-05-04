using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryDTO> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CategoryDTO>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CategoryDTO>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default);

    Task CreateAsync(
        CategoryDTO categoryDTO,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        CategoryDTO categoryDTO,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsByNameAsync(
        string userId,
        string name,
        CancellationToken cancellationToken = default);

    Task<CategoryDTO> GetByNameAsync(
        string userId,
        string name,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CategoryDTO>> GetDefaultCategoriesAsync(
        string userId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<CategoryDTO>> GetCategoriesWithTransactionsAsync(
        string userId,
        CancellationToken cancellationToken = default);

    Task<int> CountByUserAsync(
        string userId,
        CancellationToken cancellationToken = default);
}
