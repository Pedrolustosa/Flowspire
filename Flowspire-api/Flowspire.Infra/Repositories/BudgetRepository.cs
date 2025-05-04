using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Common;
using Flowspire.Infra.Data;

namespace Flowspire.Infra.Repositories;

public class BudgetRepository(ApplicationDbContext context, ILogger<BudgetRepository> logger) : IBudgetRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<BudgetRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public Task<Budget?> GetByIdAsync(int id)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Budgets
                          .Include(b => b.Category)
                          .FirstOrDefaultAsync(b => b.Id == id),
            _logger,
            nameof(GetByIdAsync));

    public Task<List<Budget>> GetAllAsync()
        => RepositoryHelper.ExecuteAsync(
            () => _context.Budgets
                          .Include(b => b.Category)
                          .ToListAsync(),
            _logger,
            nameof(GetAllAsync));

    public Task<List<Budget>> GetByUserIdAsync(string userId)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Budgets
                          .Where(b => b.UserId == userId)
                          .Include(b => b.Category)
                          .ToListAsync(),
            _logger,
            nameof(GetByUserIdAsync));

    public Task<List<Budget>> GetActiveBudgetsAsync(string userId, DateTime reference)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Budgets
                          .Where(b => b.UserId == userId
                                   && b.StartDate <= reference
                                   && b.EndDate   >= reference)
                          .Include(b => b.Category)
                          .ToListAsync(),
            _logger,
            nameof(GetActiveBudgetsAsync));

    public Task<Budget?> GetBudgetByCategoryIdAsync(string userId, int categoryId)
        => RepositoryHelper.ExecuteAsync(
            () => _context.Budgets
                          .Where(b => b.UserId == userId && b.CategoryId == categoryId)
                          .Include(b => b.Category)
                          .FirstOrDefaultAsync(),
            _logger,
            nameof(GetBudgetByCategoryIdAsync));

    public Task AddAsync(Budget budget)
        => RepositoryHelper.ExecuteAsync(async () =>
        {
            await _context.Budgets.AddAsync(budget);
            await _context.SaveChangesAsync();
        },
        _logger,
        nameof(AddAsync));

    public Task UpdateAsync(Budget budget)
        => RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
        },
        _logger,
        nameof(UpdateAsync));

    public Task DeleteAsync(Budget budget)
        => RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
        },
        _logger,
        nameof(DeleteAsync));
}
