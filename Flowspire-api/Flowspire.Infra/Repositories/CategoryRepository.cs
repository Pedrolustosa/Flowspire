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

    public Task<Category?> GetByIdAsync(int id)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Categories
                .Include(c => c.FinancialTransactions)
                .FirstOrDefaultAsync(c => c.Id == id),
            _logger,
            nameof(GetByIdAsync));

    public Task<List<Category>> GetAllAsync()
        => RepositoryHelper.ExecuteAsync(
            () => _context.Categories
                .Include(c => c.FinancialTransactions)
                .ToListAsync(),
            _logger,
            nameof(GetAllAsync));

    public Task<List<Category>> GetByUserIdAsync(string userId)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Categories
                .Include(c => c.FinancialTransactions)
                .Where(c => c.UserId == userId)
                .ToListAsync(),
            _logger,
            nameof(GetByUserIdAsync));

    public Task AddAsync(Category category)
        => RepositoryHelper.ExecuteAsync(
            async () => { await _context.Categories.AddAsync(category); await _context.SaveChangesAsync(); },
            _logger,
            nameof(AddAsync));

    public Task UpdateAsync(Category category)
        => RepositoryHelper.ExecuteAsync(
            async () => { _context.Categories.Update(category); await _context.SaveChangesAsync(); },
            _logger,
            nameof(UpdateAsync));

    public Task DeleteAsync(Category category)
        => RepositoryHelper.ExecuteAsync(
            async () => { _context.Categories.Remove(category); await _context.SaveChangesAsync(); },
            _logger,
            nameof(DeleteAsync));

    public Task<bool> ExistsByNameAsync(string userId, string name)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Categories
                .AnyAsync(c => c.UserId == userId && c.Name.ToLower() == name.ToLower()),
            _logger,
            nameof(ExistsByNameAsync));

    public Task<Category?> GetByNameAsync(string userId, string name)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Categories
                .Include(c => c.FinancialTransactions)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.Name.ToLower() == name.ToLower()),
            _logger,
            nameof(GetByNameAsync));

    public Task<List<Category>> GetDefaultCategoriesAsync(string userId)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Categories
                .Include(c => c.FinancialTransactions)
                .Where(c => c.UserId == userId && c.IsDefault)
                .ToListAsync(),
            _logger,
            nameof(GetDefaultCategoriesAsync));

    public Task<List<Category>> GetCategoriesWithTransactionsAsync(string userId)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Categories
                .Include(c => c.FinancialTransactions)
                .Where(c => c.UserId == userId && c.FinancialTransactions.Any())
                .ToListAsync(),
            _logger,
            nameof(GetCategoriesWithTransactionsAsync));

    public Task<int> CountByUserAsync(string userId)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Categories
                .CountAsync(c => c.UserId == userId),
            _logger,
            nameof(CountByUserAsync));
}
