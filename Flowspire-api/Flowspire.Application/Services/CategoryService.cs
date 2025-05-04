using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;
using Flowspire.Application.Interfaces;
using Flowspire.Application.Common;
using Microsoft.Extensions.Logging;

namespace Flowspire.Application.Services;

public class CategoryService(
    ICategoryRepository categoryRepository,
    ILogger<CategoryService> logger) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository
            ?? throw new ArgumentNullException(nameof(categoryRepository));
    private readonly ILogger<CategoryService> _logger = logger
            ?? throw new ArgumentNullException(nameof(logger));

    public async Task<CategoryDTO> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (id <= 0)
                throw new ArgumentException("Invalid category ID.", nameof(id));

            _logger.LogInformation("Fetching category {CategoryId}...", id);
            var entity = await _categoryRepository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException(ErrorMessages.CategoryNotFound);

            var dto = MapToDTO(entity);
            _logger.LogInformation("Category {CategoryId} retrieved.", id);
            return dto;
        }, _logger, nameof(GetByIdAsync));

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync(
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Retrieving all categories...");
            var list = await _categoryRepository.GetAllAsync();
            var dtos = list.Select(MapToDTO).ToList();
            _logger.LogInformation("Retrieved {Count} categories.", dtos.Count());
            return dtos;
        }, _logger, nameof(GetAllAsync));

    public async Task<IEnumerable<CategoryDTO>> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID must be provided.", nameof(userId));

            _logger.LogInformation("Retrieving categories for user {UserId}...", userId);
            var list = await _categoryRepository.GetByUserIdAsync(userId);
            var dtos = list.Select(MapToDTO).ToList();
            _logger.LogInformation("User {UserId} has {Count} categories.", userId, dtos.Count());
            return dtos;
        }, _logger, nameof(GetByUserIdAsync));

    public async Task CreateAsync(
        CategoryDTO categoryDTO,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (categoryDTO == null)
                throw new ArgumentNullException(nameof(categoryDTO));

            _logger.LogInformation("Creating category '{Name}' for user {UserId}...", categoryDTO.Name, categoryDTO.UserId);
            var entity = Category.Create(
                    categoryDTO.Name,
                    categoryDTO.UserId,
                    categoryDTO.Description,
                    categoryDTO.IsDefault,
                    categoryDTO.SortOrder);

            await _categoryRepository.AddAsync(entity);
            categoryDTO.Id = entity.Id;
            _logger.LogInformation("Category {CategoryId} created.", entity.Id);
        }, _logger, nameof(CreateAsync));

    public async Task UpdateAsync(
        CategoryDTO categoryDTO,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (categoryDTO == null)
                throw new ArgumentNullException(nameof(categoryDTO));
            if (categoryDTO.Id <= 0)
                throw new ArgumentException("Invalid category ID.", nameof(categoryDTO.Id));

            _logger.LogInformation("Updating category {CategoryId}...", categoryDTO.Id);
            var entity = await _categoryRepository.GetByIdAsync(categoryDTO.Id)
                             ?? throw new KeyNotFoundException(ErrorMessages.CategoryNotFound);

            entity.UpdateName(categoryDTO.Name);
            entity.UpdateDescription(categoryDTO.Description);
            entity.UpdateSortOrder(categoryDTO.SortOrder);
            if (entity.IsDefault != categoryDTO.IsDefault)
                entity.ToggleDefault();

            await _categoryRepository.UpdateAsync(entity);
            _logger.LogInformation("Category {CategoryId} updated.", categoryDTO.Id);
        }, _logger, nameof(UpdateAsync));

    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (id <= 0)
                throw new ArgumentException("Invalid category ID.", nameof(id));

            _logger.LogInformation("Deleting category {CategoryId}...", id);
            var entity = await _categoryRepository.GetByIdAsync(id)
                             ?? throw new KeyNotFoundException(ErrorMessages.CategoryNotFound);

            await _categoryRepository.DeleteAsync(entity);
            _logger.LogInformation("Category {CategoryId} deleted.", id);
        }, _logger, nameof(DeleteAsync));

    public async Task<bool> ExistsByNameAsync(
        string userId,
        string name,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(
            () => _categoryRepository.ExistsByNameAsync(userId, name),
            _logger,
            nameof(ExistsByNameAsync));

    public async Task<CategoryDTO> GetByNameAsync(
        string userId,
        string name,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID required.", nameof(userId));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name required.", nameof(name));

            _logger.LogInformation("Fetching category '{Name}' for user {UserId}...", name, userId);
            var entity = await _categoryRepository.GetByNameAsync(userId, name)
                             ?? throw new KeyNotFoundException(ErrorMessages.CategoryNotFound);

            var dto = MapToDTO(entity);
            _logger.LogInformation("Category '{Name}' retrieved.", name);
            return dto;
        }, _logger, nameof(GetByNameAsync));

    public async Task<IEnumerable<CategoryDTO>> GetDefaultCategoriesAsync(
        string userId,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID required.", nameof(userId));

            _logger.LogInformation("Fetching default categories for user {UserId}...", userId);
            var list = await _categoryRepository.GetDefaultCategoriesAsync(userId);
            var dtos = list.Select(MapToDTO).ToList();
            _logger.LogInformation("Retrieved {Count} default categories.", dtos.Count());
            return dtos;
        }, _logger, nameof(GetDefaultCategoriesAsync));

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesWithTransactionsAsync(
        string userId,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID required.", nameof(userId));

            _logger.LogInformation("Fetching categories with transactions for user {UserId}...", userId);
            var list = await _categoryRepository.GetCategoriesWithTransactionsAsync(userId);
            var dtos = list.Select(MapToDTO).ToList();
            _logger.LogInformation("Retrieved {Count} categories with transactions.", dtos.Count());
            return dtos;
        }, _logger, nameof(GetCategoriesWithTransactionsAsync));

    public async Task<int> CountByUserAsync(
        string userId,
        CancellationToken cancellationToken = default)
        => await ServiceHelper.ExecuteAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("User ID required.", nameof(userId));

            _logger.LogInformation("Counting categories for user {UserId}...", userId);
            var count = await _categoryRepository.CountByUserAsync(userId);
            _logger.LogInformation("User {UserId} has {Count} categories.", userId, count);
            return count;
        }, _logger, nameof(CountByUserAsync));

    private static CategoryDTO MapToDTO(Category c)
    {
        return new CategoryDTO
        {
            Id          = c.Id,
            Name        = c.Name,
            Description = c.Description,
            UserId      = c.UserId,
            IsDefault   = c.IsDefault,
            SortOrder   = c.SortOrder
        };
    }
}
