using Flowspire.Domain.Entities;

namespace Flowspire.Domain.Interfaces;
public interface ICategoryRepository
{
    Task<Category> AddAsync(Category category);
    Task<List<Category>> GetByUserIdAsync(string userId);
    Task<Category> GetByIdAsync(int id);
    Task UpdateAsync(Category category);
}