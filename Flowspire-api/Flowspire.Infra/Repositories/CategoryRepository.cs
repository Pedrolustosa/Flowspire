using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Common;
using Flowspire.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Flowspire.Infra.Repositories;

public class CategoryRepository(ApplicationDbContext context, ILogger<CategoryRepository> logger) : ICategoryRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<CategoryRepository> _logger = logger;

    public async Task<Category> GetByIdAsync(int id)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Categories
                .Include(c => c.FinancialTransactions)
                .FirstOrDefaultAsync(c => c.Id == id);
        }, _logger, nameof(GetByIdAsync));
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Categories
                .Include(c => c.FinancialTransactions)
                .ToListAsync();
        }, _logger, nameof(GetAllAsync));
    }

    public async Task<IEnumerable<Category>> GetByUserIdAsync(string userId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Categories
                .Include(c => c.FinancialTransactions)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }, _logger, nameof(GetByUserIdAsync));
    }

    public async Task AddAsync(Category category)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }, _logger, nameof(AddAsync));
    }

    public async Task UpdateAsync(Category category)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }, _logger, nameof(UpdateAsync));
    }

    public async Task DeleteAsync(Category category)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }, _logger, nameof(DeleteAsync));
    }

    public async Task<bool> ExistsByNameAsync(string userId, string name)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Categories
                .AnyAsync(c => c.UserId == userId && c.Name.ToLower() == name.ToLower());
        }, _logger, nameof(ExistsByNameAsync));
    }

    public async Task<Category> GetCategoryByNameAsync(string userId, string name)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Categories
                .Include(c => c.FinancialTransactions)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Name.ToLower() == name.ToLower());
        }, _logger, nameof(GetCategoryByNameAsync));
    }

    public async Task<IEnumerable<Category>> GetDefaultCategoriesAsync(string userId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Categories
                .Include(c => c.FinancialTransactions)
                .Where(c => c.UserId == userId && c.IsDefault)
                .ToListAsync();
        }, _logger, nameof(GetDefaultCategoriesAsync));
    }

    public async Task<IEnumerable<Category>> GetCategoriesWithTransactionsAsync(string userId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Categories
                .Include(c => c.FinancialTransactions)
                .Where(c => c.UserId == userId && c.FinancialTransactions.Any())
                .ToListAsync();
        }, _logger, nameof(GetCategoriesWithTransactionsAsync));
    }

    public async Task<int> CountByUserAsync(string userId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Categories.CountAsync(c => c.UserId == userId);
        }, _logger, nameof(CountByUserAsync));
    }
}
