using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Flowspire.Domain.Entities;
using Flowspire.Domain.Interfaces;
using Flowspire.Infra.Data;
using Flowspire.Infra.Common;

namespace Flowspire.Infra.Repositories;

public class BudgetRepository(ApplicationDbContext context, ILogger<BudgetRepository> logger) : IBudgetRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<BudgetRepository> _logger = logger;

    public async Task<Budget> GetByIdAsync(int id)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Budgets
                .FirstOrDefaultAsync(b => b.Id == id);
        }, _logger, nameof(GetByIdAsync));
    }

    public async Task<IEnumerable<Budget>> GetAllAsync()
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Budgets
                .ToListAsync();
        }, _logger, nameof(GetAllAsync));
    }

    public async Task<IEnumerable<Budget>> GetByUserIdAsync(string userId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Budgets
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }, _logger, nameof(GetByUserIdAsync));
    }

    public async Task AddAsync(Budget budget)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            await _context.Budgets.AddAsync(budget);
            await _context.SaveChangesAsync();
        }, _logger, nameof(AddAsync));
    }

    public async Task UpdateAsync(Budget budget)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.Budgets.Update(budget);
            await _context.SaveChangesAsync();
        }, _logger, nameof(UpdateAsync));
    }

    public async Task DeleteAsync(Budget budget)
    {
        await RepositoryHelper.ExecuteAsync(async () =>
        {
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
        }, _logger, nameof(DeleteAsync));
    }

    public async Task<IEnumerable<Budget>> GetActiveBudgetsAsync(string userId, DateTime date)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Budgets
                .Where(b => b.UserId == userId && b.StartDate <= date && b.EndDate >= date)
                .ToListAsync();
        }, _logger, nameof(GetActiveBudgetsAsync));
    }

    public async Task<Budget> GetBudgetByCategoryIdAsync(string userId, int categoryId)
    {
        return await RepositoryHelper.ExecuteAsync(async () =>
        {
            return await _context.Budgets
                .FirstOrDefaultAsync(b => b.UserId == userId && b.CategoryId == categoryId);
        }, _logger, nameof(GetBudgetByCategoryIdAsync));
    }
}