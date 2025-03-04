using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto)
    {
        var category = Category.Create(categoryDto.Name, categoryDto.UserId);
        var addedCategory = await _categoryRepository.AddAsync(category);
        return new CategoryDTO
        {
            Id = addedCategory.Id,
            Name = addedCategory.Name,
            UserId = addedCategory.UserId
        };
    }

    public async Task<List<CategoryDTO>> GetCategoriesByUserIdAsync(string userId)
    {
        var categories = await _categoryRepository.GetByUserIdAsync(userId);
        return categories.Select(c => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name,
            UserId = c.UserId
        }).ToList();
    }

    public async Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categoryDto)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
        if (category == null)
            throw new Exception("Categoria não encontrada.");

        category.UpdateName(categoryDto.Name);
        await _categoryRepository.UpdateAsync(category);
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name,
            UserId = category.UserId
        };
    }
}