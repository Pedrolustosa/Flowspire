using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;
using Flowspire.Application.Interfaces;

namespace Flowspire.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<CategoryDTO> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return MapToDTO(category);
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(MapToDTO).ToList();
    }

    public async Task<IEnumerable<CategoryDTO>> GetByUserIdAsync(string userId)
    {
        var categories = await _categoryRepository.GetByUserIdAsync(userId);
        return categories.Select(MapToDTO).ToList();
    }

    public async Task CreateAsync(CategoryDTO categoryDTO)
    {
        var category = Category.Create(
            categoryDTO.Name,
            categoryDTO.UserId,
            categoryDTO.Description,
            categoryDTO.IsDefault,
            categoryDTO.SortOrder);

        await _categoryRepository.AddAsync(category);
        categoryDTO.Id = category.Id;
    }

    public async Task UpdateAsync(CategoryDTO categoryDTO)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryDTO.Id);
        if (category == null)
            throw new Exception("Category not found.");

        category.UpdateName(categoryDTO.Name);
        category.UpdateDescription(categoryDTO.Description);
        category.UpdateSortOrder(categoryDTO.SortOrder);
        if (category.IsDefault != categoryDTO.IsDefault)
            category.ToggleDefault();

        await _categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            throw new Exception("Category not found.");
        await _categoryRepository.DeleteAsync(category);
    }

    public async Task<bool> ExistsByNameAsync(string userId, string name)
    {
        return await _categoryRepository.ExistsByNameAsync(userId, name);
    }

    public async Task<CategoryDTO> GetByNameAsync(string userId, string name)
    {
        var category = await _categoryRepository.GetCategoryByNameAsync(userId, name);
        return MapToDTO(category);
    }

    public async Task<IEnumerable<CategoryDTO>> GetDefaultCategoriesAsync(string userId)
    {
        var categories = await _categoryRepository.GetDefaultCategoriesAsync(userId);
        return categories.Select(MapToDTO).ToList();
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesWithTransactionsAsync(string userId)
    {
        var categories = await _categoryRepository.GetCategoriesWithTransactionsAsync(userId);
        return categories.Select(MapToDTO).ToList();
    }

    public async Task<int> CountByUserAsync(string userId)
    {
        return await _categoryRepository.CountByUserAsync(userId);
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