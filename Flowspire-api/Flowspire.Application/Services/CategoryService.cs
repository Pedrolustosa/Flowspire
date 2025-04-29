using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;
using Flowspire.Application.Interfaces;
using Flowspire.Application.Common;
using Microsoft.Extensions.Logging;

namespace Flowspire.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ILogger<CategoryService> _logger = logger;

    public async Task<CategoryDTO> GetByIdAsync(int id)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception(ErrorMessages.CategoryNotFound);

            return MapToDTO(category);
        }, _logger, nameof(GetByIdAsync));
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(MapToDTO).ToList();
        }, _logger, nameof(GetAllAsync));
    }

    public async Task<IEnumerable<CategoryDTO>> GetByUserIdAsync(string userId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var categories = await _categoryRepository.GetByUserIdAsync(userId);
            return categories.Select(MapToDTO).ToList();
        }, _logger, nameof(GetByUserIdAsync));
    }

    public async Task CreateAsync(CategoryDTO categoryDTO)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var category = Category.Create(
                categoryDTO.Name,
                categoryDTO.UserId,
                categoryDTO.Description,
                categoryDTO.IsDefault,
                categoryDTO.SortOrder);

            await _categoryRepository.AddAsync(category);

            categoryDTO.Id = category.Id;
        }, _logger, nameof(CreateAsync));
    }

    public async Task UpdateAsync(CategoryDTO categoryDTO)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDTO.Id);
            if (category == null)
                throw new Exception(ErrorMessages.CategoryNotFound);

            category.UpdateName(categoryDTO.Name);
            category.UpdateDescription(categoryDTO.Description);
            category.UpdateSortOrder(categoryDTO.SortOrder);

            if (category.IsDefault != categoryDTO.IsDefault)
                category.ToggleDefault();

            await _categoryRepository.UpdateAsync(category);
        }, _logger, nameof(UpdateAsync));
    }

    public async Task DeleteAsync(int id)
    {
        await ServiceHelper.ExecuteAsync(async () =>
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new Exception(ErrorMessages.CategoryNotFound);

            await _categoryRepository.DeleteAsync(category);
        }, _logger, nameof(DeleteAsync));
    }

    public async Task<bool> ExistsByNameAsync(string userId, string name)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            return await _categoryRepository.ExistsByNameAsync(userId, name);
        }, _logger, nameof(ExistsByNameAsync));
    }

    public async Task<CategoryDTO> GetByNameAsync(string userId, string name)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var category = await _categoryRepository.GetCategoryByNameAsync(userId, name);
            if (category == null)
                throw new Exception(ErrorMessages.CategoryNotFound);

            return MapToDTO(category);
        }, _logger, nameof(GetByNameAsync));
    }

    public async Task<IEnumerable<CategoryDTO>> GetDefaultCategoriesAsync(string userId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var categories = await _categoryRepository.GetDefaultCategoriesAsync(userId);
            return categories.Select(MapToDTO).ToList();
        }, _logger, nameof(GetDefaultCategoriesAsync));
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesWithTransactionsAsync(string userId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            var categories = await _categoryRepository.GetCategoriesWithTransactionsAsync(userId);
            return categories.Select(MapToDTO).ToList();
        }, _logger, nameof(GetCategoriesWithTransactionsAsync));
    }

    public async Task<int> CountByUserAsync(string userId)
    {
        return await ServiceHelper.ExecuteAsync(async () =>
        {
            return await _categoryRepository.CountByUserAsync(userId);
        }, _logger, nameof(CountByUserAsync));
    }

    private CategoryDTO MapToDTO(Category category)
    {
        if (category == null)
            return null;

        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            UserId = category.UserId,
            IsDefault = category.IsDefault,
            SortOrder = category.SortOrder
        };
    }
}
