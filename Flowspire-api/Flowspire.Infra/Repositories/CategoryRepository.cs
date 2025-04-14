using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Data;

namespace Flowspire.Infra.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Category> GetByIdAsync(int id)
    {
        return await _context.Categories
                             .Include(c => c.FinancialTransactions)
                             .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
                             .Include(c => c.FinancialTransactions)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetByUserIdAsync(string userId)
    {
        return await _context.Categories
                             .Include(c => c.FinancialTransactions)
                             .Where(c => c.UserId == userId)
                             .ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string userId, string name)
    {
        return await _context.Categories.AnyAsync(c =>
            c.UserId == userId && c.Name.ToLower() == name.ToLower());
    }

    public async Task<Category> GetCategoryByNameAsync(string userId, string name)
    {
        return await _context.Categories
                             .Include(c => c.FinancialTransactions)
                             .FirstOrDefaultAsync(c =>
                                c.UserId == userId && c.Name.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<Category>> GetDefaultCategoriesAsync(string userId)
    {
        return await _context.Categories
                             .Include(c => c.FinancialTransactions)
                             .Where(c => c.UserId == userId && c.IsDefault)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithTransactionsAsync(string userId)
    {
        return await _context.Categories
                             .Include(c => c.FinancialTransactions)
                             .Where(c => c.UserId == userId && c.FinancialTransactions.Any())
                             .ToListAsync();
    }

    public async Task<int> CountByUserAsync(string userId)
    {
        return await _context.Categories.CountAsync(c => c.UserId == userId);
    }
}
