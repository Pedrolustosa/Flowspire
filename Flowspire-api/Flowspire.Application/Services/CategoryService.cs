using Flowspire.Application.Interfaces;
using Flowspire.Domain.Entities;
using Flowspire.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Interfaces;

namespace Flowspire.Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto)
    {
        try
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
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao adicionar a categoria ao banco de dados.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar categoria.", ex);
        }
    }

    public async Task<List<CategoryDTO>> GetCategoriesByUserIdAsync(string userId)
    {
        try
        {
            var categories = await _categoryRepository.GetByUserIdAsync(userId);
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                UserId = c.UserId
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar categorias.", ex);
        }
    }

    public async Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categoryDto)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(categoryDto.Id);
            if (category == null)
                throw new KeyNotFoundException("Categoria não encontrada.");

            category.UpdateName(categoryDto.Name);
            await _categoryRepository.UpdateAsync(category);
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                UserId = category.UserId
            };
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao atualizar a categoria no banco de dados.", ex);
        }
        catch (KeyNotFoundException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao atualizar categoria.", ex);
        }
    }
}