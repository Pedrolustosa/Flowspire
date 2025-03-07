using Flowspire.Application.DTOs;

namespace Flowspire.Application.Interfaces;
public interface ICategoryService
{
    Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDto);
    Task<List<CategoryDTO>> GetCategoriesByUserIdAsync(string userId);
    Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categoryDto);
}